using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace mrkwResult
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region 共通イベントハンドラ
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CommonEventHandler.SelectAll(sender, e);
        }
        private void TextBox_PreviewInput(object sender, InputEventArgs e)
        {
            CommonEventHandler.CheckInput(sender, e);
        }
        private void FocusTransfer(object sender, KeyEventArgs e)
        {
            CommonEventHandler.FocusTransfer(sender, e);
        }
        #endregion
    }

}
