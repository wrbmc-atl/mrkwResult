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
            JissekiInfo.RACE_KBN = "GRN";
            JissekiInfo.RANK = 0;
            JissekiInfo.HEADCOUNT = 0;
            JissekiInfo.RATE_END = 8000;
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

        // --- 画像の存在有無をBindingするための新しいプロパティ ---
        private bool _isPic1Available;
        public bool IsPic1Available
        {
            get { return _isPic1Available; }
            set { _isPic1Available = value; NotifyPropertyChanged(); }
        }

        private bool _isPic2Available;
        public bool IsPic2Available
        {
            get { return _isPic2Available; }
            set { _isPic2Available = value; NotifyPropertyChanged(); }
        }

        private bool _isPic3Available;
        public bool IsPic3Available
        {
            get { return _isPic3Available; }
            set { _isPic3Available = value; NotifyPropertyChanged(); }
        }
        // -----------------------------------------------------

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
                obcStartCourseList = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList, false);
                obcGoalCourseList = await req.GetCourseListAsync(ComIns.ConnStr, ConstItems.PKG_GetCourseList, false);
                obcRaceKbnList = await req.GetCodeListAsync(ComIns.ConnStr, ConstItems.PKG_GetCode1List, "RACE_KBN", false);
                ret = true;
                return ret;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// レース情報とコース情報を検索する。
        /// 検索成功後、関連する画像ファイルの存在を確認する。
        /// </summary>
        /// <returns></returns>
        internal async Task<Dictionary<bool, string>> Search()
        {
            bool ret = false;
            string msg = string.Empty;
            Dictionary<bool, string> dic = new Dictionary<bool, string>();
            try
            {
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
                        // 検索結果取得成功後、画像ファイルの存在を確認
                        string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                        IsPic1Available = !string.IsNullOrEmpty(ResultInfo?.SHTC_PIC1) && File.Exists(Path.Combine(projectDirectory, "Images", ResultInfo?.SHTC_PIC1));
                        IsPic2Available = !string.IsNullOrEmpty(ResultInfo?.SHTC_PIC2) && File.Exists(Path.Combine(projectDirectory, "Images", ResultInfo?.SHTC_PIC2));
                        IsPic3Available = !string.IsNullOrEmpty(CourseInfo?.SHTC_PIC) && File.Exists(Path.Combine(projectDirectory, "Images", CourseInfo?.SHTC_PIC));

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

        internal async Task<Dictionary<bool, string>> Insert()
        {
            bool ret = false;
            string msg = string.Empty;
            Dictionary<bool, string> dic = new Dictionary<bool, string>();
            try
            {
                // 入力値のバリデーション
                if (_SelectedStartCourse == null || _SelectedGoalCourse == null)
                {
                    msg = "出発地と目的地を選択してください。";
                }
                else
                {
                    if (_JissekiInfo == null || ResultInfo == null)
                    {
                        msg = "レース情報が選択されていません。コースの検索を行ってください。";
                    }
                    else
                    {
                        // バリデーションに成功した場合、JissekiInfoに選択されたステージ情報を設定
                        _JissekiInfo.START_CD = _SelectedStartCourse.COURSE_CD;
                        _JissekiInfo.GOAL_CD = _SelectedGoalCourse.COURSE_CD;
                        _JissekiInfo.STAGE_TYP = ResultInfo?.STAGE_TYP;
                        _JissekiInfo.HEADCOUNT = _JissekiInfo.HEADCOUNT == 0 ? null : _JissekiInfo.HEADCOUNT;
                        _JissekiInfo.RANK = _JissekiInfo.RANK == 0 ? null : _JissekiInfo.RANK;
                        _JissekiInfo.RATE_END = _JissekiInfo.RATE_END == 0 ? null : _JissekiInfo.RATE_END;

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
                // ここは画像ファイル名が存在しない場合なので、画像の存在有無チェックとは別に扱う
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
                    imageViewer.ShowDialog();
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