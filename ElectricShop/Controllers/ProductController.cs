using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ElectricShop.Common.Enum;
using System.Web.Http.Cors;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;
namespace ElectricShop.Controllers
{
    public class ProductController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion
                var lstData = MemoryInfo.GetAllProduct();
                List<ProductResponse> lstRes = new List<ProductResponse>();
                foreach (var product in lstData)
                {
                    lstRes.Add(new ProductResponse
                    {
                        Product = product,
                        ListImages = ImagesUtils.GetImagesUrl(product.ImageId)
                    });
                }
                var res = new RequestErrorCode(true, null, null);
                res.ListDataResult.AddRange(lstRes);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion
                var data = MemoryInfo.GetProduct(id);
                var res = new RequestErrorCode(true, null, null);
                res.ListDataResult.Add(new ProductResponse
                {
                    Product = data,
                    ListImages = ImagesUtils.GetImagesUrl(data.ImageId)
                });
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]NewProductReq req)
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                if (!Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.CreateProduct))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen tao moi"));
                }
                #endregion
                #region Validate
                if (!Validate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Tạo key
                var productOldKey = Memory.Memory.GetMaxKey(Product.EntityName());
                var propertyOldKey = Memory.Memory.GetMaxKey(Product.EntityName());
                int productNewKey =  productOldKey + 1;
                int propertyNewKey = propertyOldKey + 1;
                var productInsert = req.Product;
                productInsert.Id = productNewKey;// set key

                #endregion

                #region Process
                productInsert.CreatedAt = DateTime.Now;
                productInsert.CreatedBy = userInfo.IdUserLogin;
                productInsert.UpdatedAt = DateTime.Now;
                productInsert.UpdatedBy = userInfo.IdUserLogin;
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                var lstUpdateMemory = new List<BaseEntity>();
                foreach (var property in req.Properties)
                {
                    property.Id = propertyNewKey;
                    property.IdProduct = productNewKey;
                    property.CreatedAt = DateTime.Now;
                    property.CreatedBy = userInfo.IdUserLogin;
                    property.UpdatedAt = DateTime.Now;
                    property.UpdatedBy = userInfo.IdUserLogin;
                    lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(property), EntityAction = EntityAction.Insert });
                    lstUpdateMemory.Add(property);
                    propertyNewKey++;
                }
                
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(productInsert), EntityAction = EntityAction.Insert });
                lstUpdateMemory.Add(productInsert);
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                foreach (var obj in lstUpdateMemory)
                {
                    MemorySet.UpdateAndInsertEntity(obj);
                }
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(),true);
                Logger.Write(string.Format("Request: {0}",req.ToString()),true);
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Product req)
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion

                #region Validate
                if (!ValidateUpdate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetProduct(id);
                if (obj == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), "Khong ton tai"));
                }
                #endregion
                req.Id = obj.Id; // gan lai id de update
                #region Process
                req.UpdatedAt = DateTime.Now;
                req.UpdatedBy = userInfo.IdUserLogin;
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Update });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.UpdateAndInsertEntity(req);
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(), true);
                Logger.Write(string.Format("Request: {0}", req.ToString()), true);
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetProduct(id);
                if (obj == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), "Khong ton tai"));
                }
                #endregion

                // check role 
                if (!Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.None))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen xoa"));
                }

                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(obj), EntityAction = EntityAction.Delete });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.RemoveEntity(obj);
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(), true);
            }
            return BadRequest("Unknow");
        }

        #region Validation
        private bool Validate(NewProductReq obj, out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {

            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                throw;
            }
            return true;
        }

        private bool ValidateUpdate(Product obj, out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {

            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                throw;
            }
            return true;
        }
        #endregion


    }
}

