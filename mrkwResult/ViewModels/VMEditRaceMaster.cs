using DocumentFormat.OpenXml.Wordprocessing;
using Models;
using mrkwResult.Models;
using mrkwResult.Models.DBInfo;
using System.Collections.ObjectModel;
using System.Windows.Interop;
using ViewModels;

namespace Services
{
    public class VMEditRaceMaster : ViewModelBase
    {
        public VMEditRaceMaster()
        {
            req = new RequestToDB();
        }

        #region プロパティ

        public RequestToDB req;

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
                obcStartCourse = await req.GetCourseListAsync(CommonInstance.ConnStr, ConstItems.PKG_GetCourseList, false);
                obcGoalCourse = await req.GetCourseListAsync(CommonInstance.ConnStr, ConstItems.PKG_GetCourseList, false);
                ret = true;
                return ret;
            }
            catch
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
                if (_SelectedStartCourse == null || _SelectedGoalCourse == null)
                {
                    msg = "コースが選択されていません。";
                }
                else
                {
                    RaceInfo = await req.GetRaceInfoAsync(CommonInstance.ConnStr, ConstItems.PKG_GetRaceInfo, _SelectedStartCourse.COURSE_CD, _SelectedGoalCourse.COURSE_CD);

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
                ret = false;
                msg = ex.Message;
                dic.Add(ret, msg);
                return dic;
            }
        }

        internal async Task<Dictionary<bool, string>> Update()
        {
            bool ret = false;
            string msg = string.Empty;
            Dictionary<bool, string> dic = new Dictionary<bool, string>();
            try
            {
                if (_SelectedStartCourse == null || _SelectedGoalCourse == null)
                {
                    msg = "コースを選択してください。";
                }
                else
                {

                    if (_RaceInfo == null)
                    {
                        msg = "更新対象が選択されていません。コースの検索を行ってください。";
                    }
                    else
                    {
                        ret = await req.UpdateRaceMasterAsync(CommonInstance.ConnStr, ConstItems.PKG_UpdateRaceMaster, _RaceInfo);

                        if (ret)
                        {
                            msg = "レースマスタの更新が完了しました。";
                        }
                        else
                        {
                            msg = "レースマスタの更新に失敗しました。";
                        }
                    }
                }

                dic.Add(ret, msg);
                return dic;
            }
            catch (Exception ex)
            {
                ret = false;
                msg = ex.Message;
                dic.Add(ret, msg);
                return dic;
            }
        }

    }
}