using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Views.UserControls
{
    /// <summary>
    /// Interaction logic for DateUpDown.xaml
    /// </summary>
    public partial class DateUpDown : UserControl
    {
        // 依存関係プロパティとして日付の値を定義
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime), typeof(DateUpDown), new PropertyMetadata(DateTime.Now));

        public DateTime Value
        {
            get { return (DateTime)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public DateUpDown()
        {
            InitializeComponent();
        }

        // 上ボタンクリック時の処理（+1日）
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Value = Value.AddDays(1);
        }

        // 下ボタンクリック時の処理（-1日）
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            Value = Value.AddDays(-1);
        }

        // テキストボックスへの入力制限（数字とスラッシュのみ許可）
        private void ValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9/]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // キーボード操作の処理
        private void ValueTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Value = Value.AddDays(1);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                Value = Value.AddDays(-1);
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                Value = Value.AddMonths(-1);
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                Value = Value.AddMonths(1);
                e.Handled = true;
            }
        }
    }

    // DateTimeをstringに変換するコンバーター
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("yyyy/MM/dd");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string dateString)
            {
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    return date;
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
