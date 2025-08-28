using Models;
using mrkwResult.Common;
using mrkwResult.Models.DBInfo;
using System.Collections.ObjectModel;
using ViewModels;

namespace Services
{
    public class VMEditRaceMaster : ViewModelBase
    {
        public VMEditRaceMaster(CommonInstance comIns)
        {
            ComIns = comIns;
            req = new RequestToDB();
        }

        #region プロパティ

        public RequestToDB req;
        public CommonInstance ComIns;

        private ObservableCollection<M_COURSE> _obcStartCourse = new ObservableCollection<M_COURSE>();
        public ObservableCollection<M_COURSE> obcStartCourse
        {
            get { return _obcStartCourse; }
            set { _obcStartCourse = value; NotifyPropertyChanged(); }
        }
        private M_COURSE? _SelectedStartCourse;
        public M_COURSE? SelectedStartCourse
        {
            get { return _SelectedStartCourse; }
            set { _SelectedStartCourse = value; NotifyPropertyChanged(); }
        }
        
        private ObservableCollection<M_COURSE> _obcGoalCourse = new ObservableCollection<M_COURSE>();
        public ObservableCollection<M_COURSE> obcGoalCourse
        {
            get { return _obcGoalCourse; }
            set { _obcGoalCourse = value; NotifyPropertyChanged(); }
        }
        private M_COURSE? _SelectedGoalCourse;
        public M_COURSE? SelectedGoalCourse
        {
            get { return _SelectedGoalCourse; }
            set { _SelectedGoalCourse = value; NotifyPropertyChanged(); }
        }
                
        private M_RACE? _RaceInfo;
        public M_RACE? RaceInfo
        {
            get { return _RaceInfo; }
            set { _RaceInfo = value; NotifyPropertyChanged(); }
        }

        #endregion

        public async Task<bool> Init()
        {
            try
            {
                bool ret = false;
                await SetInitialize();
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<bool> SetInitialize()
        {
            try
            {
                bool ret = false;
                obcStartCourse = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList);
                obcGoalCourse = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList);
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<Dictionary<bool, string>> Search()
        {
            try
            {
                bool ret = false;
                string msg = string.Empty;
                Dictionary<bool, string> dic = new Dictionary<bool, string>();

                if (_SelectedStartCourse == null || _SelectedGoalCourse == null)
                {
                    msg = "コースが選択されていません。";
                }
                else
                {
                    RaceInfo = await req.GetRaceInfoAsync(ComIns.ConnStr, ConstItems.PKG_GetRaceInfo, _SelectedStartCourse.COURSE_CD, _SelectedGoalCourse.COURSE_CD);

                    if (RaceInfo == null)
                    {
                        msg = "検索結果が0件でした。";
                    }
                    else
                    {
                        ret = true;
                    }
                }
                dic.Add(ret, msg);
                return dic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<Dictionary<bool, string>> Update()
        {
            try
            {
                bool ret = false;
                string msg = string.Empty;
                Dictionary<bool, string> dic = new Dictionary<bool, string>();

                if (_SelectedStartCourse == null || _SelectedGoalCourse == null)
                {
                    msg = "コースを選択してください。";
                }
                else
                {
                    ret = await req.UpdateRaceMasterAsync(ComIns.ConnStr, ConstItems.PKG_UpdateRaceMaster, _RaceInfo);

                    if (ret)
                    {
                        msg = "レースマスタの更新が完了しました。";
                    }
                    else
                    {
                        msg = "レースマスタの更新に失敗しました。";
                    }
                }

                dic.Add(ret, msg);
                return dic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}