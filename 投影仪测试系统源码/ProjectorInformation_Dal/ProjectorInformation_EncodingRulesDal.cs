using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProjectorInformation_Model;

namespace ProjectorInformation_Dal
{
    public class ProjectorInformation_EncodingRulesDal
    {
        /// <summary>
        /// 查询前缀表
        /// </summary>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<ProjectorInformation_EncodingRules> selectProjectorInformation_EncodingRulesDal(string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            List<ProjectorInformation_EncodingRules> list = new List<ProjectorInformation_EncodingRules>();

            try
            {
                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProjectorInformation_EncodingRules
                            {
                                TypeName=reader.GetString(0),
                                Prefix_BodyCode=reader.IsDBNull(1)?"":reader.GetString(1),
                                Prefix_OpticalCode=reader.IsDBNull(2)?"":reader.GetString(2),
                                Prefix_MotherboardEncoding=reader.IsDBNull(3)?"":reader.GetString(3)
                            });
                        }
                    }
                    return list;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 插入前缀表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_EncodingRulesDal(List<ProjectorInformation_EncodingRules> list,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@TypeName",SqlDbType.VarChar,50){Value=list[0].TypeName},
                new SqlParameter("@Prefix_BodyCode",SqlDbType.VarChar,50){Value=list[0].Prefix_BodyCode},
                new SqlParameter("@Prefix_OpticalCode",SqlDbType.VarChar,50){Value=list[0].Prefix_OpticalCode},
                new SqlParameter("@Prefix_MotherboardEncoding",SqlDbType.VarChar,50){Value=list[0].Prefix_MotherboardEncoding},
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
    }
}
