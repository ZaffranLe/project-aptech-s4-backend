using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Anotar.NLog;

namespace QuantEdge.Lib.Utils
{
    public class EncodingUtils
    {
        public static UTF8Encoding _encodingUtf8 = null;
        public static CultureInfo CurrentCulture = null;
        private static Dictionary<char, char> dicKey = null;

        private static string FormatExt(string msg)
        {
            if (dicKey == null)
            {
                dicKey = new Dictionary<char, char>();
                var list = "{$type:QuanEdg.MsRqSikrGmo, V=10ClPbcKTWDvL[NIfAh\\F3B25-78w}94OH6UZXY_()/J]x@j&z#%+;!*?\"";
                var len = list.Length;
                for (var i = 0; i < len; i++)
                {
                    var ctx = list[i];
                    if (dicKey.ContainsKey(ctx)) continue;
                    var chx = list[len - i - 1];
                    dicKey[ctx] = chx;
                    if (dicKey.ContainsKey(chx)) continue;
                    dicKey[chx] = ctx;
                }
            }
            var str = new StringBuilder();
            foreach (var c in msg)
                if (dicKey.ContainsKey(c))
                    str.Append(dicKey[c]);
                else
                    str.Append(c);
            return str.ToString();
        }

        public static void InitEncoding()
        {
            try
            {
                if (CurrentCulture == null)
                {
                    //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    //Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    CurrentCulture = Thread.CurrentThread.CurrentCulture;
                    LogTo.Info("Set CurrentCulture Endcoding= " + CurrentCulture.Name);
                    _encodingUtf8 = new UTF8Encoding(true, true);
                }
            }
            catch (Exception ex)
            {
                if (CurrentCulture == null)
                {
                    //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    //Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    CurrentCulture = Thread.CurrentThread.CurrentCulture;
                    LogTo.Info("Set CurrentCulture Endcoding= " + CurrentCulture.Name);
                    _encodingUtf8 = new UTF8Encoding(true, true);
                }
                LogTo.Error(ex.ToString());
            }
        }

        public static string GetString(byte[] msg)
        {
            try
            {
                if (_encodingUtf8 == null) InitEncoding();
                try
                {
                    if (CurrentCulture.Name == "")
                        CurrentCulture = Thread.CurrentThread.CurrentCulture;
                    if (CurrentCulture.Name != Thread.CurrentThread.CurrentCulture.Name)
                    {
                        LogTo.Error("CurrentCulture.Name != Thread.CurrentThread.CurrentCulture.Name\r\n" +
                                    "CurrentCulture.Name = " + CurrentCulture.Name + "\r\n" +
                                    "Thread.CurrentThread.CurrentCulture.Name = " + Thread.CurrentThread.CurrentCulture.Name);
                        InitEncoding();
                    }
                }
                catch (Exception ex)
                {
                    LogTo.Error(ex.ToString());
                }

                //Truoc do dung ham Encoding.UTF8.GetString(msg);
                return _encodingUtf8.GetString(msg);
                //var raw = _encodingUtf8.GetString(msg);
                //return FormatExt(raw);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return "";
        }

        public static byte[] GetBytes(string jsonStr)
        {
            try
            {
                if (_encodingUtf8 == null) InitEncoding();
                try
                {
                    if (CurrentCulture.Name == "")
                        CurrentCulture = Thread.CurrentThread.CurrentCulture;
                    if (CurrentCulture.Name != Thread.CurrentThread.CurrentCulture.Name)
                    {
                        LogTo.Error("CurrentCulture.Name != Thread.CurrentThread.CurrentCulture.Name\r\n" +
                                    "CurrentCulture.Name = " + CurrentCulture.Name + "\r\n" +
                                    "Thread.CurrentThread.CurrentCulture.Name = " + Thread.CurrentThread.CurrentCulture.Name);
                        InitEncoding();
                    }
                }
                catch (Exception ex)
                {
                    LogTo.Error(ex.ToString());
                }

                //Truoc do dung ham Encoding.UTF8.GetString(msg);
                //var raw = FormatExt(jsonStr);
                return _encodingUtf8.GetBytes(jsonStr);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return null;
        }
    }
}