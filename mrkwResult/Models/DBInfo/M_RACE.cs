using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mrkwResult.Models.DBInfo
{
    public class M_RACE
    {
        private string _START_CD = string.Empty;
        public string START_CD
        {
            get { return _START_CD; }
            set { _START_CD = value; }
        }

        private string _GOAL_CD = string.Empty;
        public string GOAL_CD
        {
            get { return _GOAL_CD; }
            set { _GOAL_CD = value; }
        }

        private string _START_NM = string.Empty;
        public string START_NM
        {
            get { return _START_NM; }
            set { _START_NM = value; }
        }

        private string _GOAL_NM = string.Empty;
        public string GOAL_NM
        {
            get { return _GOAL_NM; }
            set { _GOAL_NM = value; }
        }

        private string _REVERSE_FLG = string.Empty;
        public string REVERSE_FLG
        {
            get { return _REVERSE_FLG; }
            set { _REVERSE_FLG = value; }
        }

        private long? _DISPORDER;
        public long? DISPORDER
        {
            get { return _DISPORDER; }
            set { _DISPORDER = value; }
        }

        private string _ITEM = string.Empty;
        public string ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

        private string _SHTC1 = string.Empty;
        public string SHTC1
        {
            get { return _SHTC1; }
            set { _SHTC1 = value; }
        }

        private string _SHTC2 = string.Empty;
        public string SHTC2
        {
            get { return _SHTC2; }
            set { _SHTC2 = value; }
        }

        private string _SHTC_PIC1 = string.Empty;
        public string SHTC_PIC1
        {
            get { return _SHTC_PIC1; }
            set { _SHTC_PIC1 = value; }
        }

        private string _SHTC_PIC2 = string.Empty;
        public string SHTC_PIC2
        {
            get { return _SHTC_PIC2; }
            set { _SHTC_PIC2 = value; }
        }

        private string _REMARK = string.Empty;
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        private DateTime? _SKSI_DT;
        public DateTime? SKSI_DT
        {
            get { return _SKSI_DT; }
            set { _SKSI_DT = value; }
        }

        private string _SKSIPGR_CD = string.Empty;
        public string SKSIPGR_CD
        {
            get { return _SKSIPGR_CD; }
            set { _SKSIPGR_CD = value; }
        }

        private DateTime? _SISIKSHN_DT;
        public DateTime? SISIKSHN_DT
        {
            get { return _SISIKSHN_DT; }
            set { _SISIKSHN_DT = value; }
        }

        private string _SISIKSHNPRG_CD = string.Empty;
        public string SISIKSHNPRG_CD
        {
            get { return _SISIKSHNPRG_CD; }
            set { _SISIKSHNPRG_CD = value; }
        }

        private string _SKJ_FLG = string.Empty;
        public string SKJ_FLG
        {
            get { return _SKJ_FLG; }
            set { _SKJ_FLG = value; }
        }

        private string _STAGE_TYP = string.Empty;
        public string STAGE_TYP
        {
            get { return _STAGE_TYP; }
            set { _STAGE_TYP = value; }
        }
    }
}
