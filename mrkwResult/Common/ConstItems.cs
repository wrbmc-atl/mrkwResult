using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ConstItems
    {
        public const string ConnStrDev = "User ID=mrkw_dev;Password=mrkw_dev;Data Source=localhost:1521/XEPDB1;";
        public const string ConnStrProd = "User ID=mrkw_prod;Password=mrkw_prod;Data Source=localhost:1521/XEPDB1;";
        public const string PKG_GetStageList = "PKG_GRN_001.OPEN_CURSOR";
        public const string PKG_GetStageInfo = "PKG_GRN_002.OPEN_CURSOR";


        // メッセージ
        public const string InitError = "初期化に失敗しました。";
        public const string SearchError = "検索に失敗しました。";
        public const string InsertError = "登録に失敗しました。";
        public const string CheckError = "入力内容を確認してください。";

        // ============その他============
    }
}
