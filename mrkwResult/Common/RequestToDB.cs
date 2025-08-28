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
        public async Task<ObservableCollection<M_COURSE>> GetCourseListAsync(string connectionString, string packageName)
        {
            var stageList = new ObservableCollection<M_COURSE>();

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // OUTパラメータとしてカーソルを定義
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var stageItem = new M_COURSE();
                                stageItem.COURSE_CD = reader.IsDBNull(reader.GetOrdinal("COURSE_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_CD"));
                                stageItem.COURSE_NM = reader.IsDBNull(reader.GetOrdinal("COURSE_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM"));
                                stageItem.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? (long?)null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                stageItem.ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM"));
                                stageItem.SHTC = reader.IsDBNull(reader.GetOrdinal("SHTC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC"));
                                stageItem.SHTC_PIC = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC"));
                                stageItem.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                stageItem.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                stageItem.SKSIPGR_CD = reader.IsDBNull(reader.GetOrdinal("SKSIPGR_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKSIPGR_CD"));
                                stageItem.SISIKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISIKSHN_DT")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("SISIKSHN_DT"));
                                stageItem.SISIKSHNPRG_CD = reader.IsDBNull(reader.GetOrdinal("SISIKSHNPRG_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SISIKSHNPRG_CD"));
                                stageItem.SKJ_FLG = reader.IsDBNull(reader.GetOrdinal("SKJ_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKJ_FLG"));
                                stageItem.COURSE_NM_DISP = reader.IsDBNull(reader.GetOrdinal("COURSE_NM_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM_DISP"));
                                stageList.Add(stageItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("コースマスタの取得に失敗しました。", ex);
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
        /// <returns>M_RACE型のステージ情報。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<M_RACE> GetRaceInfoAsync(string connectionString, string packageName, string p_start_cd, string p_goal_cd)
        {
            var stageInfo = new M_RACE();

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("P_START_CD", OracleDbType.Varchar2, p_start_cd, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_GOAL_CD", OracleDbType.Varchar2, p_goal_cd, ParameterDirection.Input));

                        // OUTパラメータとしてカーソルを定義
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                stageInfo = new M_RACE();
                                stageInfo.START_CD = reader.IsDBNull(reader.GetOrdinal("START_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_CD"));
                                stageInfo.GOAL_CD = reader.IsDBNull(reader.GetOrdinal("GOAL_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_CD"));
                                stageInfo.START_NM = reader.IsDBNull(reader.GetOrdinal("START_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_NM"));
                                stageInfo.GOAL_NM = reader.IsDBNull(reader.GetOrdinal("GOAL_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_NM"));
                                stageInfo.REVERSE_FLG = reader.IsDBNull(reader.GetOrdinal("REVERSE_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("REVERSE_FLG"));
                                stageInfo.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? (long?)null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                stageInfo.ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM"));
                                stageInfo.SHTC1 = reader.IsDBNull(reader.GetOrdinal("SHTC1")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC1"));
                                stageInfo.SHTC2 = reader.IsDBNull(reader.GetOrdinal("SHTC2")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC2"));
                                stageInfo.SHTC_PIC1 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC1")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC1"));
                                stageInfo.SHTC_PIC2 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC2")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC2"));
                                stageInfo.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                stageInfo.STAGE_TYP = reader.IsDBNull(reader.GetOrdinal("STAGE_TYP")) ? string.Empty : reader.GetString(reader.GetOrdinal("STAGE_TYP"));
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
                throw new Exception("レースマスタの取得に失敗しました。", ex);
            }

            return stageInfo;
        }
        
        /// <summary>
        /// レース実績を登録
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        /// <param name="packageName">呼び出すOracleパッケージ名。</param>
        /// <param name="racejssk">実績テーブルクラス。</param>
        /// <returns>登録が成功した場合はtrue、それ以外はfalse。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<bool> InsertRaceJsskAsync(string connectionString, string packageName, T_RACEJSSK racejssk)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("P_RACE_KBN", OracleDbType.Varchar2, racejssk.RACE_KBN, ParameterDirection.Input);
                        cmd.Parameters.Add("P_RACE_DATE", OracleDbType.Varchar2, racejssk.RACE_DATE?.ToString("yyyy/MM/dd") ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_START_CD", OracleDbType.Varchar2, racejssk.START_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_GOAL_CD", OracleDbType.Varchar2, racejssk.GOAL_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_STAGE_TYP", OracleDbType.Varchar2, racejssk.STAGE_TYP, ParameterDirection.Input);
                        cmd.Parameters.Add("P_REVERSE_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(racejssk.REVERSE_FLG) ? (object)DBNull.Value : racejssk.REVERSE_FLG, ParameterDirection.Input);
                        cmd.Parameters.Add("P_MIRROR_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(racejssk.MIRROR_FLG) ? (object)DBNull.Value : racejssk.MIRROR_FLG, ParameterDirection.Input);
                        cmd.Parameters.Add("P_RANK", OracleDbType.Varchar2, racejssk.RANK?.ToString() ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_HEADCOUNT", OracleDbType.Varchar2, racejssk.HEADCOUNT?.ToString() ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_RATE_END", OracleDbType.Varchar2, racejssk.RATE_END?.ToString() ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2, racejssk.REMARK, ParameterDirection.Input);
                        cmd.Parameters.Add("CUR_ITEM", OracleDbType.RefCursor, ParameterDirection.InputOutput);

                        await cmd.ExecuteNonQueryAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("レース実績の登録に失敗しました。", ex);
            }
        }


        /// <summary>
        /// 出発地、目的地からマスタ情報を取得
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        /// <param name="packageName">呼び出すOracleパッケージ名。</param>
        /// <param name="p_course_cd">コースコード</param>
        /// <returns>M_COURSE型のステージ情報。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<M_COURSE> GetCourseInfoAsync(string connectionString, string packageName, string p_course_cd)
        {
            var stageInfo = new M_COURSE();

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("P_COURSE_CD", OracleDbType.Varchar2, p_course_cd, ParameterDirection.Input));

                        // OUTパラメータとしてカーソルを定義
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                stageInfo.COURSE_CD = reader.IsDBNull(reader.GetOrdinal("COURSE_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_CD"));
                                stageInfo.COURSE_NM = reader.IsDBNull(reader.GetOrdinal("COURSE_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM"));
                                stageInfo.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? (long?)null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                stageInfo.ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM"));
                                stageInfo.SHTC = reader.IsDBNull(reader.GetOrdinal("SHTC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC"));
                                stageInfo.SHTC_PIC = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC"));
                                stageInfo.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                stageInfo.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                stageInfo.SKSIPGR_CD = reader.IsDBNull(reader.GetOrdinal("SKSIPGR_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKSIPGR_CD"));
                                stageInfo.SISIKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISIKSHN_DT")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("SISIKSHN_DT"));
                                stageInfo.SISIKSHNPRG_CD = reader.IsDBNull(reader.GetOrdinal("SISIKSHNPRG_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SISIKSHNPRG_CD"));
                                stageInfo.SKJ_FLG = reader.IsDBNull(reader.GetOrdinal("SKJ_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKJ_FLG"));
                                stageInfo.COURSE_NM_DISP = reader.IsDBNull(reader.GetOrdinal("COURSE_NM_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM_DISP"));
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
                throw new Exception("コースマスタの取得に失敗しました。", ex);
            }

            return stageInfo;
        }

        public async Task<bool> UpdateCourseMasterAsync(string connectionString, string packageName, M_COURSE course)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("P_COURSE_CD", OracleDbType.Varchar2, course.COURSE_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_DISPORDER", OracleDbType.Varchar2, course.DISPORDER?.ToString() ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_ITEM", OracleDbType.Varchar2, course.ITEM, ParameterDirection.Input);
                        cmd.Parameters.Add("P_SHTC", OracleDbType.Varchar2, course.SHTC, ParameterDirection.Input);
                        cmd.Parameters.Add("P_SHTC_PIC", OracleDbType.Varchar2, course.SHTC_PIC, ParameterDirection.Input);
                        cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2, course.REMARK, ParameterDirection.Input);
                        cmd.Parameters.Add("CUR_ITEM", OracleDbType.RefCursor, ParameterDirection.InputOutput);

                        
                        await cmd.ExecuteNonQueryAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("コースマスタの更新に失敗しました。", ex);
            }
        }

        public async Task<bool> UpdateRaceMasterAsync(string connectionString, string packageName, M_RACE race)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("P_START_CD", OracleDbType.Varchar2, race.START_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_GOAL_CD", OracleDbType.Varchar2, race.GOAL_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_REVERSE_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(race.REVERSE_FLG) ? (object)DBNull.Value : race.REVERSE_FLG, ParameterDirection.Input);
                        cmd.Parameters.Add("P_DISPORDER", OracleDbType.Varchar2, race.DISPORDER?.ToString() ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_ITEM", OracleDbType.Varchar2, race.ITEM, ParameterDirection.Input);
                        cmd.Parameters.Add("P_SHTC1", OracleDbType.Varchar2, race.SHTC1, ParameterDirection.Input);
                        cmd.Parameters.Add("P_SHTC2", OracleDbType.Varchar2, race.SHTC2, ParameterDirection.Input);
                        cmd.Parameters.Add("P_SHTC_PIC1", OracleDbType.Varchar2, race.SHTC_PIC1, ParameterDirection.Input);
                        cmd.Parameters.Add("P_SHTC_PIC2", OracleDbType.Varchar2, race.SHTC_PIC2, ParameterDirection.Input);
                        cmd.Parameters.Add("P_REMARK", OracleDbType.Varchar2, race.REMARK, ParameterDirection.Input);
                        cmd.Parameters.Add("CUR_ITEM", OracleDbType.RefCursor, ParameterDirection.InputOutput);

                        
                        await cmd.ExecuteNonQueryAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("レースマスタの更新に失敗しました。", ex);
            }
        }
    }
}
