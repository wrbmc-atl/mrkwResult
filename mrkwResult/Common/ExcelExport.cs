using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Models
{
    public static class ExcelExport
    {

        /// <summary>
        /// 指定されたObservableCollectionのデータをExcelファイルに出力します。
        /// </summary>
        /// <typeparam name="T">ObservableCollection内のデータ型。</typeparam>
        /// <param name="obcList">出力するデータを含むObservableCollection。</param>
        /// <param name="sheetName">Excelシートの名前（オプション、デフォルトは"Data"）。</param>
        /// <param name="fileNamePrefix">出力ファイル名のプレフィックス（オプション、デフォルトは"Export"）。</param>
        /// <returns>Excel出力が成功した場合はtrue、それ以外はfalse。</returns>
        public static bool OutputExcel<T>(ObservableCollection<T> obcList, string sheetName = "Data", string fileNamePrefix = "Export")
        {
            try
            {
                if (obcList == null || !obcList.Any())
                {
                    // データがない場合は false を返す
                    System.Diagnostics.Debug.WriteLine("出力するデータがありません。");
                    return false;
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(sheetName); // 引数で指定されたシート名を使用

                    // ヘッダーの書き出し
                    // T 型のパブリックプロパティを取得
                    PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    for (int i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = properties[i].Name;
                    }

                    // データ行の書き出し
                    int currentRow = 2; // ヘッダーの次の行から開始
                    foreach (var item in obcList)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            var property = properties[i];
                            object value = property.GetValue(item); // プロパティの値を取得

                            if (value == null)
                            {
                                // Nullable型の場合やDBNullの場合、string.Emptyを設定
                                worksheet.Cell(currentRow, i + 1).Value = string.Empty;
                            }
                            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                            {
                                DateTime dtValue = (DateTime)value;
                                worksheet.Cell(currentRow, i + 1).Value = dtValue;

                                // DateTime型の書式設定は、プロパティ名に依存しない汎用的なものか、
                                // 呼び出し元で指定させる方が良いですが、ここでは例として特定のプロパティ名に依存しないようにします。
                                // 必要に応じて、呼び出し元で特定のプロパティに対する書式をカスタマイズできるように拡張することも可能です。
                                // 時分秒が必要な場合は、ToString()で明示的に指定するか、全てのDateTime型に適用するかを検討
                                worksheet.Cell(currentRow, i + 1).Style.DateFormat.SetFormat("yyyy/MM/dd HH:mm:ss");
                            }
                            else if (property.PropertyType == typeof(TimeSpan) || property.PropertyType == typeof(TimeSpan?))
                            {
                                TimeSpan tsValue = (TimeSpan)value;
                                worksheet.Cell(currentRow, i + 1).Value = tsValue.ToString(@"hh\:mm\:ss");
                            }
                            else if (IsNumericType(property.PropertyType))
                            {
                                worksheet.Cell(currentRow, i + 1).Value = Convert.ToDouble(value);
                            }
                            else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                            {
                                // bool型はExcelでTRUE/FALSEとして表示される
                                worksheet.Cell(currentRow, i + 1).Value = (bool)value;
                            }
                            else
                            {
                                // その他の型はToString()で文字列として出力
                                worksheet.Cell(currentRow, i + 1).Value = value.ToString();
                            }
                        }
                        currentRow++;
                    }

                    // 列の幅を自動調整
                    worksheet.Columns().AdjustToContents();

                    // ファイルの保存場所とファイル名を指定
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    // 引数で指定されたプレフィックスを使用
                    string fileName = $"{fileNamePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    string filePath = Path.Combine(desktopPath, fileName);

                    workbook.SaveAs(filePath);

                    System.Diagnostics.Debug.WriteLine($"Excelファイルが {filePath} に出力されました。");
                    return true; // 成功
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Excel出力中にエラーが発生しました: {ex.Message}\n{ex.StackTrace}");
                // エラーが発生した場合は false を返す
                return false;
            }
        }
     
        /// <summary>
        /// 指定されたTypeが数値型であるかを判定します。
        /// Nullable型にも対応しています。
        /// </summary>
        /// <param name="type">判定する型</param>
        /// <returns>数値型であればtrue、そうでなければfalse。</returns>
        private static bool IsNumericType(Type type)
        {
            // Nullable<T> 型の場合、基になる型を取得
            Type actualType = Nullable.GetUnderlyingType(type) ?? type;

            switch (Type.GetTypeCode(actualType))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
