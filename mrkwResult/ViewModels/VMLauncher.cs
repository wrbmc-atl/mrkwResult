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
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ViewModels
{
    public class VMLauncher: ViewModelBase
    {
        public VMLauncher()
        {
            ComIns = new CommonInstance();
        }

        public CommonInstance ComIns { get; set; }

        private string _version;
        public string version
        {
            get { return _version; }
            set { _version = value; NotifyPropertyChanged(); }
        }
        
        private string _direction;
        public string direction
        {
            get { return _direction; }
            set { _direction = value; NotifyPropertyChanged(); }
        }

        public bool Init()
        {
            try
            {
                bool ret = false;
                SetInitialize();

                ret = true;
                return ret;
            }
            catch
            {
                return false;
            }
        }

        internal bool SetInitialize()
        {
            try
            {
                bool ret = false;
                GetConfigInfo();
                version = "Launcher_" + GetBuildDate().ToString("yy.M.d");
                version += direction == "dev" ? "_開発環境" : "_本番環境";

                ret = true;
                return ret;
            }
            catch
            {
                return false;
            }
        }

        internal void GetConfigInfo()
        {

            try
            {
                // 実行ファイルと同じディレクトリにあるConfig.xmlを読み込む
                string configFilePath = "Config.xml";
                XDocument doc = XDocument.Load(configFilePath);

                // <Direction>要素の値を取得
                direction = doc.Descendants("Direction").FirstOrDefault()?.Value;

                // 値が取得できたか確認し、switch文で分岐
                if (!string.IsNullOrEmpty(direction))
                {
                    switch (direction.ToLower()) // 小文字に変換して比較
                    {
                        case "dev":
                            ComIns.ConnStr = ConstItems.ConnStrDev;
                            break;
                        case "prod":
                            ComIns.ConnStr = ConstItems.ConnStrProd;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    // <Direction>要素が見つからない場合
                    Console.WriteLine("環境設定が見つかりません。");
                }
            }
            catch (Exception ex)
            {
                // XMLファイルの読み込みエラーなど
                Console.WriteLine($"設定ファイルの読み込み中にエラーが発生しました: {ex.Message}");
                return;
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

        public void ShowNewDialog(ConstItems.WindowType winTyp)
        {
            try
            {
                switch (winTyp)
                {
                    case ConstItems.WindowType.GrandPrix:
                        WindowGrandPrixResult frm1 = new WindowGrandPrixResult(ComIns);
                        bool? result1 = frm1.ShowDialog();
                        break;
                    case ConstItems.WindowType.EditCourse:
                        WindowEditCourseMaster frm2 = new WindowEditCourseMaster(ComIns);
                        bool? result2 = frm2.ShowDialog();
                        break;
                    case ConstItems.WindowType.EditRace:
                        WindowEditRaceMaster frm3 = new WindowEditRaceMaster(ComIns);
                        bool? result3 = frm3.ShowDialog();
                        break;
                    case ConstItems.WindowType.GrandPrixView:
                        WindowGrandPrixResultHistory frm4 = new WindowGrandPrixResultHistory(ComIns);
                        bool? result4 = frm4.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
