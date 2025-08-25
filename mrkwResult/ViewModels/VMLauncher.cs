using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.ComponentModel;
using static ViewModels.VMLauncher;
using Views;
using Models;
using System.IO;
using System.Windows.Media;
using System.Windows.Input;
using System.Data;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ViewModels
{
    public class VMLauncher: ViewModelBase
    {
        public VMLauncher()
        {

        }

        private string _version;
        public string version
        {
            get { return _version; }
            set { _version = value; NotifyPropertyChanged(); }
        }

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
                version = "Launcher_" + GetBuildDate().ToString("yy.M.d");
                
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 最終ビルド日取得
        /// </summary>
        /// <returns></returns>
        public static DateTime GetBuildDate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var location = assembly.Location;

            if (File.Exists(location))
            {
                return File.GetLastWriteTime(location); // アセンブリの最終更新日時
            }
            return DateTime.MinValue.Date;
        }

    }
}
