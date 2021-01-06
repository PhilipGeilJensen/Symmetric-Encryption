using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Symmetric_Encryption
{
    public abstract class Encrypt
    {
        /// <summary>
        /// Generates a random key
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GenerateKey(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
        /// <summary>
        /// Encrypts the data, with a key and iv
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public abstract byte[] Encryption(byte[] dataToEncrypt, byte[] key, byte[] iv);

        /// <summary>
        /// Decrypts the data with a key and iv
        /// </summary>
        /// <param name="dataToDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public abstract byte[] Decryption(byte[] dataToDecrypt, byte[] key, byte[] iv);
    }
}
