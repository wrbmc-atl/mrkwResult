using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Oracle.ManagedDataAccess.Client;
using mrkwResult.Common;
using mrkwResult.Models.DBInfo;
using Views;
using Models;

namespace ViewModels
{
    public class VMGrandPrixResult : ViewModelBase
    {
        public VMGrandPrixResult(CommonInstance comIns)
        {
            ComIns = comIns;
            req = new RequestToDB();
            JissekiInfo = new T_RACEJSSK();
            JissekiInfo.RACE_DATE = DateTime.Today;
        }

        #region プロパティ

        public RequestToDB req;
        public CommonInstance ComIns;

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

        private M_RACE? _ResultInfo;
        public M_RACE? ResultInfo
        {
            get { return _ResultInfo; }
            set { _ResultInfo = value; NotifyPropertyChanged(); }
        }

        private M_COURSE? _CourseInfo;
        public M_COURSE? CourseInfo
        {
            get { return _CourseInfo; }
            set { _CourseInfo = value; NotifyPropertyChanged(); }
        }

        private T_RACEJSSK _JissekiInfo;
        public T_RACEJSSK JissekiInfo
        {
            get { return _JissekiInfo; }
            set { _JissekiInfo = value; NotifyPropertyChanged(); }
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
                obcStartCourseList = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList);
                obcGoalCourseList = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList);
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

                if (_SelectedStartCourse == null)
                {
                    msg = "出発地が選択されていません。";
                }
                else if (_SelectedGoalCourse == null)
                {
                    msg = "目的地が選択されていません。";
                }
                else
                {
                    ResultInfo = await req.GetRaceInfoAsync(ComIns.ConnStr, ConstItems.PKG_GetRaceInfo, _SelectedStartCourse.COURSE_CD, _SelectedGoalCourse.COURSE_CD);
                    CourseInfo = await req.GetCourseInfoAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseInfo, _SelectedGoalCourse.COURSE_CD);

                    if (ResultInfo == null)
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

        internal async Task<Dictionary<bool, string>> Insert()
        {
            try
            {
                bool ret = false;
                string msg = string.Empty;
                Dictionary<bool, string> dic = new Dictionary<bool, string>();

                // 入力値のバリデーション
                if (_SelectedStartCourse == null || _SelectedGoalCourse == null)
                {
                    msg = "出発地と目的地を選択してください。";
                }
                else
                {
                    // バリデーションに成功した場合、JissekiInfoに選択されたステージ情報を設定
                    _JissekiInfo.START_CD = _SelectedStartCourse.COURSE_CD;
                    _JissekiInfo.GOAL_CD = _SelectedGoalCourse.COURSE_CD;
                    _JissekiInfo.STAGE_TYP = ResultInfo?.STAGE_TYP;

                    // 登録処理の実行
                    ret = await req.InsertRaceJsskAsync(ComIns.ConnStr, ConstItems.PKG_InsertRaceJssk, _JissekiInfo);

                    if (ret)
                    {
                        msg = "実績の登録が完了しました。";
                    }
                    else
                    {
                        msg = "実績の登録に失敗しました。";
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

        internal async Task<Dictionary<bool, string>> ShowPicture(string picNumber)
        {
            var dic = new Dictionary<bool, string>();
            string msg = string.Empty;

            if (ResultInfo == null && CourseInfo == null)
            {
                msg = "表示する画像情報がありません。";
                dic.Add(false, msg);
                return dic;
            }

            string imageFileName = string.Empty;
            switch (picNumber)
            {
                case "1":
                    imageFileName = ResultInfo?.SHTC_PIC1;
                    break;
                case "2":
                    imageFileName = ResultInfo?.SHTC_PIC2;
                    break;
                case "3":
                    imageFileName = CourseInfo?.SHTC_PIC;
                    break;
                default:
                    msg = "無効な画像番号です。";
                    dic.Add(false, msg);
                    return dic;
            }

            if (string.IsNullOrEmpty(imageFileName))
            {
                dic.Add(true, string.Empty);
                return dic;
            }

            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectDirectory, "Images", imageFileName);

            if (File.Exists(fullPath))
            {
                try
                {
                    var imageViewer = new WindowImageViewer(fullPath);
                    imageViewer.Show();
                    dic.Add(true, string.Empty);
                }
                catch (Exception ex)
                {
                    msg = $"画像の表示中にエラーが発生しました。\n詳細: {ex.Message}";
                    dic.Add(false, msg);
                }
            }
            else
            {
                msg = $"指定されたファイルが見つかりません: {imageFileName}";
                dic.Add(false, msg);
            }

            return dic;
        }
    }
}