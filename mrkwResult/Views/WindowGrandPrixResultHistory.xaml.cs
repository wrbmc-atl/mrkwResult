using Models;
using mrkwResult.Models.DBInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;

namespace Views
{
    /// <summary>
    /// WindowGrandPrixResultHistory.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowGrandPrixResultHistory : Window
    {
        VMGrandPrixResultHistory vm;
        public WindowGrandPrixResultHistory(CommonInstance comIns)
        {
            InitializeComponent();
            vm = new VMGrandPrixResultHistory(comIns);
            this.DataContext = vm;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool res = await vm.Init();
            if (!res)
            {
                MessageBox.Show(ConstItems.InitError, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cmbRaceKbn.Focus();
            cmbRaceKbn.SelectedIndex = 0;
            cmbStageType.SelectedIndex = 0;
            cmbStartCourse.SelectedIndex = 0;
            cmbGoalCourse.SelectedIndex = 0;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
            }
        }

        private async void Click_Search(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<bool, string> res = await vm.Search();
                if (res.ContainsKey(false))
                {
                    MessageBox.Show(ConstItems.SearchError + res[false], "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
            }
        }
    }
}
