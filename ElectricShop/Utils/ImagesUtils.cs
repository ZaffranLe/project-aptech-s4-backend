using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ElectricShop.Common.Enum;
using ElectricShop.Memory;

namespace ElectricShop.Utils
{
    public class ImagesUtils
    {
        public static List<string> GetImagesUrl(string lstImagesIdString)
        {
            var lstResult = new List<string>();
            try
            {
                String[] spearator = { "," };

                // using the method 
                String[] lstImagesId = lstImagesIdString.Split(spearator,
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (String imgIdString in lstImagesId)
                {
                    //parse ve int
                    int imagesId = 0;
                    if (!Int32.TryParse(imgIdString, out imagesId))
                        continue;
                    // lay imagesObject
                    var imagesObj = MemoryInfo.GetImage(imagesId);
                    var imagesUrl = AppGlobal.ElectricConfig.BaseUrl + imagesObj.ImageUrl;
                    lstResult.Add(imagesUrl);
                }

                return lstResult;
            }
            catch (Exception ex)
            {
                Logger.Write("Co loi trong qua trinh get ImagesUrl");
            }
            return lstResult;
        }
        
    }

    
}
