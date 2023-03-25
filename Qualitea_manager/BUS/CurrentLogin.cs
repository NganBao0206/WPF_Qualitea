using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BUS
{
    public class CurrentLogin
    {
        private static readonly object padlock = new object();
        private static CurrentLogin _instance;
        private Login _login;
        private CurrentLogin()
        {

        }
        public static CurrentLogin Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new CurrentLogin();
                    }
                    return _instance;
                }
            }
        }

        public Login Login
        {
            get { return _login; }
            set { _login = value; }
        }

        
    }
}
