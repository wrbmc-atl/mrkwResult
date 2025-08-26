using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CommonInstance
    {

        private string _ConnStr = string.Empty;
        public string ConnStr
        {
            get { return _ConnStr; }
            set { _ConnStr = value; }
        }

    }
}
