using Models;
using mrkwResult.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using mrkwResult.Models.DBInfo;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Interop;

namespace ViewModels
{
    /// <summary>
    /// 実績見える化画面のViewModel
    /// </summary>
    public class VMGrandPrixResultHistory : ViewModelBase
    {
        public VMGrandPrixResultHistory(CommonInstance comIns)
        {
            ComIns = comIns;
            req = new RequestToDB();
            codeList = new M_CODE();
            obcRaceKbnList = new ObservableCollection<M_CODE>();
            obcStageTypeList = new ObservableCollection<M_CODE>();
            obcStartCourseList = new ObservableCollection<M_COURSE>();
            obcGoalCourseList = new ObservableCollection<M_COURSE>();
            obcRaceJsskResultList = new ObservableCollection<T_RACEJSSK>();
            StartDate = DateTime.Today.AddMonths(-1);
            EndDate = DateTime.Today;
        }

        #region Properties

        public RequestToDB req;
        public M_CODE codeList;
        public CommonInstance ComIns;

        private ObservableCollection<M_CODE> _obcRaceKbnList = new ObservableCollection<M_CODE>();
        public ObservableCollection<M_CODE> obcRaceKbnList
        {
            get { return _obcRaceKbnList; }
            set { _obcRaceKbnList = value; NotifyPropertyChanged(); }
        }

        private M_CODE? _SelectedRaceKbn;
        public M_CODE? SelectedRaceKbn
        {
            get { return _SelectedRaceKbn; }
            set { _SelectedRaceKbn = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<M_COURSE> _obcStartCourseList = new ObservableCollection<M_COURSE>();
        public ObservableCollection<M_COURSE> obcStartCourseList
        {
            get { return _obcStartCourseList; }
            set { _obcStartCourseList = value; NotifyPropertyChanged(); }
        }
        private M_COURSE? _SelectedStartCourse;
        public M_COURSE? SelectedStartCourse
        {
            get { return _SelectedStartCourse; }
            set { _SelectedStartCourse = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<M_COURSE> _obcGoalCourseList = new ObservableCollection<M_COURSE>();
        public ObservableCollection<M_COURSE> obcGoalCourseList
        {
            get { return _obcGoalCourseList; }
            set { _obcGoalCourseList = value; NotifyPropertyChanged(); }
        }
        private M_COURSE? _SelectedGoalCourse;
        public M_COURSE? SelectedGoalCourse
        {
            get { return _SelectedGoalCourse; }
            set { _SelectedGoalCourse = value; NotifyPropertyChanged(); }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; NotifyPropertyChanged(); }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<M_CODE> _obcStageTypeList = new ObservableCollection<M_CODE>();
        public ObservableCollection<M_CODE> obcStageTypeList
        {
            get { return _obcStageTypeList; }
            set { _obcStageTypeList = value; NotifyPropertyChanged(); }
        }

        private M_CODE? _SelectedStageType;
        public M_CODE? SelectedStageType
        {
            get { return _SelectedStageType; }
            set { _SelectedStageType = value; NotifyPropertyChanged(); }
        }

        private long _raceJsskCount = 0;
        public long raceJsskCount
        {
            get { return _raceJsskCount; }
            set { _raceJsskCount = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<T_RACEJSSK> _obcRaceJsskResultList;
        public ObservableCollection<T_RACEJSSK> obcRaceJsskResultList
        {
            get { return _obcRaceJsskResultList; }
            set 
            { 
                _obcRaceJsskResultList = value; 
                NotifyPropertyChanged();
                raceJsskCount = obcRaceJsskResultList.Count;
            }
        }

        #endregion

        #region Commands

        #endregion

        #region Methods

        public async Task<bool> Init()
        {
            try
            {
                bool ret = false;
                await SetInitialize();
                ret = true;
                return ret;
            }
            catch
            {
                return false;
            }
        }

        internal async Task<bool> SetInitialize()
        {
            try
            {
                bool ret = false;

                obcRaceKbnList = await req.GetCodeListAsync(ComIns.ConnStr, ConstItems.PKG_GetCode1List, "RACE_KBN", true);
                obcStageTypeList = await req.GetCodeListAsync(ComIns.ConnStr, ConstItems.PKG_GetCode1List, "STAGE_TYP", true);

                obcStartCourseList = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList, true);
                obcGoalCourseList = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList, true);

                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal async Task<Dictionary<bool, string>> Search()
        {
            bool ret = false;
            string msg = string.Empty;
            Dictionary<bool, string> dic = new Dictionary<bool, string>();
            try
            {
                // バリデーション：検索条件が1つも入力されていない場合はエラー
                if (SelectedStartCourse == null && SelectedGoalCourse == null && StartDate == null && EndDate == null && SelectedRaceKbn == null && SelectedStageType == null)
                {
                    msg = "検索条件を最低1つ入力してください。";
                }
                else
                {
                    // PKG_GRN_007を呼び出す
                    obcRaceJsskResultList.Clear();
                    obcRaceJsskResultList = await req.GetRaceJsskListAsync(ComIns.ConnStr, ConstItems.PKG_GetRacejsskInfo, SelectedRaceKbn?.CODE2, SelectedStartCourse?.COURSE_CD, SelectedGoalCourse?.COURSE_CD, StartDate?.ToString("yyyy/MM/dd"), EndDate?.ToString("yyyy/MM/dd"), SelectedStageType?.CODE2);

                    if (obcRaceJsskResultList == null)
                    {
                        msg = "検索に失敗しました。";
                        return dic;
                    }
                    else if (obcRaceJsskResultList.Count == 0)
                    {
                        msg = "\n検索結果が0件です。";
                        return dic;
                    }
                    else
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ret = false;
                msg = ex.Message;
            }
            finally
            {
                dic.Add(ret, msg);
            }
            return dic;
        }

        #endregion
    }
}
