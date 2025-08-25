using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Models
{
    public static class Common
    {
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 中央値を計算する関数
        public static double CalculateMedian(ObservableCollection<double> collection)
        {
            if (collection.Count == 0) return 0;

            // 昇順にソートしたリストを取得
            var sorted = collection.OrderBy(x => x).ToList();

            int count = sorted.Count;
            if (count % 2 == 0)
            {
                // 偶数の場合、中央の2つの値の平均を返す
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
            }
            else
            {
                // 奇数の場合、中央の値を返す
                return sorted[count / 2];
            }
        }

        // 分散を計算する関数
        public static double CalculateVariance(ObservableCollection<double> collection)
        {
            if (collection.Count == 0) return 0;

            // 平均値を計算
            double average = collection.Average();

            // 分散を計算 (要素ごとの (値 - 平均値) の二乗の平均)
            double variance = collection.Average(x => Math.Pow(x - average, 2));

            return variance;
        }

        //// 最小二乗法による線形回帰モデルを使って予測する
        //public static long Predict(ObservableCollection<BorderPredict> obcBorderPredicts, DateTime newDate)
        //{
        //    // x = DateTime（double）、y = Value（long）
        //    var xValues = obcBorderPredicts.Select(dp => ConvertDateTimeToDouble(dp.recorddate)).ToArray();
        //    var yValues = obcBorderPredicts.Select(dp => (double)dp.eventpoint).ToArray();

        //    // xの平均とyの平均を計算
        //    double xMean = xValues.Average();
        //    double yMean = yValues.Average();

        //    // 最小二乗法の計算
        //    double numerator = 0;
        //    double denominator = 0;
        //    for (int i = 0; i < xValues.Length; i++)
        //    {
        //        numerator += (xValues[i] - xMean) * (yValues[i] - yMean);
        //        denominator += (xValues[i] - xMean) * (xValues[i] - xMean);
        //    }

        //    // 傾き(slope)と切片(intercept)の計算
        //    double slope = numerator / denominator;
        //    double intercept = yMean - (slope * xMean);

        //    // 新しいDateTimeの予測
        //    double newX = ConvertDateTimeToDouble(newDate);
        //    double predictedY = slope * newX + intercept;

        //    return (long)Math.Round(predictedY);
        //}

        // DateTimeをdoubleに変換する（例えば、Unix時間に変換）
        public static double ConvertDateTimeToDouble(DateTime dateTime)
        {
            return dateTime.ToOADate();  // OLE Automation Date を使用
        }

    }
}
