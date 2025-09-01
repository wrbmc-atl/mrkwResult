using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace mrkwResult.Models.DBInfo
{
    public class T_RACEJSSK : ViewModelBase
    {
        private string _RACEJSSKNO = string.Empty;
        public string RACEJSSKNO
        {
            get { return _RACEJSSKNO; }
            set { _RACEJSSKNO = value; NotifyPropertyChanged(); }
        }

        private string _RACE_KBN = string.Empty;
        public string RACE_KBN
        {
            get { return _RACE_KBN; }
            set { _RACE_KBN = value; NotifyPropertyChanged(); }
        }

        private DateTime? _RACE_DATE;
        public DateTime? RACE_DATE
        {
            get { return _RACE_DATE; }
            set { _RACE_DATE = value; NotifyPropertyChanged(); }
        }

        private string _START_CD = string.Empty;
        public string START_CD
        {
            get { return _START_CD; }
            set { _START_CD = value; NotifyPropertyChanged(); }
        }

        private string _GOAL_CD = string.Empty;
        public string GOAL_CD
        {
            get { return _GOAL_CD; }
            set { _GOAL_CD = value; NotifyPropertyChanged(); }
        }

        private string _STAGE_TYP = string.Empty;
        public string STAGE_TYP
        {
            get { return _STAGE_TYP; }
            set { _STAGE_TYP = value; NotifyPropertyChanged(); }
        }

        private string _REVERSE_FLG = string.Empty;
        public string REVERSE_FLG
        {
            get { return _REVERSE_FLG; }
            set { _REVERSE_FLG = value; NotifyPropertyChanged(); }
        }

        private string _MIRROR_FLG = string.Empty;
        public string MIRROR_FLG
        {
            get { return _MIRROR_FLG; }
            set { _MIRROR_FLG = value; NotifyPropertyChanged(); }
        }

        private long? _RANK;
        public long? RANK
        {
            get { return _RANK; }
            set { _RANK = value; NotifyPropertyChanged(); }
        }

        private long? _HEADCOUNT;
        public long? HEADCOUNT
        {
            get { return _HEADCOUNT; }
            set { _HEADCOUNT = value; NotifyPropertyChanged(); }
        }

        private long? _RATE_END;
        public long? RATE_END
        {
            get { return _RATE_END; }
            set { _RATE_END = value; NotifyPropertyChanged(); }
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

        private string _START_NM_DISP = string.Empty;
        public string START_NM_DISP
        {
            get { return _START_NM_DISP; }
            set { _START_NM_DISP = value; NotifyPropertyChanged(); }
        }
        
        private string _GOAL_NM_DISP = string.Empty;
        public string GOAL_NM_DISP
        {
            get { return _GOAL_NM_DISP; }
            set { _GOAL_NM_DISP = value; NotifyPropertyChanged(); }
        }
        
        private string _RACE_KBN_DISP = string.Empty;
        public string RACE_KBN_DISP
        {
            get { return _RACE_KBN_DISP; }
            set { _RACE_KBN_DISP = value; NotifyPropertyChanged(); }
        }
        
        private string _REVERSE_FLG_DISP = string.Empty;
        public string REVERSE_FLG_DISP
        {
            get { return _REVERSE_FLG_DISP; }
            set { _REVERSE_FLG_DISP = value; NotifyPropertyChanged(); }
        }
        
        private string _MIRROR_FLG_DISP = string.Empty;
        public string MIRROR_FLG_DISP
        {
            get { return _MIRROR_FLG_DISP; }
            set { _MIRROR_FLG_DISP = value; NotifyPropertyChanged(); }
        }
        
        private string _STAGE_TYP_DISP = string.Empty;
        public string STAGE_TYP_DISP
        {
            get { return _STAGE_TYP_DISP; }
            set { _STAGE_TYP_DISP = value; NotifyPropertyChanged(); }
        }

    }
}