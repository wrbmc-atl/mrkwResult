using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Views
{
    /// <summary>
    /// WindowImageViewer.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowImageViewer : Window
    {
        public WindowImageViewer(string imagePath)
        {
            InitializeComponent();

            // ファイルが存在するか確認
            if (!File.Exists(imagePath))
            {
                MessageBox.Show("指定された画像ファイルが見つかりません。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 画像ファイルからBitmapImageを作成
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // ファイルロックを防ぐ
                bitmap.EndInit();

                // ImageコントロールのSourceに設定
                ImageViewer.Source = bitmap;

                // ウィンドウタイトルをファイル名に設定
                this.Title = Path.GetFileName(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"画像の読み込み中にエラーが発生しました。\n詳細: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
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
                throw ex;
            }
        }

    }
}