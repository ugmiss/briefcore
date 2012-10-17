using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthDemo
{
    public class Auth : IAuth
    {
        public bool Verify()
        {
            return false;
        }
    }
    public interface IAuth
    {
        bool Verify();
    }
}
