using Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace mrkwResult
{
    public partial class App : Application
    {
        private void OnComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            // Prevent left and right arrow keys from changing the selected item
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                e.Handled = true;
            }
        }

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
