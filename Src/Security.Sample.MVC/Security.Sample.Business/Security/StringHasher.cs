﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Security.Sample.Service.Security
{
    public class StringHasher : IStringHasher
    {
        private int saltSize = 4;

        public bool CompareStringToHash(string s, string hash)
        {
            byte[] sourceArray = Convert.FromBase64String(hash);
            byte[] destinationArray = new byte[this.SaltSize];
            Array.Copy(sourceArray, sourceArray.Length - destinationArray.Length, destinationArray, 0, destinationArray.Length);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] buffer4 = new byte[bytes.Length + destinationArray.Length];
            Array.Copy(bytes, 0, buffer4, 0, bytes.Length);
            Array.Copy(destinationArray, 0, buffer4, bytes.Length, destinationArray.Length);
            byte[] buffer5 = new SHA1Managed().ComputeHash(buffer4);
            byte[] buffer6 = new byte[buffer5.Length + destinationArray.Length];
            Array.Copy(buffer5, 0, buffer6, 0, buffer5.Length);
            Array.Copy(destinationArray, 0, buffer6, buffer5.Length, destinationArray.Length);
            return Convert.ToBase64String(sourceArray).Equals(Convert.ToBase64String(buffer6));
        }

        public string Encrypt(string original)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(original);
            byte[] data = new byte[this.saltSize];
            new RNGCryptoServiceProvider().GetNonZeroBytes(data);
            byte[] destinationArray = new byte[bytes.Length + data.Length];
            Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
            Array.Copy(data, 0, destinationArray, bytes.Length, data.Length);
            byte[] sourceArray = new SHA1Managed().ComputeHash(destinationArray);
            byte[] buffer5 = new byte[sourceArray.Length + data.Length];
            Array.Copy(sourceArray, 0, buffer5, 0, sourceArray.Length);
            Array.Copy(data, 0, buffer5, sourceArray.Length, data.Length);
            return Convert.ToBase64String(buffer5);
        }

        public int SaltSize
        {
            get
            {
                return this.saltSize;
            }
            set
            {
                this.saltSize = value;
            }
        }
    }
}
