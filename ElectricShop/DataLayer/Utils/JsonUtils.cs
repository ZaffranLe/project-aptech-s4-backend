using System;
using System.Collections.Generic;
using Anotar.NLog;
using QuantEdge.Definition;
using QuantEdge.Message.Broadcast;
using QuantEdge.Message.Common;
using QuantEdge.Message.Request;
using QuantEdge.Message.Response;
using fastJSON;

namespace QuantEdge.Lib.Utils
{
    public class JsonUtils
    {
        /*
        /// <summary>
        /// Use the optimized fast Dataset Schema format (default = True)
        /// </summary>
        public bool UseOptimizedDatasetSchema = true;
        /// <summary>
        /// Use the fast GUID format (default = True)
        /// </summary>
        public bool UseFastGuid = true;
        /// <summary>
        /// Serialize null values to the output (default = True)
        /// </summary>
        public bool SerializeNullValues = true;
        /// <summary>
        /// Use the UTC date format (default = True)
        /// </summary>
        public bool UseUTCDateTime = true;
        /// <summary>
        /// Show the readonly properties of types in the output (default = False)
        /// </summary>
        public bool ShowReadOnlyProperties = false;
        /// <summary>
        /// Use the $types extension to optimise the output json (default = True)
        /// </summary>
        public bool UsingGlobalTypes = true;
        /// <summary>
        /// Ignore case when processing json and deserializing 
        /// </summary>
        [Obsolete("Not needed anymore and will always match")]
        public bool IgnoreCaseOnDeserialize = false;
        /// <summary>
        /// Anonymous types have read only properties 
        /// </summary>
        public bool EnableAnonymousTypes = false;
        /// <summary>
        /// Enable fastJSON extensions $types, $type, $map (default = True)
        /// </summary>
        public bool UseExtensions = true;
        /// <summary>
        /// Use escaped unicode i.e. \uXXXX format for non ASCII characters (default = True)
        /// </summary>
        public bool UseEscapedUnicode = true;
        /// <summary>
        /// Output string key dictionaries as "k"/"v" format (default = False) 
        /// </summary>
        public bool KVStyleStringDictionary = false;
        /// <summary>
        /// Output Enum values instead of names (default = False)
        /// </summary>
        public bool UseValuesOfEnums = false;
        /// <summary>
        /// Ignore attributes to check for (default : XmlIgnoreAttribute)
        /// </summary>
        public List<Type> IgnoreAttributes = new List<Type> { typeof(System.Xml.Serialization.XmlIgnoreAttribute) };
        /// <summary>
        /// If you have parametric and no default constructor for you classes (default = False)
        /// 
        /// IMPORTANT NOTE : If True then all initial values within the class will be ignored and will be not set
        /// </summary>
        public bool ParametricConstructorOverride = false;
        /// <summary>
        /// Serialize DateTime milliseconds i.e. yyyy-MM-dd HH:mm:ss.nnn (default = false)
        /// </summary>
        public bool DateTimeMilliseconds = false;
        /// <summary>
        /// Maximum depth for circular references in inline mode (default = 20)
        /// </summary>
        public byte SerializerMaxDepth = 20;
        /// <summary>
        /// Inline circular or already seen objects instead of replacement with $i (default = False) 
        /// </summary>
        public bool InlineCircularReferences = false;
        /// <summary>
        /// Save property/field names as lowercase (default = false)
        /// </summary>
        public bool SerializeToLowerCaseNames = false;
         */

        #region Serialize for Config, object,...
        private static readonly JSONParameters _parameters = new JSONParameters()
        {
            UsingGlobalTypes = false,//khong con toi uu hoa phan thuc hien chuyen du lieu
            UseExtensions = true, //Set gia tri nay cho da thua ke
            UseUTCDateTime = false,//mac dinh thoi gian dang set la UTC nen neu set lan nua thi se mat gia tri
            DateTimeMilliseconds = true,//mac dinh yeu cau time co milisecond
            SerializerMaxDepth = byte.MaxValue,//chieu sau toi da
            InlineCircularReferences = true,

            //Gan thong tin mac dinh
            UseOptimizedDatasetSchema = true,
            UseFastGuid = true,
            SerializeNullValues = false,
            ShowReadOnlyProperties = false,
            IgnoreCaseOnDeserialize = false,
            EnableAnonymousTypes = false,
            UseEscapedUnicode = true,
            KVStyleStringDictionary = false,
            UseValuesOfEnums = true,
            //IgnoreAttributes = new List<Type>(), //Ko duoc set do du lieu can phai xoa XmlInoge
            ParametricConstructorOverride = false,
            SerializeToLowerCaseNames = false,
        };

        public static string Serialize(object obj)
        {
            lock (obj)
            {
                return JSON.ToJSON(obj, _parameters);
            }
        }

        public static T Deserialize<T>(string stringJson)
        {
            try
            {
                return JSON.ToObject<T>(stringJson, _parameters);
            }
            catch (Exception)
            {
                if (stringJson.Length > 100000)
                    LogTo.Error("Invalid parse msg : (1000) :" + stringJson.Substring(0, 1000));
                else LogTo.Error("Invalid parse msg :" + stringJson);
                throw;
            }
        }

        public static object Deserialize(string stringJson)
        {
            try
            {
                return JSON.ToObject(stringJson, _parameters);
            }
            catch (Exception)
            {
                if (stringJson.Length > 100000)
                    LogTo.Error("Invalid parse msg : (1000) :" + stringJson.Substring(0, 1000));
                else LogTo.Error("Invalid parse msg :" + stringJson);
                throw;
            }
        }

        #endregion

        #region Serialize for Network
        private static readonly JSONParameters _param = new JSONParameters()
        {
            UsingGlobalTypes = false,//khong con toi uu hoa phan thuc hien chuyen du lieu
            UseExtensions = false, //Set gia tri nay cho da thua ke
            UseUTCDateTime = false,//mac dinh thoi gian dang set la UTC nen neu set lan nua thi se mat gia tri
            DateTimeMilliseconds = true,//mac dinh yeu cau time co milisecond
            SerializerMaxDepth = byte.MaxValue,//chieu sau toi da
            InlineCircularReferences = true,

            //Gan thong tin mac dinh
            UseOptimizedDatasetSchema = true,
            UseFastGuid = true,
            SerializeNullValues = false,
            ShowReadOnlyProperties = false,
            IgnoreCaseOnDeserialize = false,
            EnableAnonymousTypes = false,
            UseEscapedUnicode = true,
            KVStyleStringDictionary = false,
            UseValuesOfEnums = true,
            //IgnoreAttributes = new List<Type>(),  //Ko duoc set do du lieu can phai xoa XmlInoge
            ParametricConstructorOverride = false,
            SerializeToLowerCaseNames = false,
        };

        public static string SerializeMessage(IMessage obj)
        {
            lock (obj)
            {
                if (obj is Request)
                {
                    var msg = Converter.GetRequest(obj as Request);
                    return JSON.ToJSON(msg, _param);
                }
                if (obj is Broadcast)
                {
                    var msg = Converter.GetBroadcast(obj as Broadcast);
                    return JSON.ToJSON(msg, _param);
                }
                if (obj is Response)
                {
                    var msg = Converter.GetResponse(obj as Response);
                    return JSON.ToJSON(msg, _param);
                }
            }
            return null;
        }

        public static IMessage DeserializeMessage(string stringJson)
        {
            try
            {
                //hash code tam phan nay de nhan biet broad, res, req
                //Sau khi dung.vu sua xong terminal thi check ReceiveData tai EventUnMarshaler de bet truoc duoc bang 1 bien cu the
                if (stringJson.StartsWith("{\"$type\":\"QuantEdge.Message.Common.PartialMessage, "))
                {
                    //Neu la msg chia nho thi parse luon msg nay
                    var part = JSON.ToObject<PartialMessage>(stringJson, _param);
                    return part;
                }
                var req = JSON.ToObject<Msg>(stringJson, _param);
                return Converter.GetMessage(req);
            }
            catch (Exception)
            {
                if (stringJson.Length > 100000)
                    LogTo.Error("Invalid parse msg : (1000) :" + stringJson.Substring(0, 1000));
                else LogTo.Error("Invalid parse msg :" + stringJson);
                throw;
            }
        }

        #endregion
    }
}
