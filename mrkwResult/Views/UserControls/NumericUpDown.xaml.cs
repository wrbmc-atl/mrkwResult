using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Views.UserControls
{
    // 文字列とNull許容intを変換するコンバータ
    public class IntNullableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && int.TryParse(stringValue, out int intValue))
            {
                return intValue;
            }
            return null;
        }
    }

    /// <summary>
    /// NumericUpDown.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int?), typeof(NumericUpDown), new PropertyMetadata(null));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MinValue));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MaxValue));

        public int? Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public NumericUpDown()
        {
            InitializeComponent();

            // テキストボックスの入力検証を追加
            valueTextBox.PreviewTextInput += (s, e) =>
            {
                // 数字とマイナス記号のみ許可
                bool isNumber = int.TryParse(e.Text, out _);
                bool isMinus = e.Text == "-" && valueTextBox.SelectionStart == 0 && !valueTextBox.Text.Contains("-");
                e.Handled = !(isNumber || isMinus);
            };
        }

        // キーボード操作 (上/下/左/右キー)
        private void ValueTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                // 上キー: +1
                ChangeValue(1);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                // 下キー: -1
                ChangeValue(-1);
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                // 左キー: -10
                ChangeValue(-10);
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                // 右キー: +10
                ChangeValue(10);
                e.Handled = true;
            }
        }

        // 値を増減させるヘルパーメソッド
        private void ChangeValue(int delta)
        {
            int currentValue = Value ?? Minimum;
            int newValue = currentValue + delta;

            // MinimumとMaximumの範囲内に収める
            if (newValue >= Minimum && newValue <= Maximum)
            {
                Value = newValue;
            }
            else
            {
                Value = Math.Max(Minimum, Math.Min(Maximum, newValue));
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeValue(1);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeValue(-1);
        }
    }
}
