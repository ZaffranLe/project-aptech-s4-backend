using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Anotar.NLog;
using ElectricShop.Common.Enum;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;

namespace ElectricShop.Utils
{
    public class ProductUtils
    {
        public static List<EntityCommand> UpdateProductCount(string listProductId, bool isImport,out List<Product> lstProducts)
        {
            var lstCommandResult = new List<EntityCommand>();
            lstProducts = new List<Product>();
            try
            {
                //listProductId co dang 1-2,2-1,3-4
                // cong so luong san pham tuong ung
                char[] spearatorArray = { ',' };
                char[] spearator = { '-' };
                string[] productCountArray = listProductId.Split(spearatorArray); // ["1-2","2-1","3-4"]
                foreach (var productCount in productCountArray)
                {
                    // 1-2
                    string[] productCountElement = productCount.Split(spearator);
                    if (productCountElement.Length == 0 || productCountElement[0] == null || productCountElement[1] == null)
                        continue;
                    int productId = 0;
                    int productCountInt = 0;
                    if (!int.TryParse(productCountElement[0], out productId) ||
                        !int.TryParse(productCountElement[1], out productCountInt))
                    {
                        Logger.Write(String.Format("Dinh dang so luong san pham sai: {0}", productCount), true);
                        continue;
                    }
                    // lay product ra update
                    var product = MemoryInfo.GetProduct(productId);
                    if (product == null)
                        continue;
                    int oldQuantity = product.Quantity ?? 0;
                    if (isImport)
                    {
                        product.Quantity = oldQuantity + productCountInt;
                    }
                    else
                    {
                        product.Quantity = oldQuantity - productCountInt;
                    }
                    lstProducts.Add(product);
                    lstCommandResult.Add(new EntityCommand { BaseEntity = new Entity.Entity(product), EntityAction = EntityAction.Update });
                }
                
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(), true);
            }
            return lstCommandResult;
        }
    }
}