using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class CommonInstance
    {

        private static string _ConnStr = string.Empty;
        public static string ConnStr
        {
            get { return _ConnStr; }
            set { _ConnStr = value; }
        }

    }
}
