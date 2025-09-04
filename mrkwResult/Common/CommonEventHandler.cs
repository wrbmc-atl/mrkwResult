using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Models
{
    public static class CommonEventHandler
    {

        public static bool IsTextAllowed_NaturalNumber(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static long NumericOperation(KeyEventArgs e, long ret, bool enableLR, long unitVal, long? minVal, long? maxVal)
        {
            // 矢印キー以外はそのまま返す
            if(e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Left && e.Key != Key.Right) { return ret; }
            // LRキーにおける変化量
            long operationLR = enableLR ? 10 * unitVal : unitVal;
            // 最大値が指定されている場合
            if(maxVal != null)
            {
                if(ret > maxVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Up:
                        ret = Math.Min(ret + unitVal, maxVal.Value);
                        break;
                    case Key.Right:
                        ret = (long)Math.Min(ret + operationLR, maxVal.Value);
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        ret = ret + unitVal;
                        break;
                    case Key.Right:
                        ret = ret + operationLR;
                        break;
                }
            }
            // 最小値が指定されている場合
            if (minVal != null)
            {
                if (ret < minVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Down:
                        ret = Math.Max(ret - unitVal, minVal.Value);
                        break;
                    case Key.Left:
                        ret = Math.Max(ret - operationLR, minVal.Value);
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Down:
                        ret = ret + unitVal;
                        break;
                    case Key.Left:
                        ret = ret + operationLR;
                        break;
                }
            }
                return ret;
        }
        public static decimal DecimalOperation(KeyEventArgs e, decimal ret, bool enableLR, decimal unitVal, decimal? minVal, decimal? maxVal)
        {
            // 矢印キー以外はそのまま返す
            if(e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Left && e.Key != Key.Right) { return ret; }
            // LRキーにおける変化量
            decimal operationLR = enableLR ? 10 * unitVal : unitVal;
            // 最大値が指定されている場合
            if(maxVal != null)
            {
                if(ret > maxVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Up:
                        ret = Math.Min(ret + unitVal, maxVal.Value);
                        break;
                    case Key.Right:
                        ret = (decimal)Math.Min(ret + operationLR, maxVal.Value);
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        ret = ret + unitVal;
                        break;
                    case Key.Right:
                        ret = ret + operationLR;
                        break;
                }
            }
            // 最小値が指定されている場合
            if (minVal != null)
            {
                if (ret < minVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Down:
                        ret = Math.Max(ret - unitVal, minVal.Value);
                        break;
                    case Key.Left:
                        ret = Math.Max(ret - operationLR, minVal.Value);
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Down:
                        ret = ret + unitVal;
                        break;
                    case Key.Left:
                        ret = ret + operationLR;
                        break;
                }
            }
                return ret;
        }
        public static DateTime MonthOperation(KeyEventArgs e, DateTime ret, bool enableLR, double unitVal, DateTime? minVal, DateTime? maxVal)
        {
            // 矢印キー以外はそのまま返す
            if(e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Left && e.Key != Key.Right) { return ret; }
            // 最大値が指定されている場合
            if(maxVal != null)
            {
                if(ret > maxVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Up:
                        ret = maxVal.HasValue ? (ret.AddMonths((int)unitVal) < maxVal.Value ? ret.AddMonths((int)unitVal) : maxVal.Value) : ret.AddMonths((int)unitVal);
                        break;
                    case Key.Right:
                        if (enableLR)
                        {
                            ret = maxVal.HasValue ? (ret.AddYears((int)unitVal) < maxVal.Value ? ret.AddYears((int)unitVal) : maxVal.Value) : ret.AddYears((int)unitVal);
                        }
                        else
                        {
                            ret = maxVal.HasValue ? (ret.AddMonths((int)unitVal) < maxVal.Value ? ret.AddMonths((int)unitVal) : maxVal.Value) : ret.AddMonths((int)unitVal);
                        }
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        ret = ret.AddMonths((int)unitVal);
                        break;
                    case Key.Right:
                        if (enableLR)
                        {
                            ret = ret.AddYears((int)unitVal);
                        }
                        else
                        {
                            ret = ret.AddMonths((int)unitVal);
                        }
                        break;
                }
            }
            // 最小値が指定されている場合
            if (minVal != null)
            {
                if (ret < minVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Down:
                        ret = minVal.HasValue ? (ret.AddMonths(-(int)unitVal) > minVal.Value ? ret.AddMonths(-(int)unitVal) : minVal.Value) : ret.AddMonths(-(int)unitVal);
                        break;
                    case Key.Left:
                        if (enableLR)
                        {
                            ret = minVal.HasValue ? (ret.AddYears(-(int)unitVal) > minVal.Value ? ret.AddYears(-(int)unitVal) : minVal.Value) : ret.AddYears(-(int)unitVal);
                        }
                        else
                        {
                            ret = minVal.HasValue ? (ret.AddMonths(-(int)unitVal) > minVal.Value ? ret.AddMonths(-(int)unitVal) : minVal.Value) : ret.AddMonths(-(int)unitVal);
                        }
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Down:
                        ret = ret.AddMonths(-(int)unitVal);
                        break;
                    case Key.Left:
                        if (enableLR)
                        {
                            ret = ret.AddYears(-(int)unitVal);
                        }
                        else
                        {
                            ret = ret.AddMonths(-(int)unitVal);
                        }
                        break;
                }
            }
            return ret;
        }
        public static DateTime DayOperation(KeyEventArgs e, DateTime ret, bool enableLR, double unitVal, DateTime? minVal, DateTime? maxVal)
        {
            // 矢印キー以外はそのまま返す
            if(e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Left && e.Key != Key.Right) { return ret; }
            // 最大値が指定されている場合
            if(maxVal != null)
            {
                if(ret > maxVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Up:
                        ret = maxVal.HasValue ? (ret.AddDays(unitVal) < maxVal.Value ? ret.AddDays(unitVal) : maxVal.Value) : ret.AddDays(unitVal);
                        break;
                    case Key.Right:
                        if (enableLR)
                        {
                            ret = maxVal.HasValue ? (ret.AddMonths((int)unitVal) < maxVal.Value ? ret.AddMonths((int)unitVal) : maxVal.Value) : ret.AddMonths((int)unitVal);
                        }
                        else
                        {
                            ret = maxVal.HasValue ? (ret.AddDays(unitVal) < maxVal.Value ? ret.AddDays(unitVal) : maxVal.Value) : ret.AddDays(unitVal);
                        }
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        ret = ret.AddDays(unitVal);
                        break;
                    case Key.Right:
                        if (enableLR)
                        {
                            ret = ret.AddMonths((int)unitVal);
                        }
                        else
                        {
                            ret = ret.AddDays(unitVal);
                        }
                        break;
                }
            }
            // 最小値が指定されている場合
            if (minVal != null)
            {
                if (ret < minVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Down:
                        ret = minVal.HasValue ? (ret.AddDays(-unitVal) > minVal.Value ? ret.AddDays(-unitVal) : minVal.Value) : ret.AddDays(-unitVal);
                        break;
                    case Key.Left:
                        if (enableLR)
                        {
                            ret = minVal.HasValue ? (ret.AddMonths(-(int)unitVal) > minVal.Value ? ret.AddMonths(-(int)unitVal) : minVal.Value) : ret.AddMonths(-(int)unitVal);
                        }
                        else
                        {
                            ret = minVal.HasValue ? (ret.AddDays(-unitVal) > minVal.Value ? ret.AddDays(-unitVal) : minVal.Value) : ret.AddDays(-unitVal);
                        }
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Down:
                        ret = ret.AddDays(-unitVal);
                        break;
                    case Key.Left:
                        if (enableLR)
                        {
                            ret = ret.AddMonths(-(int)unitVal);
                        }
                        else
                        {
                            ret = ret.AddDays(-unitVal);
                        }
                        break;
                }
            }
            return ret;
        }
        public static DateTime SecondOperation(KeyEventArgs e, DateTime ret, bool enableLR, double unitVal, DateTime? minVal, DateTime? maxVal)
        {
            // 矢印キー以外はそのまま返す
            if(e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Left && e.Key != Key.Right) { return ret; }
            // 最大値が指定されている場合
            if(maxVal != null)
            {
                if(ret > maxVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Up:
                        ret = maxVal.HasValue ? (ret.AddSeconds(unitVal) < maxVal.Value ? ret.AddSeconds(unitVal) : maxVal.Value) : ret.AddSeconds(unitVal);
                        break;
                    case Key.Right:
                        if (enableLR)
                        {
                            ret = maxVal.HasValue ? (ret.AddMinutes(unitVal) < maxVal.Value ? ret.AddMinutes(unitVal) : maxVal.Value) : ret.AddMinutes(unitVal);
                        }
                        else
                        {
                            ret = maxVal.HasValue ? (ret.AddSeconds(unitVal) < maxVal.Value ? ret.AddSeconds(unitVal) : maxVal.Value) : ret.AddSeconds(unitVal);
                        }
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Up:
                        ret = ret.AddSeconds(unitVal);
                        break;
                    case Key.Right:
                        if (enableLR)
                        {
                            ret = ret.AddMinutes(unitVal);
                        }
                        else
                        {
                            ret = ret.AddSeconds(unitVal);
                        }
                        break;
                }
            }
            // 最小値が指定されている場合
            DateTime zero = new DateTime(1, 1, 1, 0, 0, 0);
            DateTime one = new DateTime(1, 1, 1, 0, 1, 0);
            if (minVal != null)
            {
                if (ret < minVal)
                {
                    return ret;
                }
                switch (e.Key)
                {
                    case Key.Down:
                        ret = minVal.HasValue ? (ret == zero ? zero : ret.AddSeconds(-unitVal) > minVal.Value ? ret.AddSeconds(-unitVal) : minVal.Value) : (ret.AddSeconds(-unitVal) < zero ? zero : ret.AddSeconds(-unitVal));
                        break;
                    case Key.Left:
                        if (enableLR)
                        {
                            if (ret <= one)
                            {
                                ret = zero;
                            }
                            else
                            {
                                ret = minVal.HasValue ? (ret.AddMinutes(-unitVal) > minVal.Value ? ret.AddMinutes(-unitVal) : minVal.Value) : ret.AddMinutes(-unitVal);
                            }
                        }
                        else
                        {
                            ret = minVal.HasValue ? (ret == zero ? zero : ret.AddSeconds(-unitVal) > minVal.Value ? ret.AddSeconds(-unitVal) : minVal.Value) : (ret.AddSeconds(-unitVal) < zero ? zero : ret.AddSeconds(-unitVal));
                        }
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Down:
                        ret = ret == zero ? zero : ret.AddSeconds(-unitVal);
                        break;
                    case Key.Left:
                        if (enableLR)
                        {
                            if (ret <= one)
                            {
                                ret = zero;
                            }
                            else
                            {
                                ret = ret.AddMinutes(-unitVal);
                            }
                        }
                        else
                        {
                            ret = ret == zero ? zero : ret.AddSeconds(-unitVal);
                        }
                        break;
                }
            }
            return ret;
        }
        public static bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }
        public static bool IsTextNumericOrDecimal(string input, string currentText)
        {
            // 入力が数字なら true を返す
            if (int.TryParse(input, out _))
            {
                return true;
            }

            // 入力が小数点で、かつ現在のテキストに小数点が含まれていない場合のみ true を返す
            if (input == "." && !currentText.Contains("."))
            {
                return true;
            }

            // それ以外は false
            return false;
        }
        #region TextBoxの入力規則
        public static void CheckInput(object sender, InputEventArgs e)
        {
            if (e is TextCompositionEventArgs textCompositionEventArgs)
            {
                TextBox textBox = sender as TextBox;

                // ここでどの検証メソッドを呼び出すかを決定
                if (textBox.Tag?.ToString() == "Decimal")
                {
                    e.Handled = !IsValidDecimalInput(textBox, textCompositionEventArgs.Text);
                }
                else if (textBox.Tag?.ToString() == "Integer")
                {
                    e.Handled = !IsValidIntegerInput(textBox, textCompositionEventArgs.Text);
                }
            }
            else if (e is KeyEventArgs keyEventArgs)
            {
                // BackspaceやDeleteなどの特定のキーは常に許可
                if (keyEventArgs.Key == Key.Back || keyEventArgs.Key == Key.Delete || keyEventArgs.Key == Key.Tab)
                {
                    e.Handled = false;
                }
            }
        }

        public static bool IsValidIntegerInput(TextBox textBox, string input)
        {
            Regex regex = new Regex(@"^\d*$");
            string newText = textBox.Text.Insert(textBox.SelectionStart, input);
            return regex.IsMatch(newText);
        }

        public static bool IsValidDecimalInput(TextBox textBox, string input)
        {
            Regex regex = new Regex(@"^(\d+(\.\d*)?|\.\d+)$");
            string newText = textBox.Text.Insert(textBox.SelectionStart, input);
            return regex.IsMatch(newText);
        }
        #endregion

        #region エンターキー押下時のフォーカス遷移
        public static void FocusTransfer(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIElement nextControl = null;

                // senderの型に応じて次のコントロールを取得
                switch (sender)
                {
                    case TextBox textBox:
                        nextControl = FocusHelper.GetNextControl(textBox);
                        break;
                    case ComboBox comboBox:
                        nextControl = FocusHelper.GetNextControl(comboBox);
                        break;
                    case CheckBox checkBox:
                        nextControl = FocusHelper.GetNextControl(checkBox);
                        break;
                    case RadioButton radioButton:
                        nextControl = FocusHelper.GetNextControl(radioButton);
                        break;
                    // 追加のコントロールがあればここにケースを追加
                    default:
                        break;
                }

                // 次のコントロールが存在する場合、フォーカスを移動
                if (nextControl != null)
                {
                    nextControl.Focus();
                    e.Handled = true; // イベントの処理を完了とする
                }
            }
        }
        #endregion

        #region フォーカス遷移時に全選択
        public static void SelectAll(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
        #endregion
    }
}
