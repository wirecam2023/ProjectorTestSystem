using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ProjectorInformation_Model;

namespace ProjectorInformation_Dal
{
    public class ProjectorInformation_MainTableDal
    {
        /// <summary>
        /// 查询有没有存在机身编码
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int selectCountProjectorInformation_MainTableDal(string FuselageCode,string ZhiDan,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=FuselageCode},  
              new SqlParameter("@ZhiDan",SqlDbType.VarChar,50){Value=ZhiDan}  
            };

            return (int)SQLhelp.ExecuteScalar(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 打光机插入
        /// </summary>
        /// <param name="pdg"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTableDal(ProjectorInformation_DG pdg,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@OpticalCode",SqlDbType.VarChar,80){Value=pdg.OpticalCode},
                new SqlParameter("@PolishingMachineTime",SqlDbType.DateTime){Value=pdg.PolishingMachineTime},
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=pdg.FuselageCode},
                new SqlParameter("@MainBoardCode",SqlDbType.VarChar,50){Value=pdg.MainBoardCode},
                new SqlParameter("@MainBoardTime",SqlDbType.DateTime){Value=pdg.MainBoardTime},
                new SqlParameter("@ZhiDan",SqlDbType.VarChar,50){Value=pdg.ZhiDan}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 打光机更新
        /// </summary>
        /// <param name="pdg"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTableDal(ProjectorInformation_DG pdg, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@OpticalCode",SqlDbType.VarChar,80){Value=pdg.OpticalCode},
                new SqlParameter("@PolishingMachineTime",SqlDbType.DateTime){Value=pdg.PolishingMachineTime},
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=pdg.FuselageCode},
                new SqlParameter("@MainBoardCode",SqlDbType.VarChar,50){Value=pdg.MainBoardCode},
                new SqlParameter("@MainBoardTime",SqlDbType.DateTime){Value=pdg.MainBoardTime},
                new SqlParameter("@ZhiDan",SqlDbType.VarChar,50){Value=pdg.ZhiDan}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化前测试，如果主表没有该机身码，则插入
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_LHQDal(ProjectorInformation_LHQ proje,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=proje.FuselageCode},
              new SqlParameter("@PreAgingTestTime",SqlDbType.DateTime){Value=proje.PreAgingTestTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 查找老化前测试是否是第二次测试
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public ProjectorInformation_LHQ selectProjectorInformation_MainTable_LHQDal(string FuselageCode, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            ProjectorInformation_LHQ pif = new ProjectorInformation_LHQ();
            DateTime dt = new DateTime(1993, 5, 17);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=FuselageCode}  
            };

            using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text, pms))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pif.FuselageCode = reader.GetString(0);
                        pif.PreAgingTestTime = reader.IsDBNull(1) ? dt : reader.GetDateTime(1);
                        pif.PreAgingTestTime2 = reader.IsDBNull(2) ? null : reader.GetDateTime(2).ToString();
                    }
                }
                return pif;
            }
        }
        /// <summary>
        /// 老化前测试更新
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="PreAgingTestTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LHQDal(string FuselageCode,DateTime PreAgingTestTime,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=FuselageCode},
              new SqlParameter("@PreAgingTestTime",SqlDbType.DateTime){Value=PreAgingTestTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化后测试更新
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="PreAgingTestTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LHHDal(string FuselageCode, DateTime PreAgingTestTime, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=FuselageCode},
              new SqlParameter("@PostAgingTestTime",SqlDbType.DateTime){Value=PreAgingTestTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化上架时间插入
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_LHSDal(ProjectorInformation_LHS lhs, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=lhs.FuselageCode},
              new SqlParameter("@AgeingBeginTime",SqlDbType.DateTime){Value=lhs.AgeingBeginTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化上架时间更新
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LHSDal(ProjectorInformation_LHS lhs, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=lhs.FuselageCode},
              new SqlParameter("@AgeingBeginTime",SqlDbType.DateTime){Value=lhs.AgeingBeginTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化下架时间更新
        /// </summary>
        /// <param name="lhx"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LHXDal(ProjectorInformation_LHX lhx, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=lhx.FuselageCode},
              new SqlParameter("@AgeingEndTime",SqlDbType.DateTime){Value=lhx.AgeingEndTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化下架时间插入
        /// </summary>
        /// <param name="lhx"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_LHXDal(ProjectorInformation_LHX lhx, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=lhx.FuselageCode},
              new SqlParameter("@AgeingEndTime",SqlDbType.DateTime){Value=lhx.AgeingEndTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化后插入数据
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_LHHDal(ProjectorInformation_LHH proje, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=proje.FuselageCode},
              new SqlParameter("@PostAgingTestTime",SqlDbType.DateTime){Value=proje.PostAgingTestTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 老化后测试查找是否是第二次测试
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public ProjectorInformation_LHH selectProjectorInformation_MainTable_LHHDal(string FuselageCode, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            ProjectorInformation_LHH pif = new ProjectorInformation_LHH();
            DateTime dt = new DateTime(1993,5,17);


            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=FuselageCode}  
            };

            using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text, pms))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pif.FuselageCode = reader.GetString(0);
                        pif.PostAgingTestTime = reader.IsDBNull(1) ? dt : reader.GetDateTime(1);
                        pif.PostAgingTestTime2 = reader.IsDBNull(2) ? null : reader.GetDateTime(2).ToString();
                    }
                }
                return pif;
            }
        }
        /// <summary>
        /// 亮度测试插入
        /// </summary>
        /// <param name="piLD"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_LDDal(ProjectorInformation_LD piLD,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piLD.FuselageCode},
                new SqlParameter("@IlluminationValue",SqlDbType.VarChar,50){Value=piLD.IlluminationValue},
                new SqlParameter("@WiredMAC",SqlDbType.VarChar,50){Value=piLD.WiredMAC},
                new SqlParameter("@wirelessMAC",SqlDbType.VarChar,50){Value=piLD.wirelessMAC},
                new SqlParameter("@LuminanceTestTime",SqlDbType.DateTime){Value=piLD.LuminanceTestTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 亮度测试更新
        /// </summary>
        /// <param name="piLD"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LDDal(ProjectorInformation_LD piLD, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piLD.FuselageCode},
                new SqlParameter("@IlluminationValue",SqlDbType.VarChar,50){Value=piLD.IlluminationValue},
                new SqlParameter("@WiredMAC",SqlDbType.VarChar,50){Value=piLD.WiredMAC},
                new SqlParameter("@wirelessMAC",SqlDbType.VarChar,50){Value=piLD.wirelessMAC},
                new SqlParameter("@LuminanceTestTime",SqlDbType.DateTime){Value=piLD.LuminanceTestTime},
                new SqlParameter("@ZhiDan",SqlDbType.VarChar,50){Value=piLD.ZhiDan},

            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 亮度测试更新
        /// </summary>
        /// <param name="piLD"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LDZDal(ProjectorInformation_LD piLD, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piLD.FuselageCode},
                new SqlParameter("@IlluminationValue",SqlDbType.VarChar,50){Value=piLD.IlluminationValue},
                new SqlParameter("@LuminanceTestTime",SqlDbType.DateTime){Value=piLD.LuminanceTestTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 亮度测试更新
        /// </summary>
        /// <param name="piLD"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_LDZ2Dal(ProjectorInformation_LD piLD, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piLD.FuselageCode},
                new SqlParameter("@IlluminationValue",SqlDbType.VarChar,50){Value=piLD.IlluminationValue},
                new SqlParameter("@WiredMAC",SqlDbType.VarChar,50){Value=piLD.WiredMAC},
                new SqlParameter("@wirelessMAC",SqlDbType.VarChar,50){Value=piLD.wirelessMAC},
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 维修插入
        /// </summary>
        /// <param name="piWX"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_WXDal(ProjectorInformation_WX piWX,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piWX.FuselageCode},
                new SqlParameter("@RepairText",SqlDbType.VarChar,500){Value=piWX.RepairText},
                new SqlParameter("@RepairTime",SqlDbType.DateTime){Value=piWX.RepairTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 维修更新
        /// </summary>
        /// <param name="piWX"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_WXDal(ProjectorInformation_WX piWX, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piWX.FuselageCode},
                new SqlParameter("@RepairText",SqlDbType.VarChar,500){Value="++"+piWX.RepairText},
                new SqlParameter("@RepairTime",SqlDbType.DateTime){Value=piWX.RepairTime},
                new SqlParameter("@AfterMaintenanceOpticalCode",SqlDbType.VarChar,80){Value=piWX.AfterMaintenanceOpticalCode},
                new SqlParameter("@AfterMaintenanceMainBoardCode",SqlDbType.VarChar,50){Value=piWX.AfterMaintenanceMainBoardCode},
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 包装插入
        /// </summary>
        /// <param name="piBZ"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainTable_BZDal(ProjectorInformation_BZ piBZ,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piBZ.FuselageCode},
                new SqlParameter("@PackingTime",SqlDbType.VarChar,500){Value=piBZ.PackingTime} 
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 包装更新
        /// </summary>
        /// <param name="piBZ"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainTable_BZDal(ProjectorInformation_BZ piBZ, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=piBZ.FuselageCode},
                new SqlParameter("@PackingTime",SqlDbType.VarChar,500){Value=piBZ.PackingTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 主表按机身码查询
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<ProjectorInformation_MainTable_STR> selectProjectorInformation_MainTableDal(string FuselageCode, string ZhiDan, string OpticalCode, string MainBoardCode, string WiredMAC, string wirelessMAC, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<ProjectorInformation_MainTable_STR> list = new List<ProjectorInformation_MainTable_STR>();
            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> wherelist = new List<string>();

            if (FuselageCode.Length > 0)
            {
                wherelist.Add(" FuselageCode=@FuselageCode ");
                listsqlpar.Add(new SqlParameter("@FuselageCode", SqlDbType.VarChar, 80) { Value = FuselageCode });
            }
            if (ZhiDan.Length > 0)
            {
                wherelist.Add(" ZhiDan=@ZhiDan ");
                listsqlpar.Add(new SqlParameter("@ZhiDan", SqlDbType.VarChar, 50) { Value = ZhiDan });
            }
            if (OpticalCode.Length > 0)
            {
                wherelist.Add(" OpticalCode=@OpticalCode ");
                listsqlpar.Add(new SqlParameter("@OpticalCode", SqlDbType.VarChar, 80) { Value = OpticalCode });
            }
            if (MainBoardCode.Length > 0)
            {
                wherelist.Add(" MainBoardCode=@MainBoardCode ");
                listsqlpar.Add(new SqlParameter("@MainBoardCode", SqlDbType.VarChar, 50) { Value = MainBoardCode });
            }
            if (WiredMAC.Length > 0)
            {
                wherelist.Add(" WiredMAC=@WiredMAC ");
                listsqlpar.Add(new SqlParameter("@WiredMAC", SqlDbType.VarChar, 50) { Value = WiredMAC });
            }
            if (wirelessMAC.Length > 0)
            {
                wherelist.Add(" wirelessMAC=@wirelessMAC ");
                listsqlpar.Add(new SqlParameter("@wirelessMAC", SqlDbType.VarChar, 50) { Value = wirelessMAC });
            }

            if (wherelist.Count > 0)
            {
                sql.Append(" where ");
                sql.Append(string.Join(" and ", wherelist));
            }

            try
            {
                if (listsqlpar.Count > 0)
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text, listsqlpar.ToArray()))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new ProjectorInformation_MainTable_STR()
                                {
                                    FuselageCode = reader.GetString(0),
                                    OpticalCode = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    PolishingMachineTime = reader.IsDBNull(2) ? "" : reader.GetDateTime(2).ToString(),
                                    PreAgingTestTime = reader.IsDBNull(3) ? "" : reader.GetDateTime(3).ToString(),
                                    AgeingBeginTime = reader.IsDBNull(4) ? "" : reader.GetDateTime(4).ToString(),
                                    AgeingEndTime = reader.IsDBNull(5) ? "" : reader.GetDateTime(5).ToString(),
                                    PostAgingTestTime = reader.IsDBNull(6) ? "" : reader.GetDateTime(6).ToString(),
                                    PreAgingTestTime2 = reader.IsDBNull(7) ? "" : reader.GetDateTime(7).ToString(),
                                    PostAgingTestTime2 = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString(),
                                    IlluminationValue = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                    WiredMAC = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                    wirelessMAC = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                    LuminanceTestTime = reader.IsDBNull(12) ? "" : reader.GetDateTime(12).ToString(),
                                    RepairText = reader.IsDBNull(13) ? "" : reader.GetString(13),
                                    RepairTime = reader.IsDBNull(14) ? "" : reader.GetDateTime(14).ToString(),
                                    PackingTime = reader.IsDBNull(15) ? "" : reader.GetDateTime(15).ToString(),
                                    MainBoardCode = reader.IsDBNull(16) ? "" : reader.GetString(16),
                                    MainBoardTime = reader.IsDBNull(17) ? "" : reader.GetDateTime(17).ToString(),
                                    ZhiDan = reader.IsDBNull(18) ? "" : reader.GetString(18),
                                    AfterMaintenanceOpticalCode=reader.IsDBNull(19)?"":reader.GetString(19),
                                    AfterMaintenanceMainBoardCode=reader.IsDBNull(20)?"":reader.GetString(20),
                                    LuminanceTestQTime = reader.IsDBNull(21) ? "" : reader.GetDateTime(21).ToString(),
                                });
                            }
                        }
                        return list;
                    }
                }
                else
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new ProjectorInformation_MainTable_STR()
                                {
                                    FuselageCode = reader.GetString(0),
                                    OpticalCode = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    PolishingMachineTime = reader.IsDBNull(2) ? "" : reader.GetDateTime(2).ToString(),
                                    PreAgingTestTime = reader.IsDBNull(3) ? "" : reader.GetDateTime(3).ToString(),
                                    AgeingBeginTime = reader.IsDBNull(4) ? "" : reader.GetDateTime(4).ToString(),
                                    AgeingEndTime = reader.IsDBNull(5) ? "" : reader.GetDateTime(5).ToString(),
                                    PostAgingTestTime = reader.IsDBNull(6) ? "" : reader.GetDateTime(6).ToString(),
                                    PreAgingTestTime2 = reader.IsDBNull(7) ? "" : reader.GetDateTime(7).ToString(),
                                    PostAgingTestTime2 = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString(),
                                    IlluminationValue = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                    WiredMAC = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                    wirelessMAC = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                    LuminanceTestTime = reader.IsDBNull(12) ? "" : reader.GetDateTime(12).ToString(),
                                    RepairText = reader.IsDBNull(13) ? "" : reader.GetString(13),
                                    RepairTime = reader.IsDBNull(14) ? "" : reader.GetDateTime(14).ToString(),
                                    PackingTime = reader.IsDBNull(15) ? "" : reader.GetDateTime(15).ToString(),
                                    MainBoardCode = reader.IsDBNull(16) ? "" : reader.GetString(16),
                                    MainBoardTime = reader.IsDBNull(17) ? "" : reader.GetDateTime(17).ToString(),
                                    ZhiDan = reader.IsDBNull(18) ? "" : reader.GetString(18),
                                    AfterMaintenanceOpticalCode = reader.IsDBNull(19) ? "" : reader.GetString(19),
                                    AfterMaintenanceMainBoardCode = reader.IsDBNull(20) ? "" : reader.GetString(20),
                                    LuminanceTestQTime = reader.IsDBNull(21) ? "" : reader.GetDateTime(21).ToString(),
                                });
                            }
                        }
                        return list;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 多条件删除
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="ZhiDan"></param>
        /// <param name="OpticalCode"></param>
        /// <param name="MainBoardCode"></param>
        /// <param name="WiredMAC"></param>
        /// <param name="wirelessMAC"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteProjectorInformation_MainTableDal(string FuselageCode, string ZhiDan, string OpticalCode, string MainBoardCode, string WiredMAC, string wirelessMAC, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> wherelist = new List<string>();

            if (FuselageCode.Length > 0)
            {
                wherelist.Add(" FuselageCode=@FuselageCode ");
                listsqlpar.Add(new SqlParameter("@FuselageCode", SqlDbType.VarChar, 80) { Value = FuselageCode });
            }
            if (ZhiDan.Length > 0)
            {
                wherelist.Add(" ZhiDan=@ZhiDan ");
                listsqlpar.Add(new SqlParameter("@ZhiDan", SqlDbType.VarChar, 50) { Value = ZhiDan });
            }
            if (OpticalCode.Length > 0)
            {
                wherelist.Add(" OpticalCode=@OpticalCode ");
                listsqlpar.Add(new SqlParameter("@OpticalCode", SqlDbType.VarChar, 80) { Value = OpticalCode });
            }
            if (MainBoardCode.Length > 0)
            {
                wherelist.Add(" MainBoardCode=@MainBoardCode ");
                listsqlpar.Add(new SqlParameter("@MainBoardCode", SqlDbType.VarChar, 50) { Value = MainBoardCode });
            }
            if (WiredMAC.Length > 0)
            {
                wherelist.Add(" WiredMAC=@WiredMAC ");
                listsqlpar.Add(new SqlParameter("@WiredMAC", SqlDbType.VarChar, 50) { Value = WiredMAC });
            }
            if (wirelessMAC.Length > 0)
            {
                wherelist.Add(" wirelessMAC=@wirelessMAC ");
                listsqlpar.Add(new SqlParameter("@wirelessMAC", SqlDbType.VarChar, 50) { Value = wirelessMAC });
            }

            if (wherelist.Count > 0)
            {
                sql.Append(" where ");
                sql.Append(string.Join(" and ", wherelist));
            }

            if (listsqlpar.Count > 0)
            {
                return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text, listsqlpar.ToArray());
            }
            else
            {
                return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text);
            }
        }
        /// <summary>
        /// 按机身码删除数据
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteProjectorInformation_MainTableFuselageCodeDal(string FuselageCode,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@FuselageCode",SqlDbType.VarChar,80){Value=FuselageCode}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
    }
}
