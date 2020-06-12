using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Anotar.NLog;
using ElectricShop.Common.Enum;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;

namespace ElectricShop.Utils
{
    public class CheckUtils
    {
        public const int MAXIMUM_DOUBLE_DIGIT = 15;
        public static bool CheckValidMobile(string str)
        {
            const string pattern1 = @"^([+]?[8][4][1-9]\d{8,13})";
            const string pattern2 = @"^0[1-9]\d{8,13}$";
            //const string pattern3 = @"^([8][4][1-9]\d{7,12})";
            var reg1 = new Regex(pattern1);
            var reg2 = new Regex(pattern2);
            //var reg3 = new Regex(pattern3);
            bool result = !string.IsNullOrEmpty(str) && (reg1.IsMatch(str) || reg2.IsMatch(str) /*|| reg3.IsMatch(str)*/);
            return result;
        }

        #region Check ký tự đặc biệt

        private static readonly char[] CharSpecial = new[] { ' ', '~', '!', '#', '@', '$', '^', '&', '*', '(', '?', '{', '}', '[', ']', '<', '>', ':', ';' };

        public static bool CheckValidName(string str)
        {
            if (str == null || CharSpecial == null)
            {
                return false;
            }
            int strSize = str.Length;
            int invalidSize = CharSpecial.Length;
            for (int i = 0; i < strSize; i++)
            {
                char ch = str[i];
                for (int j = 0; j < invalidSize; j++)
                {
                    if (CharSpecial[j] == ch)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CheckFomatNumber(string str)
        {
            const string pattern = @"^[0-9]{1,15}$";
            var reg = new Regex(pattern);

            bool result = !string.IsNullOrEmpty(str) && reg.IsMatch(str);
            //return the value to the calling method
            return result;
        }

        public static bool CheckInValidString(string str)
        {
            // string pattern = @"<(?!\s)>~@#$%^&*:;''/|+";
            const string pattern = @"^([a-zA-Z0-9_\-\.]+)";
            var reg = new Regex(pattern);

            bool result = !string.IsNullOrEmpty(str) && reg.IsMatch(str);
            //return the value to the calling method
            return result;
        }

        public static bool IsNumber(string sValue)
        {
            var pattern = new Regex(@"^[\d]{0,}$");
            return pattern.IsMatch(sValue);
        }

        /// Validate TextBox int value input Currency.
        /// <summary>
        /// Kiểm tra email
        /// </summary>
        public static bool ValidateEmail(string email)
        {
            //Check định dạng email
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var re = new Regex(strRegex);
            if (!re.IsMatch(email))
                return false;
            return true;
        }
        

      



        #endregion

        /// <summary>
        /// Hàm kiểm tra ngày muốn tìm kiếm từ ngày bắt đầu đến ngày kết thúc
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        

        // Kiểm tra giới hạn nhập username (> 3 và < 32)
        public static bool LimitUser(string username)
        {

            var getUsername = username.Trim();
            if (getUsername.Length > 32 || getUsername.Length < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Kiểm tra giới hạn nhập mobile (> 7 và < 15)
        public static bool LimitMobile(string mobile)
        {

            var getUsername = mobile.Trim();
            if (getUsername.Length > 15 || getUsername.Length < 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
        

        #region Kiểm tra trùng UserLogin

        //Trả về true là tồn tại thông tin đăng nhập 
        //Truyền userId đối với trường hợp update, isCreate = false
        public static bool CheckExistUserLogin(string userNameCheck, bool isCreate, long userId = 0)
        {
            try
            {
                //Lấy tất cả userLogin đang được sử dụng
                List<UserLogin> listUserLogin = MemoryInfo.GetAllUserLogin().FindAll(u => u.Username.Equals(userNameCheck, StringComparison.InvariantCultureIgnoreCase));
                if (isCreate)
                {
                    //Nếu là tạo mới userLogin
                    if (listUserLogin.Count > 0) return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.Message);
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Kiểm tra ký tự tiếng việt
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ContainsUnicodeCharacter(string input)
        {
            bool ok = false;
            foreach (char c in input)
            {
                if ((c > 47 && c < 58))
                    ok = true;
                else if ((c > 64 && c < 91))
                    ok = true;
                else if ((c > 96 && c < 123))
                {
                    ok = true;
                }
                else if (c == 45 || c == 46 || c == 95)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }

        /// <summary>
        /// Kiểm tra ký tự nhập vào chỉ là số
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ContainsUnicodeCharacterForNumber(string input)
        {
            bool ok = false;
            foreach (char c in input)
            {
                if ((c > 47 && c < 58))
                    ok = true;
                else
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }




        public static bool CheckUserNameLogin(string input)
        {
            bool ok = false;
            if (input.Length < 3 || input.Length > 32)
            {
                return false;
            }
            foreach (char c in input)
            {
                if ((c > 47 && c < 58))
                    ok = true;
                else if ((c > 64 && c < 91))
                    ok = true;
                else if ((c > 96 && c < 123))
                {
                    ok = true;
                }
                else if (c == 95 || c == 46)
                    ok = true;
                else
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }

        public static bool CheckMobile(string input)
        {
            bool ok = true;
            input = input.Trim();
            if (input.Length < 9 || input.Length > 17)
            {
                return false;
            }
            string threeStr;
            var fistStr = input.Substring(0, 4);
            if (fistStr.Equals("+84."))
            {
                var secondStr = input.Substring(4, 1);
                if (!secondStr.Any(c => c > 48 && c < 58))
                {
                    return false;
                }
                threeStr = input.Substring(5, (input.Length - 5));
            }
            else
            {
                fistStr = input.Substring(0, 3);
                if (fistStr.Equals("84."))
                {
                    var secondStr = input.Substring(3, 1);
                    if (!secondStr.Any(c => c > 48 && c < 58))
                    {
                        return false;
                    }
                    threeStr = input.Substring(4, (input.Length - 4));
                }
                else
                {
                    fistStr = input.Substring(0, 1);
                    if (fistStr.Equals("0"))
                    {
                        var secondStr = input.Substring(1, 1);
                        if (!secondStr.Any(c => c > 48 && c < 58))
                        {
                            return false;
                        }
                        threeStr = input.Substring(2, (input.Length - 2));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (threeStr.Any(c => c <= 47 || c >= 58))
            {
                return false;
            }

            return ok;
        }

        public static bool ContainsUnicodeCharacterForRoleGroup(string input)
        {
            foreach (char c in input)
            {
                if ((33 <= c && c <= 47))
                    return false;
                if ((c >= 58 && c <= 64))
                    return false;
                if ((c >= 91 && c <= 94))
                    return false;
                if ((c >= 123 && c <= 126))
                    return false;
                if (c == 96)
                    return false;
            }
            return true;
        }

        public static string CheckPassWord(string s)
        {
            if (s.Length > 32)
            {
                return ErrorCodeEnum.ErrorPasswordFormat.ToString();
            }
            if (s.Length < 6)
            {
                return ErrorCodeEnum.ErrorPasswordFormat.ToString();
            }
            
            return string.Empty;
        }

        public static bool CadNo(string input)
        {
            if (input.Length < 9 || input.Length > 13)
            {
                return false;
            }
            foreach (var a in input)
            {
                if (a == '0')
                    continue;
                int b = 0;
                int.TryParse(a.ToString(), out b);
                if (b == 0)
                {
                    return false;
                }
            }

            //String cmnd = "[0-9]";
            //if (!Regex.IsMatch(input, cmnd))
            //{
            //    return false;
            //}
            return true;
        }

        
        //  Kiểm tra số chẵn đến phần chục. VD: 20170 -> failed, 20180 -> true
        public static bool CheckMoney(decimal? Money)
        {
            // decimal? _money = Money / 10;
            decimal? _money = Money;
            if (_money % 2 == 0) return true;
            return false;
        }
        //check phep chia het
        public static bool CheckDivisonNumber(decimal dividendNumber, decimal divisorNumber)
        {
            if (dividendNumber == 0 || divisorNumber == 0) return false;
            if (dividendNumber % divisorNumber == 0)
            {
                return true;
            }
            return false;
        }
    }
}