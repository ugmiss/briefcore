using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthDemo
{
    public class AuthManagement : IAuthManagement
    {
        public bool Verify()
        {
            return false;
        }
    }
    public interface IAuthManagement
    {
        bool Verify();
    }
}
