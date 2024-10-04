using LaxmiSunriseBank.CommonUtlilies;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LaxmiSunriseBank.CommonUtility
{
    public class CommonUtility : ICommonUtility
    {
        public async Task<string> GenerateSHA256Signature(params string[] parameters)
        {
            StringBuilder inputString = new StringBuilder();
            StringBuilder hashStringBuilder = new StringBuilder();
            foreach (var parameter in parameters) { inputString.Append(parameter); }
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString.ToString());
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }
            }
            return hashStringBuilder.ToString();
        }
    }
}
