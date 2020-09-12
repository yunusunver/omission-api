using System;
using System.Security.Cryptography;
using System.Text;

namespace omission.api.Utility.Crypto
{
    public static class CryptoPassword
    {
        // SHA1 (string)--> [SHA] (string)
        public static string GetSha(string password)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            var shaInstance = SHA1.Create();
            var computedHash = shaInstance.ComputeHash(passwordBytes);
            return hexFromByte(computedHash);
        }

        private static string hexFromByte(byte[] computedHash)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in computedHash)
            {
                var computedSection = item.ToString("X2");
                sb.Append(computedSection);
            }
            return sb.ToString();
        }
    }
}