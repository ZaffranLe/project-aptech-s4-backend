using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Misc;
using ElectricShop.Entity;
using ElectricShop.Memory;
using ElectricShop.ReaderDatabase;

namespace ElectricShop
{
    public class AppGlobal
    {
        public static bool InitMemory()
        {
            try
            {
                var lstEntityName = Memory.Memory.GetListEntityNameInit();
                List<EntityQuery> lstEntityQuery = lstEntityName.Select(x => EntityQuery.RequestAllByName(x)).ToList();
                lstEntityQuery = ProcessReadDatabaseUtils.GetListEntityQuery(lstEntityQuery);
                foreach (var entityQuery in lstEntityQuery)
                {
                    if (entityQuery.ReturnValue != null)
                    {
                        foreach (var entity in entityQuery.ReturnValue)
                        {

                            MemorySet.UpdateAndInsertEntity(entity.GetEntity());
                        }
                    }   
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}