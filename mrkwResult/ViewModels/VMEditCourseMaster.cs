using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Interop;
using Models;
using mrkwResult.Common;
using mrkwResult.Models.DBInfo;
using Oracle.ManagedDataAccess.Client;
using ViewModels;
using Views;

namespace Services
{
    public class VMEditCourseMaster : ViewModelBase
    {
        public VMEditCourseMaster(CommonInstance comIns)
        {
            ComIns = comIns;
            req = new RequestToDB();
        }

        #region プロパティ

        public RequestToDB req;
        public CommonInstance ComIns;

        private ObservableCollection<M_COURSE> _obcCourse = new ObservableCollection<M_COURSE>();
        public ObservableCollection<M_COURSE> obcCourse
        {
            get { return _obcCourse; }
            set { _obcCourse = value; NotifyPropertyChanged(); }
        }
        private M_COURSE? _SelectedCourse;
        public M_COURSE? SelectedCourse
        {
            get { return _SelectedCourse; }
            set { _SelectedCourse = value; NotifyPropertyChanged(); }
        }

        private M_COURSE? _CourseInfo;
        public M_COURSE? CourseInfo
        {
            get { return _CourseInfo; }
            set { _CourseInfo = value; NotifyPropertyChanged(); }
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
                obcCourse = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList);
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
                if (_SelectedCourse == null)
                {
                    msg = "コースが選択されていません。";
                }
                else
                {
                    CourseInfo = await req.GetCourseInfoAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseInfo, _SelectedCourse.COURSE_CD);

                    if (CourseInfo == null)
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
                if (_SelectedCourse == null)
                {
                    msg = "コースを選択してください。";
                }
                else
                {
                    if (_CourseInfo == null)
                    {
                        msg = "更新対象が選択されていません。コースの検索を行ってください。";
                    }
                    else
                    {
                        ret = await req.UpdateCourseMasterAsync(ComIns.ConnStr, ConstItems.PKG_UpdateCourseMaster, _CourseInfo);
                        if (ret)
                        {
                            msg = "コースマスタの更新が完了しました。";
                        }
                        else
                        {
                            msg = "コースマスタの更新に失敗しました。";
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