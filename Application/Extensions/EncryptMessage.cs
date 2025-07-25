using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class EncryptMessage
    {

        public static (byte[] Cipher, byte[] Nonce, byte[] Tag) EncryptE2E(byte[] plain, byte[] key)
        {
            var nonce = RandomNumberGenerator.GetBytes(12); 
            var cipher = new byte[plain.Length];
            var tag = new byte[16];

            using var aes = new AesGcm(key);
            aes.Encrypt(nonce, plain, cipher, tag);

            return (cipher, nonce, tag);
        }


        public static byte[] DecryptE2E(byte[] cipher, byte[] nonce, byte[] tag, byte[] key)
        {
            var plain = new byte[cipher.Length];
            using var aes = new AesGcm(key);
            aes.Decrypt(nonce, cipher, tag, plain);
            return plain;
        }
    }
}
