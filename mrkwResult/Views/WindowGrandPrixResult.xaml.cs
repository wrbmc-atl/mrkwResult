using Oracle.ManagedDataAccess.Client;
using ViewModels;
using Models;
using System.Windows;
using mrkwResult.Models.DBInfo;

namespace Views
{
    /// <summary>
    /// WindowGrandPrixResult.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowGrandPrixResult : Window
    {
        VMGrandPrixResult vm;
        public WindowGrandPrixResult(CommonInstance comIns)
        {
            InitializeComponent();
            vm = new VMGrandPrixResult(comIns);
            this.DataContext = vm;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bool res = await vm.Init();
                if (!res)
                {
                    MessageBox.Show(ConstItems.InitError, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                cmbStartStage.Focus();
                cmbStartStage.SelectedIndex = 0;
                cmbGoalStage.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelectionChanged_StartStage(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    vm.SelectedStartStageList = e.AddedItems[0] as M_STGList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
                throw ex;
            }
        }

        private void SelectionChanged_GoalStage(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    vm.SelectedGoalStageList = e.AddedItems[0] as M_STGList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
                throw ex;
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
                throw ex;
            }
        }
    }
}
