using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using ElectricShop.Common.Enum;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;
namespace ElectricShop.Controllers
{
    public class NewOrderController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]NewOrderReq req)
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
               

                #region Validate
                if (!ValidateUpdate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                var keyCustomer = Memory.Memory.GetMaxKey(Customer.EntityName());
                var keyOrderDetail = Memory.Memory.GetMaxKey(OrderDetail.EntityName());
                #region Tao ra khach hang va don hang

                var customer = new Customer
                {
                    Address = req.Address,
                    CreatedAt = DateTime.Now,
                    Email = req.Email,
                    CreatedBy = 1,
                    Id = keyCustomer + 1,
                    Name = req.Name ?? String.Format("Customer{0}", keyCustomer + 1),
                    Phone = req.Phone,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 1
                };
                StringBuilder builder = new StringBuilder();
                foreach (var id in req.ListProductId)
                {
                    builder.Append(id).Append(",");
                }

                builder.Remove(builder.Length - 1, 1);
                var orderDetail = new OrderDetail
                {
                    Address = req.Address,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    Date = DateTime.Now,
                    Id = keyOrderDetail + 1,
                    IdCustomer = customer.Id,
                    IdEmployee = 0,
                    ListProductId = builder.ToString(),
                    Name = string.Format("Order{0}", keyOrderDetail + 1),
                    OrderStatus = OrderStatus.New.ToString(),
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 1
                };

                #endregion
                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(customer), EntityAction = EntityAction.Insert });
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(orderDetail), EntityAction = EntityAction.Insert });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.UpdateAndInsertEntity(customer);
                MemorySet.UpdateAndInsertEntity(orderDetail);
                // gui mail 
                var res =  EmailUtils.SendEmailNewOrder(orderDetail, customer);
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }

        #region Validation
        private bool Validate(UserInfo obj, out string errorCode, out string errorMess)
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

        private bool ValidateUpdate(NewOrderReq obj, out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {
                if (obj.Email == null)
                {
                    errorMess = "Email khong hop le";
                    errorCode = ErrorCodeEnum.ErrorEmailFormat.ToString();
                    return false;
                }
                if (obj.Email != null)
                {
                    if (!CheckUtils.ValidateEmail(obj.Email))
                    {
                        errorMess = "Email khong hop le";
                        errorCode = ErrorCodeEnum.ErrorEmailFormat.ToString();
                        return false;
                    }
                }

                if (obj.Phone == null)
                {
                    errorMess = "Phone khong hop le";
                    errorCode = ErrorCodeEnum.ErrorPhoneFormat.ToString();
                    return false;
                }
                if (obj.Phone != null)
                {
                    if (!CheckUtils.CheckValidMobile(obj.Phone))
                    {
                        errorMess = "Phone khong hop le";
                        errorCode = ErrorCodeEnum.ErrorPhoneFormat.ToString();
                        return false;
                    }
                }
                if (obj.Address == null)
                {
                    errorMess = "Dia chi khong duoc de trong";
                    errorCode = ErrorCodeEnum.ErrorAddressIsNull.ToString();
                    return false;
                }
                if (obj.ListProductId == null || obj.ListProductId.Count == 0)
                {
                    errorMess = "San pham khong duoc de trong";
                    errorCode = ErrorCodeEnum.ErrorListProductIsNull.ToString();
                    return false;
                }
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

