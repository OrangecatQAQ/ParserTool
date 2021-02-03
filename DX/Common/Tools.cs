﻿using DX.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DX.Common
{
    class Tools
    {

        public static byte[] GetPartialBytes(byte[] bytes, int start, int count = -1)
        {
            if (bytes != null && start >= 0 && start < bytes.Length && count < 0)
            {
                count = bytes.Length - start;
            }

            if (bytes == null || count < 0 || start < 0 || start >= bytes.Length || start + count > bytes.Length)
                return null;

            byte[] ret = new byte[count];
            Array.Copy(bytes, start, ret, 0, count);

            return ret;
        }

        public static int bytesToInt(byte[] src, int offset)
        {
            int value;
            value = (int)((src[offset] & 0xFF)
                    | ((src[offset + 1] & 0xFF) << 8)
                    | ((src[offset + 2] & 0xFF) << 16)
                    | ((src[offset + 3] & 0xFF) << 24));
            return value;
        }

        public static ulong bytesToUlong(byte[] src, int offset)
        {
            ulong value;

             value = (ulong)(((src[offset] & 0xffuL) << 32)
                    | ((src[offset + 1] & 0xffuL) << 40)
                    | ((src[offset + 2] & 0xffuL) << 48)
                    | ((src[offset + 3] & 0xffuL) << 56)
                    | ((src[offset + 4] & 0xffuL))
                    | ((src[offset + 5] & 0xffuL) << 8)
                    | ((src[offset + 6] & 0xffuL) << 16)
                    | ((src[offset + 7] & 0xffuL) << 24));

            return value;
        }

        public static ulong bytesToUlong2(byte[] src, int offset)
        {
            byte[] a3 = new byte[] { src[offset+4], src[offset+5], src[offset+6], src[offset+7], src[offset], src[offset+1], src[offset+2], src[offset+3] };

            return BitConverter.ToUInt64(a3,0);
        }

        public static ulong bytesToUlong3(byte[] src, int offset)
        {
            ulong value;

            var x = ((src[offset] & 0xFFUL) << 32)
                    | ((src[offset + 1] & 0xFFUL) << 40)
                    | ((src[offset + 2] & 0xFFUL) << 48)
                    | ((src[offset + 3] & 0xFFUL) << 56)
                    | ((src[offset + 4] & 0xFFUL))
                    | ((src[offset + 5] & 0xFFUL) << 8)
                    | ((src[offset + 6] & 0xFFUL) << 16)
                    | ((src[offset + 7] & 0xFFUL) << 24);
            value = (ulong)x;
            return value;
        }

        public static int bytes2ToInt(byte high, byte low)
        {
            int value;
            value = (int)((low & 0xFF)
                    | ((high & 0xFF) << 8));
            return value;
        }

        static public byte[] ReadPcapngFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            long size = fs.Length;
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);
            fs.Close();
            return array;
        }

        static public List<Block> BytesToBlock(byte[] byteArray)
        {
            List<Block> BlockList = new List<Block>();
            for (int i = 0; i < byteArray.Length;)
            {
                int offset = bytesToInt(byteArray, i + 4);
                Block block = new Block(GetPartialBytes(byteArray,i,offset));
                BlockList.Add(block);
                i += offset;
            }
            return BlockList;
        }

        



    }
}
