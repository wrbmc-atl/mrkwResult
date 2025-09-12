using DocumentFormat.OpenXml.Drawing;
using Models;
using mrkwResult.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace mrkwResult.Models.DBInfo
{
    public class M_RACE : ViewModelBase
    {
        public M_RACE()
        {

        }

        public M_RACE(T_RACEJSSK raceJssk)
        {
            START_CD = raceJssk.START_CD;
            GOAL_CD = raceJssk.GOAL_CD;
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

        private string _START_NM = string.Empty;
        public string START_NM
        {
            get { return _START_NM; }
            set { _START_NM = value; NotifyPropertyChanged(); }
        }

        private string _GOAL_NM = string.Empty;
        public string GOAL_NM
        {
            get { return _GOAL_NM; }
            set { _GOAL_NM = value; NotifyPropertyChanged(); }
        }

        private string _REVERSE_FLG = string.Empty;
        public string REVERSE_FLG
        {
            get { return _REVERSE_FLG; }
            set { _REVERSE_FLG = value; NotifyPropertyChanged(); }
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

        private string _SHTC1 = string.Empty;
        public string SHTC1
        {
            get { return _SHTC1; }
            set { _SHTC1 = value; NotifyPropertyChanged(); }
        }

        private string _SHTC2 = string.Empty;
        public string SHTC2
        {
            get { return _SHTC2; }
            set { _SHTC2 = value; NotifyPropertyChanged(); }
        }

        private string _SHTC_PIC1 = string.Empty;
        public string SHTC_PIC1
        {
            get { return _SHTC_PIC1; }
            set { _SHTC_PIC1 = value; NotifyPropertyChanged(); }
        }

        private string _SHTC_PIC2 = string.Empty;
        public string SHTC_PIC2
        {
            get { return _SHTC_PIC2; }
            set { _SHTC_PIC2 = value; NotifyPropertyChanged(); }
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

        private string _STAGE_TYP = string.Empty;
        public string STAGE_TYP
        {
            get { return _STAGE_TYP; }
            set { _STAGE_TYP = value; NotifyPropertyChanged(); }
        }

        private string _REVERSE_FLG_DISP = string.Empty;
        public string REVERSE_FLG_DISP
        {
            get { return _REVERSE_FLG_DISP; }
            set { _REVERSE_FLG_DISP = value; NotifyPropertyChanged(); }
        }

        private string _STAGE_TYP_DISP = string.Empty;
        public string STAGE_TYP_DISP
        {
            get { return _STAGE_TYP_DISP; }
            set { _STAGE_TYP_DISP = value; NotifyPropertyChanged(); }
        }
    }
}
