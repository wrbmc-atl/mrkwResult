using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ConstItems
    {
        // 接続先
        public const string ConnStrDev = "User ID=mrkw_dev;Password=mrkw_dev;Data Source=localhost:1521/XEPDB1;";
        public const string ConnStrProd = "User ID=mrkw_prod;Password=mrkw_prod;Data Source=localhost:1521/XEPDB1;";
        
        // パッケージ名
        public const string PKG_GetCourseList = "PKG_GRN_001.OPEN_CURSOR";
        public const string PKG_GetRaceInfo = "PKG_GRN_002.OPEN_CURSOR";
        public const string PKG_InsertRaceJssk = "PKG_GRN_003.OPEN_CURSOR";
        public const string PKG_GetCourseInfo = "PKG_GRN_004.OPEN_CURSOR";
        public const string PKG_UpdateCourseMaster = "PKG_GRN_005.OPEN_CURSOR";
        public const string PKG_UpdateRaceMaster = "PKG_GRN_006.OPEN_CURSOR";
        public const string PKG_GetRacejsskInfo = "PKG_GRN_007.OPEN_CURSOR";
        public const string PKG_GetCode1List = "PKG_GRN_008.OPEN_CURSOR";
        public const string PKG_GetCode2List = "PKG_GRN_009.OPEN_CURSOR";
        public const string PKG_UpdateRaceJssk = "PKG_GRN_010.OPEN_CURSOR";


        // メッセージ
        public const string InitError = "初期化に失敗しました。";
        public const string SearchError = "検索に失敗しました。";
        public const string InsertError = "登録に失敗しました。";
        public const string UpdateError = "更新に失敗しました。";
        public const string DeleteError = "削除に失敗しました。";
        public const string GetKeyError = "キー情報の取得に失敗しました。";
        public const string CheckError = "入力内容を確認してください。";
        public const string InsertComp = "登録に成功しました。";
        public const string UpdateComp = "更新に成功しました。";
        public const string DeleteComp = "削除に成功しました。";

        // その他

        public enum WindowType
        {
            GrandPrix,
            GrandPrixView,
            EditCourse,
            EditRace
        }
    }
}
