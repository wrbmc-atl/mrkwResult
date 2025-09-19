using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using mrkwResult.Models.DBInfo;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Data;

namespace mrkwResult.Models
{
    public class RequestToDB
    {
        /// <summary>
        /// PKG_GRN_001_ステージの一覧を取得
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ObservableCollection<M_COURSE>> GetCourseListAsync(string connectionString, string packageName, bool isBlank)
        {
            var list = new ObservableCollection<M_COURSE>();
            M_COURSE blk = new M_COURSE();
            if (isBlank)
            {
                list.Add(blk);
            }

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
                                var item = new M_COURSE();
                                item.COURSE_CD = reader.IsDBNull(reader.GetOrdinal("COURSE_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_CD"));
                                item.COURSE_NM = reader.IsDBNull(reader.GetOrdinal("COURSE_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM"));
                                item.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                item.ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM"));
                                item.SHTC = reader.IsDBNull(reader.GetOrdinal("SHTC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC"));
                                item.SHTC_PIC = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC"));
                                item.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                item.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                item.SKSIPGR_CD = reader.IsDBNull(reader.GetOrdinal("SKSIPGR_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKSIPGR_CD"));
                                item.SISIKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISIKSHN_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SISIKSHN_DT"));
                                item.SISIKSHNPRG_CD = reader.IsDBNull(reader.GetOrdinal("SISIKSHNPRG_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SISIKSHNPRG_CD"));
                                item.SKJ_FLG = reader.IsDBNull(reader.GetOrdinal("SKJ_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKJ_FLG"));
                                item.COURSE_NM_DISP = reader.IsDBNull(reader.GetOrdinal("COURSE_NM_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM_DISP"));
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("コースマスタの取得に失敗しました。", ex);
            }

            return list;
        }

        /// <summary>
        /// PKG_GRN_002_出発地、目的地からマスタ情報を取得
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        /// <param name="packageName">呼び出すOracleパッケージ名。</param>
        /// <param name="p_start_cd">開始地のコード。</param>
        /// <param name="p_goal_cd">目的地のコード。</param>
        /// <returns>M_RACE型のステージ情報。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<M_RACE> GetRaceInfoAsync(string connectionString, string packageName, string p_start_cd, string p_goal_cd)
        {
            var item = new M_RACE();

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
                                item = new M_RACE();
                                item.START_CD = reader.IsDBNull(reader.GetOrdinal("START_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_CD"));
                                item.GOAL_CD = reader.IsDBNull(reader.GetOrdinal("GOAL_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_CD"));
                                item.START_NM = reader.IsDBNull(reader.GetOrdinal("START_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_NM"));
                                item.GOAL_NM = reader.IsDBNull(reader.GetOrdinal("GOAL_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_NM"));
                                item.REVERSE_FLG = reader.IsDBNull(reader.GetOrdinal("REVERSE_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("REVERSE_FLG"));
                                item.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                item.ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM"));
                                item.SHTC1 = reader.IsDBNull(reader.GetOrdinal("SHTC1")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC1"));
                                item.SHTC2 = reader.IsDBNull(reader.GetOrdinal("SHTC2")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC2"));
                                item.SHTC_PIC1 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC1")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC1"));
                                item.SHTC_PIC2 = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC2")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC2"));
                                item.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                item.STAGE_TYP = reader.IsDBNull(reader.GetOrdinal("STAGE_TYP")) ? string.Empty : reader.GetString(reader.GetOrdinal("STAGE_TYP"));
                                item.REVERSE_FLG_DISP = reader.IsDBNull(reader.GetOrdinal("REVERSE_FLG_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("REVERSE_FLG_DISP"));
                                item.STAGE_TYP_DISP = reader.IsDBNull(reader.GetOrdinal("STAGE_TYP_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("STAGE_TYP_DISP"));
                            }
                            else
                            {
                                item = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("レースマスタの取得に失敗しました。", ex);
            }

            return item;
        }

        /// <summary>
        /// PKG_GRN_003_レース実績を登録
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
                        cmd.Parameters.Add("P_REVERSE_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(racejssk.REVERSE_FLG) ? "0" : racejssk.REVERSE_FLG, ParameterDirection.Input);
                        cmd.Parameters.Add("P_MIRROR_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(racejssk.MIRROR_FLG) ? "0" : racejssk.REVERSE_FLG, ParameterDirection.Input);
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
        /// PKG_GRN_004_出発地、目的地からマスタ情報を取得
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        /// <param name="packageName">呼び出すOracleパッケージ名。</param>
        /// <param name="p_course_cd">コースコード</param>
        /// <returns>M_COURSE型のステージ情報。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<M_COURSE> GetCourseInfoAsync(string connectionString, string packageName, string p_course_cd)
        {
            var item = new M_COURSE();

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
                                item.COURSE_CD = reader.IsDBNull(reader.GetOrdinal("COURSE_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_CD"));
                                item.COURSE_NM = reader.IsDBNull(reader.GetOrdinal("COURSE_NM")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM"));
                                item.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                item.ITEM = reader.IsDBNull(reader.GetOrdinal("ITEM")) ? string.Empty : reader.GetString(reader.GetOrdinal("ITEM"));
                                item.SHTC = reader.IsDBNull(reader.GetOrdinal("SHTC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC"));
                                item.SHTC_PIC = reader.IsDBNull(reader.GetOrdinal("SHTC_PIC")) ? string.Empty : reader.GetString(reader.GetOrdinal("SHTC_PIC"));
                                item.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                item.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                item.SKSIPGR_CD = reader.IsDBNull(reader.GetOrdinal("SKSIPGR_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKSIPGR_CD"));
                                item.SISIKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISIKSHN_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SISIKSHN_DT"));
                                item.SISIKSHNPRG_CD = reader.IsDBNull(reader.GetOrdinal("SISIKSHNPRG_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SISIKSHNPRG_CD"));
                                item.SKJ_FLG = reader.IsDBNull(reader.GetOrdinal("SKJ_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKJ_FLG"));
                                item.COURSE_NM_DISP = reader.IsDBNull(reader.GetOrdinal("COURSE_NM_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("COURSE_NM_DISP"));
                            }
                            else
                            {
                                item = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("コースマスタの取得に失敗しました。", ex);
            }

            return item;
        }

        /// <summary>
        /// PKG_GRN_005_
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// PKG_GRN_006_
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <param name="race"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                        cmd.Parameters.Add("P_REVERSE_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(race.REVERSE_FLG) ? DBNull.Value : race.REVERSE_FLG, ParameterDirection.Input);
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

        /// <summary>
        /// PKG_GRN_007_レース実績の一覧を取得
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ObservableCollection<T_RACEJSSK>> GetRaceJsskListAsync(string connectionString, string packageName, string p_race_kbn, string p_start_cd, string p_goal_cd, string p_race_date_start, string p_race_date_end, string p_stage_typ)
        {
            var list = new ObservableCollection<T_RACEJSSK>();

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("P_RACE_KBN", OracleDbType.Varchar2, p_race_kbn, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_START_CD", OracleDbType.Varchar2, p_start_cd, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_GOAL_CD", OracleDbType.Varchar2, p_goal_cd, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_RACE_DATE_START", OracleDbType.Varchar2, p_race_date_start, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_RACE_DATE_END", OracleDbType.Varchar2, p_race_date_end, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_STAGE_TYP", OracleDbType.Varchar2, p_stage_typ, ParameterDirection.Input));

                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new T_RACEJSSK();
                                item.RACEJSSKNO = reader.IsDBNull(reader.GetOrdinal("RACEJSSKNO")) ? string.Empty : reader.GetString(reader.GetOrdinal("RACEJSSKNO"));
                                item.RACE_KBN = reader.IsDBNull(reader.GetOrdinal("RACE_KBN")) ? string.Empty : reader.GetString(reader.GetOrdinal("RACE_KBN"));
                                item.RACE_DATE = reader.IsDBNull(reader.GetOrdinal("RACE_DATE")) ? null : reader.GetDateTime(reader.GetOrdinal("RACE_DATE"));
                                item.START_CD = reader.IsDBNull(reader.GetOrdinal("START_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_CD"));
                                item.GOAL_CD = reader.IsDBNull(reader.GetOrdinal("GOAL_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_CD"));
                                item.STAGE_TYP = reader.IsDBNull(reader.GetOrdinal("STAGE_TYP")) ? string.Empty : reader.GetString(reader.GetOrdinal("STAGE_TYP"));
                                item.REVERSE_FLG = reader.IsDBNull(reader.GetOrdinal("REVERSE_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("REVERSE_FLG"));
                                item.MIRROR_FLG = reader.IsDBNull(reader.GetOrdinal("MIRROR_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("MIRROR_FLG"));
                                item.RANK = reader.IsDBNull(reader.GetOrdinal("RANK")) ? null : reader.GetInt64(reader.GetOrdinal("RANK"));
                                item.HEADCOUNT = reader.IsDBNull(reader.GetOrdinal("HEADCOUNT")) ? null : reader.GetInt64(reader.GetOrdinal("HEADCOUNT"));
                                item.RATE_END = reader.IsDBNull(reader.GetOrdinal("RATE_END")) ? null : reader.GetInt64(reader.GetOrdinal("RATE_END"));
                                item.REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader.GetString(reader.GetOrdinal("REMARK"));
                                item.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                item.SISIKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISIKSHN_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SISIKSHN_DT"));
                                item.START_NM_DISP = reader.IsDBNull(reader.GetOrdinal("START_NM_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("START_NM_DISP"));
                                item.GOAL_NM_DISP = reader.IsDBNull(reader.GetOrdinal("GOAL_NM_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("GOAL_NM_DISP"));
                                item.RACE_KBN_DISP = reader.IsDBNull(reader.GetOrdinal("RACE_KBN_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("RACE_KBN_DISP"));
                                item.REVERSE_FLG_DISP = reader.IsDBNull(reader.GetOrdinal("REVERSE_FLG_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("REVERSE_FLG_DISP"));
                                item.MIRROR_FLG_DISP = reader.IsDBNull(reader.GetOrdinal("MIRROR_FLG_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("MIRROR_FLG_DISP"));
                                item.STAGE_TYP_DISP = reader.IsDBNull(reader.GetOrdinal("STAGE_TYP_DISP")) ? string.Empty : reader.GetString(reader.GetOrdinal("STAGE_TYP_DISP"));
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("レース実績の取得に失敗しました。", ex);
            }

            return list;
        }

        /// <summary>
        /// PKG_GRN_008_コードマスタの一覧を取得（CODE1のみ）
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ObservableCollection<M_CODE>> GetCodeListAsync(string connectionString, string packageName, string code1, bool isBlank)
        {
            var list = new ObservableCollection<M_CODE>();
            M_CODE blk = new M_CODE();
            if (isBlank)
            {
                list.Add(blk);
            }
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("P_CODE1", OracleDbType.Varchar2, code1, ParameterDirection.Input));
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new M_CODE();
                                item.CODE1 = reader.IsDBNull(reader.GetOrdinal("CODE1")) ? string.Empty : reader.GetString(reader.GetOrdinal("CODE1"));
                                item.CODE2 = reader.IsDBNull(reader.GetOrdinal("CODE2")) ? string.Empty : reader.GetString(reader.GetOrdinal("CODE2"));
                                item.CODE3 = reader.IsDBNull(reader.GetOrdinal("CODE3")) ? string.Empty : reader.GetString(reader.GetOrdinal("CODE3"));
                                item.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                item.DESCRIPTION = reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")) ? string.Empty : reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                                item.STR1 = reader.IsDBNull(reader.GetOrdinal("STR1")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR1"));
                                item.STR2 = reader.IsDBNull(reader.GetOrdinal("STR2")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR2"));
                                item.STR3 = reader.IsDBNull(reader.GetOrdinal("STR3")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR3"));
                                item.STR4 = reader.IsDBNull(reader.GetOrdinal("STR4")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR4"));
                                item.STR5 = reader.IsDBNull(reader.GetOrdinal("STR5")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR5"));
                                item.NUM1 = reader.IsDBNull(reader.GetOrdinal("NUM1")) ? null : reader.GetInt64(reader.GetOrdinal("NUM1"));
                                item.NUM2 = reader.IsDBNull(reader.GetOrdinal("NUM2")) ? null : reader.GetInt64(reader.GetOrdinal("NUM2"));
                                item.NUM3 = reader.IsDBNull(reader.GetOrdinal("NUM3")) ? null : reader.GetInt64(reader.GetOrdinal("NUM3"));
                                item.NUM4 = reader.IsDBNull(reader.GetOrdinal("NUM4")) ? null : reader.GetInt64(reader.GetOrdinal("NUM4"));
                                item.NUM5 = reader.IsDBNull(reader.GetOrdinal("NUM5")) ? null : reader.GetInt64(reader.GetOrdinal("NUM5"));
                                item.DEC1 = reader.IsDBNull(reader.GetOrdinal("DEC1")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC1"));
                                item.DEC2 = reader.IsDBNull(reader.GetOrdinal("DEC2")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC2"));
                                item.DEC3 = reader.IsDBNull(reader.GetOrdinal("DEC3")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC3"));
                                item.DEC4 = reader.IsDBNull(reader.GetOrdinal("DEC4")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC4"));
                                item.DEC5 = reader.IsDBNull(reader.GetOrdinal("DEC5")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC5"));
                                item.DATETIME1 = reader.IsDBNull(reader.GetOrdinal("DATETIME1")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME1"));
                                item.DATETIME2 = reader.IsDBNull(reader.GetOrdinal("DATETIME2")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME2"));
                                item.DATETIME3 = reader.IsDBNull(reader.GetOrdinal("DATETIME3")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME3"));
                                item.DATETIME4 = reader.IsDBNull(reader.GetOrdinal("DATETIME4")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME4"));
                                item.DATETIME5 = reader.IsDBNull(reader.GetOrdinal("DATETIME5")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME5"));
                                item.DATE1 = reader.IsDBNull(reader.GetOrdinal("DATE1")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE1"));
                                item.DATE2 = reader.IsDBNull(reader.GetOrdinal("DATE2")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE2"));
                                item.DATE3 = reader.IsDBNull(reader.GetOrdinal("DATE3")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE3"));
                                item.DATE4 = reader.IsDBNull(reader.GetOrdinal("DATE4")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE4"));
                                item.DATE5 = reader.IsDBNull(reader.GetOrdinal("DATE5")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE5"));
                                item.TIME1 = reader.IsDBNull(reader.GetOrdinal("TIME1")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME1"));
                                item.TIME2 = reader.IsDBNull(reader.GetOrdinal("TIME2")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME2"));
                                item.TIME3 = reader.IsDBNull(reader.GetOrdinal("TIME3")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME3"));
                                item.TIME4 = reader.IsDBNull(reader.GetOrdinal("TIME4")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME4"));
                                item.TIME5 = reader.IsDBNull(reader.GetOrdinal("TIME5")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME5"));
                                item.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                item.SKSIPGR_CD = reader.IsDBNull(reader.GetOrdinal("SKSIPGR_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKSIPGR_CD"));
                                item.SISHKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISHKSHN_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SISHKSHN_DT"));
                                item.SISHKSHNPRG_CD = reader.IsDBNull(reader.GetOrdinal("SISHKSHNPRG_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SISHKSHNPRG_CD"));
                                item.SKJ_FLG = reader.IsDBNull(reader.GetOrdinal("SKJ_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKJ_FLG"));
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("コードマスタの取得に失敗しました。", ex);
            }

            return list;
        }

        /// <summary>
        /// PKG_GRN_009_コードマスタの一覧を取得（CODE1, CODE2）
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ObservableCollection<M_CODE>> GetCodeListAsync(string connectionString, string packageName, string code1, string code2, bool isBlank)
        {
            var list = new ObservableCollection<M_CODE>();
            M_CODE blk = new M_CODE();
            if (isBlank)
            {
                list.Add(blk);
            }
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("P_CODE1", OracleDbType.Varchar2, code1, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("P_CODE2", OracleDbType.Varchar2, code2, ParameterDirection.Input));
                        var cursorParameter = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(cursorParameter);

                        using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new M_CODE();
                                item.CODE1 = reader.IsDBNull(reader.GetOrdinal("CODE1")) ? string.Empty : reader.GetString(reader.GetOrdinal("CODE1"));
                                item.CODE2 = reader.IsDBNull(reader.GetOrdinal("CODE2")) ? string.Empty : reader.GetString(reader.GetOrdinal("CODE2"));
                                item.CODE3 = reader.IsDBNull(reader.GetOrdinal("CODE3")) ? string.Empty : reader.GetString(reader.GetOrdinal("CODE3"));
                                item.DISPORDER = reader.IsDBNull(reader.GetOrdinal("DISPORDER")) ? null : reader.GetInt64(reader.GetOrdinal("DISPORDER"));
                                item.DESCRIPTION = reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")) ? string.Empty : reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                                item.STR1 = reader.IsDBNull(reader.GetOrdinal("STR1")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR1"));
                                item.STR2 = reader.IsDBNull(reader.GetOrdinal("STR2")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR2"));
                                item.STR3 = reader.IsDBNull(reader.GetOrdinal("STR3")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR3"));
                                item.STR4 = reader.IsDBNull(reader.GetOrdinal("STR4")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR4"));
                                item.STR5 = reader.IsDBNull(reader.GetOrdinal("STR5")) ? string.Empty : reader.GetString(reader.GetOrdinal("STR5"));
                                item.NUM1 = reader.IsDBNull(reader.GetOrdinal("NUM1")) ? null : reader.GetInt64(reader.GetOrdinal("NUM1"));
                                item.NUM2 = reader.IsDBNull(reader.GetOrdinal("NUM2")) ? null : reader.GetInt64(reader.GetOrdinal("NUM2"));
                                item.NUM3 = reader.IsDBNull(reader.GetOrdinal("NUM3")) ? null : reader.GetInt64(reader.GetOrdinal("NUM3"));
                                item.NUM4 = reader.IsDBNull(reader.GetOrdinal("NUM4")) ? null : reader.GetInt64(reader.GetOrdinal("NUM4"));
                                item.NUM5 = reader.IsDBNull(reader.GetOrdinal("NUM5")) ? null : reader.GetInt64(reader.GetOrdinal("NUM5"));
                                item.DEC1 = reader.IsDBNull(reader.GetOrdinal("DEC1")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC1"));
                                item.DEC2 = reader.IsDBNull(reader.GetOrdinal("DEC2")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC2"));
                                item.DEC3 = reader.IsDBNull(reader.GetOrdinal("DEC3")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC3"));
                                item.DEC4 = reader.IsDBNull(reader.GetOrdinal("DEC4")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC4"));
                                item.DEC5 = reader.IsDBNull(reader.GetOrdinal("DEC5")) ? null : reader.GetDecimal(reader.GetOrdinal("DEC5"));
                                item.DATETIME1 = reader.IsDBNull(reader.GetOrdinal("DATETIME1")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME1"));
                                item.DATETIME2 = reader.IsDBNull(reader.GetOrdinal("DATETIME2")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME2"));
                                item.DATETIME3 = reader.IsDBNull(reader.GetOrdinal("DATETIME3")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME3"));
                                item.DATETIME4 = reader.IsDBNull(reader.GetOrdinal("DATETIME4")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME4"));
                                item.DATETIME5 = reader.IsDBNull(reader.GetOrdinal("DATETIME5")) ? null : reader.GetDateTime(reader.GetOrdinal("DATETIME5"));
                                item.DATE1 = reader.IsDBNull(reader.GetOrdinal("DATE1")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE1"));
                                item.DATE2 = reader.IsDBNull(reader.GetOrdinal("DATE2")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE2"));
                                item.DATE3 = reader.IsDBNull(reader.GetOrdinal("DATE3")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE3"));
                                item.DATE4 = reader.IsDBNull(reader.GetOrdinal("DATE4")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE4"));
                                item.DATE5 = reader.IsDBNull(reader.GetOrdinal("DATE5")) ? null : reader.GetDateTime(reader.GetOrdinal("DATE5"));
                                item.TIME1 = reader.IsDBNull(reader.GetOrdinal("TIME1")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME1"));
                                item.TIME2 = reader.IsDBNull(reader.GetOrdinal("TIME2")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME2"));
                                item.TIME3 = reader.IsDBNull(reader.GetOrdinal("TIME3")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME3"));
                                item.TIME4 = reader.IsDBNull(reader.GetOrdinal("TIME4")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME4"));
                                item.TIME5 = reader.IsDBNull(reader.GetOrdinal("TIME5")) ? null : reader.GetDateTime(reader.GetOrdinal("TIME5"));
                                item.SKSI_DT = reader.IsDBNull(reader.GetOrdinal("SKSI_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SKSI_DT"));
                                item.SKSIPGR_CD = reader.IsDBNull(reader.GetOrdinal("SKSIPGR_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKSIPGR_CD"));
                                item.SISHKSHN_DT = reader.IsDBNull(reader.GetOrdinal("SISHKSHN_DT")) ? null : reader.GetDateTime(reader.GetOrdinal("SISHKSHN_DT"));
                                item.SISHKSHNPRG_CD = reader.IsDBNull(reader.GetOrdinal("SISHKSHNPRG_CD")) ? string.Empty : reader.GetString(reader.GetOrdinal("SISHKSHNPRG_CD"));
                                item.SKJ_FLG = reader.IsDBNull(reader.GetOrdinal("SKJ_FLG")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKJ_FLG"));
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("コードマスタの取得に失敗しました。", ex);
            }

            return list;
        }

        /// <summary>
        /// PKG_GRN_010_レース実績を修正
        /// </summary>
        /// <param name="connectionString">データベース接続文字列。</param>
        /// <param name="packageName">呼び出すOracleパッケージ名。</param>
        /// <param name="racejssk">実績テーブルクラス。</param>
        /// <returns>登録が成功した場合はtrue、それ以外はfalse。</returns>
        /// <exception cref="Exception">データベース操作失敗時にスローされます。</exception>
        public async Task<bool> UpdateRaceJsskAsync(string connectionString, string packageName, T_RACEJSSK racejssk)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (OracleCommand cmd = new OracleCommand(packageName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("P_RACEJSSKNO", OracleDbType.Varchar2, racejssk.RACEJSSKNO, ParameterDirection.Input);
                        cmd.Parameters.Add("P_RACE_KBN", OracleDbType.Varchar2, racejssk.RACE_KBN, ParameterDirection.Input);
                        cmd.Parameters.Add("P_RACE_DATE", OracleDbType.Varchar2, racejssk.RACE_DATE?.ToString("yyyy/MM/dd") ?? (object)DBNull.Value, ParameterDirection.Input);
                        cmd.Parameters.Add("P_START_CD", OracleDbType.Varchar2, racejssk.START_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_GOAL_CD", OracleDbType.Varchar2, racejssk.GOAL_CD, ParameterDirection.Input);
                        cmd.Parameters.Add("P_MIRROR_FLG", OracleDbType.Varchar2, string.IsNullOrEmpty(racejssk.MIRROR_FLG) ? "0" : racejssk.REVERSE_FLG, ParameterDirection.Input);
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

    }
}
