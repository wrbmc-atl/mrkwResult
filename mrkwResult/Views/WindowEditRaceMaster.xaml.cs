using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Models;
using Services;
using mrkwResult.Models.DBInfo;
using mrkwResult.Models;

namespace Views
{
    /// <summary>
    /// WindowEditRaceMaster.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowEditRaceMaster : Window
    {
        private VMEditRaceMaster vm;

        public WindowEditRaceMaster()
        {
            InitializeComponent();
            vm = new VMEditRaceMaster();
            this.DataContext = vm;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool res = await vm.Init();
            if (!res)
            {
                MessageBox.Show(ConstItems.InitError, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cmbStart_Search.Focus();
            cmbStart_Search.SelectedIndex = 0;
            cmbGoal_Search.SelectedIndex = 0;
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

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("登録しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                Dictionary<bool, string> res = await vm.Update();
                if (res.ContainsKey(false))
                {
                    MessageBox.Show(ConstItems.UpdateError + res[false], "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ConstItems.UpdateComp + res[true], "メッセージ", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
            }
        }
    }
}