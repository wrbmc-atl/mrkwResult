using DocumentFormat.OpenXml.Wordprocessing;
using mrkwResult.Models.DBInfo;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Data;

namespace mrkwResult.Common
{
    public class RequestToDB
    {
        /// <summary>
        /// ステージの一覧を取得
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ObservableCollection<M_STGList>> GetStageListAsync(string connectionString, string packageName)
        {
            // ObservableCollectionを初期化
            var stageList = new ObservableCollection<M_STGList>();

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand command = new OracleCommand(packageName, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // OUTパラメータとしてカーソルを定義
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        command.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var stageItem = new M_STGList
                                {
                                    STAGE_CD = reader.GetString(reader.GetOrdinal("STAGE_CD")),
                                    STAGE_NM = reader.GetString(reader.GetOrdinal("STAGE_NM")),
                                };
                                stageList.Add(stageItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ここでは例外をスローせず、呼び出し元で処理させる
                // もしくはログに記録するなど
                throw new Exception("ステージマスタの取得に失敗しました。", ex);
            }

            return stageList;
        }

        /// <summary>
        /// 出発地、目的地からマスタ情報を取得
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        /// <param name="packageName">呼び出すOracleパッケージ名。</param>
        /// <param name="p_start_cd">開始地のコード。</param>
        /// <param name="p_goal_cd">目的地のコード。</param>
        /// <returns>M_STG型のステージ情報。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<M_STG> GetStageInfoAsync(string connectionString, string packageName, string p_start_cd, string p_goal_cd)
        {
            // M_STG型のインスタンスを初期化。もしデータが見つからなければ、この初期値が返される。
            var stageInfo = new M_STG();

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand command = new OracleCommand(packageName, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 明示的にパラメータを追加
                        command.Parameters.Add(new OracleParameter("P_START_CD", OracleDbType.Varchar2, p_start_cd, ParameterDirection.Input));
                        command.Parameters.Add(new OracleParameter("P_GOAL_CD", OracleDbType.Varchar2, p_goal_cd, ParameterDirection.Input));

                        // OUTパラメータとしてカーソルを定義
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        command.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // データが取得できた場合のみ、M_STGオブジェクトにデータを設定
                            if (await reader.ReadAsync())
                            {
                                stageInfo = new M_STG
                                {
                                    START_CD = reader.IsDBNull(reader.GetOrdinal("START_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_CD")),
                                    GOAL_CD = reader.IsDBNull(reader.GetOrdinal("GOAL_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_CD")),
                                    START_NM = reader.IsDBNull(reader.GetOrdinal("START_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_NM")),
                                    GOAL_NM = reader.IsDBNull(reader.GetOrdinal("GOAL_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_NM")),
                                    REVERSE_FLG = reader.IsDBNull(reader.GetOrdinal("REVERSE_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("REVERSE_FLG")),
                                    DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? string.Empty : reader.GetString(reader.GetOrdinal("DISPORDER")),
                                    ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM")),
                                    SHTC1 = reader.IsDBNull(reader.GetOrdinal("SHTC1")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC1")),
                                    SHTC2 = reader.IsDBNull(reader.GetOrdinal("SHTC2")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC2")),
                                    SHTC3 = reader.IsDBNull(reader.GetOrdinal("SHTC3")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC3")),
                                    SHTC_PIC1 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC1")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC1")),
                                    SHTC_PIC2 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC2")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC2")),
                                    SHTC_PIC3 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC3")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC3")),
                                    REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK")),
                                    STAGE_TYP = reader.IsDBNull(reader.GetOrdinal("STAGE_TYP")) ? string.Empty : reader.GetString(reader.GetOrdinal("STAGE_TYP"))
                                };
                            }
                            else
                            {
                                stageInfo = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // データベース操作に失敗した場合、より詳細なメッセージを含む例外を再スロー
                throw new Exception("ステージマスタの取得に失敗しました。", ex);
            }

            return stageInfo;
        }

    }
}
