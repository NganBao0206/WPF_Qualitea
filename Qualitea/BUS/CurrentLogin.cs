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
        private int _LoginID;
        private string _LoginType;
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

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }

        public string LoginType
        {
            get { return _LoginType; }
            set { _LoginType = value; }
        }



    }
}
