using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAsyncEx.Model
{
    public class USERINFO
    {
        private string username;
        private int userage;
        private string usergender;
        private string userjob;
        private string usermbti;

        public string USERNAME
        {
            get { return username; }
            set { username = value; }
        }

        public string USERGENDER
        {
            get { return usergender; }
            set { usergender = value; }
        }

        public int USERAGE
        {
            get { return userage; }
            set { userage = value; }
        }

        public string USERJOB
        {
            get { return userjob; }
            set { userjob = value; }
        }

        public string USERMBTI
        {
            get { return usermbti; }
            set { usermbti = value; }
        }


    }
}
