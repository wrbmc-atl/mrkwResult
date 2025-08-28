using System;
using System.Windows;
using ViewModels;
using Models;

namespace Views
{
    /// <summary>
    /// WindowLauncher.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowLauncher : Window
    {
        VMLauncher vm;
        public WindowLauncher()
        {
            InitializeComponent();
            vm = new VMLauncher();
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
                btnGrandPrixResult.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnGrandPrixResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.ShowNewDialog(ConstItems.WindowType.GrandPrix);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEditCourseMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.ShowNewDialog(ConstItems.WindowType.EditCourse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEditRaceMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.ShowNewDialog(ConstItems.WindowType.EditRace);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
