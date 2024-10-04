using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaxmiSunriseBank.CommonUtlilies
{
    public interface ICommonUtility
    {
        Task<string> GenerateSHA256Signature(params string[] parameters);
    }
}
