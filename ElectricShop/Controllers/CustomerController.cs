﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ElectricShop.Common.Enum;
using ElectricShop.Common.Object;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Models;
using ElectricShop.ReaderDatabase;

namespace ElectricShop.Controllers
{
    public class CustomerController : ApiController
    {
        
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var entityQry = new EntityQuery
                {
                    EntityName = Customer.EntityName(),
                    QueryAction = EntityGet.GetAllValues
                };
                var colection = new DescriptorColection
                {
                    Logical = LogicalOperator.AND
                };
                colection.Descriptors.Add(new Descriptor
                {
                    FieldName = Customer.CustomerFields.Id.ToString(),
                    FieldValue = Descriptor.GetInputValue(id),
                    Operator = DataOperator.IsEqualTo
                });
                entityQry.DescriptorColection = colection;
                entityQry.IsGetMaxKey = true;
                entityQry = ProcessReadDatabaseUtils.GetEntityQuery(entityQry);
                var listValue = new List<Customer>();
                if (entityQry.ReturnValue != null)
                {
                    listValue = entityQry.ReturnValue.Select(baseEntity =>
                        baseEntity.GetEntity() as Customer).ToList();
                }

                return Ok(listValue);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }

            Logger.Write("--------------------ErrorCodeEnum.Unknow---------------------------------");
            return BadRequest("Unknow");
        }

        public async Task<IHttpActionResult> Post([FromBody]Customer req)
        {
            try
            {
                #region Validate
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                if (!Validate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Insert });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }

            Logger.Write("--------------------ErrorCodeEnum.Unknow---------------------------------");
            return BadRequest("Unknow");
        }
        #region Custom Function

        private bool Validate(Customer customer, out string errorCode, out string errorMess)
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
