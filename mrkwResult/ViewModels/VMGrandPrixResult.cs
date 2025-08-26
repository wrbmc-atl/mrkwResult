using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.ComponentModel;
using Views;
using Models;
using System.IO;
using System.Windows.Media;
using System.Windows.Input;
using System.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using mrkwResult.Models.DBInfo;
using Oracle.ManagedDataAccess.Client;
using System.Windows;
using mrkwResult.Common;

namespace ViewModels
{
    public class VMGrandPrixResult : ViewModelBase
    {
        public VMGrandPrixResult(CommonInstance comIns)
        {
            ComIns = comIns;
            req = new RequestToDB();
        }

        #region プロパティ

        public RequestToDB req;
        public CommonInstance ComIns;

        private ObservableCollection<M_STGList> _obcStartStageList = new ObservableCollection<M_STGList>();
        public ObservableCollection<M_STGList> obcStartStageList
        {
            get { return _obcStartStageList; }
            set { _obcStartStageList = value; NotifyPropertyChanged(); }
        }
        private M_STGList? _SelectedStartStageList;
        public M_STGList? SelectedStartStageList
        {
            get { return _SelectedStartStageList; }
            set { _SelectedStartStageList = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<M_STGList> _obcGoalStageList = new ObservableCollection<M_STGList>();
        public ObservableCollection<M_STGList> obcGoalStageList
        {
            get { return _obcGoalStageList; }
            set { _obcGoalStageList = value; NotifyPropertyChanged(); }
        }
        private M_STGList? _SelectedGoalStageList;
        public M_STGList? SelectedGoalStageList
        {
            get { return _SelectedGoalStageList; }
            set { _SelectedGoalStageList = value; NotifyPropertyChanged(); }
        }

        private M_STG? _ResultInfo;
        public M_STG? ResultInfo
        {
            get { return _ResultInfo; }
            set { _ResultInfo = value; NotifyPropertyChanged(); }
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

                obcStartStageList = await req.GetStageListAsync(ComIns.ConnStr, ConstItems.PKG_GetStageList);
                obcGoalStageList = await req.GetStageListAsync(ComIns.ConnStr, ConstItems.PKG_GetStageList);

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

                if(_SelectedStartStageList == null)
                {
                    msg = "出発地が選択されていません。";
                }
                else if(_SelectedGoalStageList == null)
                {
                    msg = "目的地が選択されていません。";
                }
                else
                {
                    ResultInfo = await req.GetStageInfoAsync(ComIns.ConnStr, ConstItems.PKG_GetStageInfo, _SelectedStartStageList.STAGE_CD, _SelectedGoalStageList.STAGE_CD);
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

    }
}
