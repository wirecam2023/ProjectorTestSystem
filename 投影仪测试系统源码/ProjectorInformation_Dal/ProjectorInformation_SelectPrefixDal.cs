using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProjectorInformation_Model;

namespace ProjectorInformation_Dal
{
    public class ProjectorInformation_SelectPrefixDal
    {
        /// <summary>
        /// 前缀订单更新表
        /// </summary>
        /// <param name="projeSelect"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_SelectPrefixDal(ProjectorInformation_SelectPrefix projeSelect,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@TypeName",SqlDbType.VarChar,50){Value=projeSelect.TypeName},
                new SqlParameter("@Prefix_BodyCode",SqlDbType.VarChar,50){Value=projeSelect.Prefix_BodyCode},
                new SqlParameter("@Prefix_OpticalCode",SqlDbType.VarChar,50){Value=projeSelect.Prefix_OpticalCode},
                new SqlParameter("@Prefix_MotherboardEncoding",SqlDbType.VarChar,50){Value=projeSelect.Prefix_MotherboardEncoding},
                new SqlParameter("@ZhiDan",SqlDbType.VarChar,50){Value=projeSelect.ZhiDan},
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        public ProjectorInformation_SelectPrefix selecctProjectorInformation_SelectPrefixDal(string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            ProjectorInformation_SelectPrefix projeSelect = new ProjectorInformation_SelectPrefix();

            try
            {
                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            projeSelect.TypeName = reader.GetString(0);
                            projeSelect.Prefix_BodyCode = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            projeSelect.Prefix_OpticalCode = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            projeSelect.Prefix_MotherboardEncoding = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            projeSelect.ZhiDan = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        }
                    }
                    return projeSelect;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
