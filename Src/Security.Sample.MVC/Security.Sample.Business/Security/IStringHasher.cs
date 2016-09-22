using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.Service.Security
{
    public interface IStringHasher
    {
        bool CompareStringToHash(string s, string hash);
        string Encrypt(string original);

        int SaltSize { get; set; }
    }
}
