using ElectricShop.Models;

namespace ElectricShop.Const
{
    public class ErrorCodeDef
    {
        internal static TokenErrorCode ErrorCodeBodyNull()
        {
            return new TokenErrorCode
            {
                error = "invalid_body",
                error_description = "Body null"
            };
        }

        internal static object GrantTypeInvalid()
        {
            return new TokenErrorCode
            {
                error = "unsupport_grant_type",
                error_description = "Unsupport grant type"
            };
        }

        internal static object UserNamePasswordInvalid()
        {
            return new TokenErrorCode
            {
                error = "invalid_grant",
                error_description = "Wrong user name or password!"
            };
        }

        internal static object IpInvalid()
        {
            return new TokenErrorCode
            {
                error = "invalid_grant",
                error_description = "Invalid IP request"
            };
        }

        internal static object DataInvalid()
        {
            return new TokenErrorCode
            {
                error = "invalid_body",
                error_description = "Data invalid"
            };
        }

        internal static object TokenInvalid(int? seq = null)
        {
            return new RequestErrorCode
            {
                IsSuccess = false,
                ErrorCode = 401,
                ErrorMsg = "Invalid Access Token!",
                SeqResult = seq ?? 0
            };
        }

        internal static object Success(int? seq = null)
        {
            return new RequestErrorCode
            {
                IsSuccess = true,
                ErrorCode = 2,
                ErrorMsg = "Success",
                SeqResult = seq ?? 0
            };
        }
    }
}