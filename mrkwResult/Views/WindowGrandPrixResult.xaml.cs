using Oracle.ManagedDataAccess.Client;
using ViewModels;
using Models;
using System.Windows;
using System.Windows.Input;
using mrkwResult.Models.DBInfo;
using System.Windows.Controls;

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
                cmbStartCourse.Focus();
                cmbStartCourse.SelectedIndex = 0;
                cmbGoalCourse.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
            }
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        private async void Click_pic1(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<bool, string> res = await vm.ShowPicture("1");
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

        private async void Click_pic2(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<bool, string> res = await vm.ShowPicture("2");
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

        private async void Click_pic3(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<bool, string> res = await vm.ShowPicture("3");
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

        private async void Click_Register(object sender, RoutedEventArgs e)
        {
            // 登録確認メッセージボックスを表示
            MessageBoxResult result = MessageBox.Show("登録しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                Dictionary<bool, string> res = await vm.Insert();
                if (res.ContainsKey(false))
                {
                    MessageBox.Show(ConstItems.InsertError + res[false], "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ConstItems.InsertComp + res[true], "メッセージ", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK);
            }
        }

        private void HeadCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!CommonEventHandler.IsNumericTextAllowed(textBox.Text, e.Text, false, false, 1, 24))
            {
                e.Handled = true;
            }
        }

        private void Rank_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!CommonEventHandler.IsNumericTextAllowed(textBox.Text, e.Text, false, false, 1, 24))
            {
                e.Handled = true;
            }
        }

        private void RateEnd_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!CommonEventHandler.IsNumericTextAllowed(textBox.Text, e.Text, false, false, null, null))
            {
                e.Handled = true;
            }
        }
    }
}
