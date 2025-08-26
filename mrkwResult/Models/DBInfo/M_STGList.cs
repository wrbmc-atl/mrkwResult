using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mrkwResult.Models.DBInfo
{
    public class M_STGList
    {
        private string _STAGE_CD = string.Empty;
        public string STAGE_CD
        {
            get { return _STAGE_CD; }
            set { _STAGE_CD = value; }
        }
        private string _STAGE_NM = string.Empty;
        public string STAGE_NM
        {
            get { return _STAGE_NM; }
            set { _STAGE_NM = value; }
        }
    }
}
