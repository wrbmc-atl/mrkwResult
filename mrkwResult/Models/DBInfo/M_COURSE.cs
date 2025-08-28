using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace mrkwResult.Models.DBInfo
{
    public class M_COURSE : ViewModelBase
    {
        private string _COURSE_CD = string.Empty;
        public string COURSE_CD
        {
            get { return _COURSE_CD; }
            set { _COURSE_CD = value; NotifyPropertyChanged(); }
        }

        private string _COURSE_NM = string.Empty;
        public string COURSE_NM
        {
            get { return _COURSE_NM; }
            set { _COURSE_NM = value; NotifyPropertyChanged(); }
        }

        private long? _DISPORDER;
        public long? DISPORDER
        {
            get { return _DISPORDER; }
            set { _DISPORDER = value; NotifyPropertyChanged(); }
        }

        private string _ITEM = string.Empty;
        public string ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; NotifyPropertyChanged(); }
        }

        private string _SHTC = string.Empty;
        public string SHTC
        {
            get { return _SHTC; }
            set { _SHTC = value; NotifyPropertyChanged(); }
        }

        private string _SHTC_PIC = string.Empty;
        public string SHTC_PIC
        {
            get { return _SHTC_PIC; }
            set { _SHTC_PIC = value; NotifyPropertyChanged(); }
        }

        private string _REMARK = string.Empty;
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; NotifyPropertyChanged(); }
        }

        private DateTime? _SKSI_DT;
        public DateTime? SKSI_DT
        {
            get { return _SKSI_DT; }
            set { _SKSI_DT = value; NotifyPropertyChanged(); }
        }

        private string _SKSIPGR_CD = string.Empty;
        public string SKSIPGR_CD
        {
            get { return _SKSIPGR_CD; }
            set { _SKSIPGR_CD = value; NotifyPropertyChanged(); }
        }

        private DateTime? _SISIKSHN_DT;
        public DateTime? SISIKSHN_DT
        {
            get { return _SISIKSHN_DT; }
            set { _SISIKSHN_DT = value; NotifyPropertyChanged(); }
        }

        private string _SISIKSHNPRG_CD = string.Empty;
        public string SISIKSHNPRG_CD
        {
            get { return _SISIKSHNPRG_CD; }
            set { _SISIKSHNPRG_CD = value; NotifyPropertyChanged(); }
        }

        private string _SKJ_FLG = string.Empty;
        public string SKJ_FLG
        {
            get { return _SKJ_FLG; }
            set { _SKJ_FLG = value; NotifyPropertyChanged(); }
        }

        private string _COURSE_NM_DISP = string.Empty;
        public string COURSE_NM_DISP
        {
            get { return _COURSE_NM_DISP; }
            set { _COURSE_NM_DISP = value; NotifyPropertyChanged(); }
        }

    }
}
