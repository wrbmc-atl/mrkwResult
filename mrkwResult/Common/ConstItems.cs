using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ConstItems
    {
        public const string ConnStr = "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=brkshcir;";

        // 譜面確認URLヘッダー
        public const string CheetUrlHead = "";
        // 楽曲WikiURL
        public const string MstMscUrl = "https://pjsekai.com/?aad6ee23b0";
        public const string MstMscScr_Header = "https://sdvx.in/prsk/";

        // メッセージ
        public const string InitError = "初期化に失敗しました。";
        public const string SearchError = "検索に失敗しました。";
        public const string InsertError = "登録に失敗しました。";
        public const string CheckError = "入力内容を確認してください。";

        // 数値
        public const long MIN_EXP_LV = 21;
        public const long MAX_EXP_LV = 31;
        public const long MIN_MAS_LV = 25;
        public const long MAX_MAS_LV = 37;
        public const long MIN_APD_LV = 24;
        public const long MAX_APD_LV = 38;
        public const long MAXROW = 1000;
        public const int DIFF_MONTH = 3;

        // ============その他============
        // プロセカサービス開始日
        public const string START_DATE = "2020/09/30";
    }
}
