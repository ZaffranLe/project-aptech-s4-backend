using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Anotar.NLog;

namespace QuantEdge.Lib.Utils
{
    public class ByteArrayUtils
    {
        public static byte[] Combine(params byte[][] arrays)
        {
            var ret = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;
            foreach (var data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }

        public static bool Split(byte[] arrays, int index, int len, out byte[] first, out byte[] second)
        {
            first = null;
            second = null;
            if (index < 0 || len <= 0) return false;
            if (arrays == null || arrays.Length <= index) return false;

            int len1 = len;
            if (len1 > arrays.Length - index - 1)
                len1 = arrays.Length - index;
            int len2 = arrays.Length - index - len1;
            first = new byte[len1];
            Buffer.BlockCopy(arrays, index, first, 0, len1);
            if (len2 > 0)
            {
                second = new byte[len2];
                Buffer.BlockCopy(arrays, index + len1, second, 0, len2);
            }

            return true;
        }

        public static List<byte[]> Split(byte[] arrays, int range)
        {
            var list = new List<byte[]>();
            int pos = 0;
            int remaining;

            while ((remaining = arrays.Length - pos) > 0)
            {
                var block = new byte[Math.Min(remaining, range)];
                Array.Copy(arrays, pos, block, 0, block.Length);
                list.Add(block);
                pos += block.Length;
            }
            return list;
        }

        public static object ConvertBytesToStruct(byte[] dataIn, Type type)
        {
            try
            {
                int rawsize = Marshal.SizeOf(type);
                if (rawsize > dataIn.Length)
                    return null;
                GCHandle handle = GCHandle.Alloc(dataIn, GCHandleType.Pinned);
                IntPtr buffer = handle.AddrOfPinnedObject();
                object retobj = Marshal.PtrToStructure(buffer, type);
                handle.Free();
                return retobj;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
                return null;
            }
        }

        public static byte[] ConvertStructToBytes(object data)
        {
            try
            {
                int rawsize = Marshal.SizeOf(data);
                var rawdatas = new byte[rawsize];
                GCHandle handle = GCHandle.Alloc(rawdatas, GCHandleType.Pinned);
                IntPtr buffer = handle.AddrOfPinnedObject();
                Marshal.StructureToPtr(data, buffer, false);
                handle.Free();
                return rawdatas;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
                return null;
            }
        }
    }
}