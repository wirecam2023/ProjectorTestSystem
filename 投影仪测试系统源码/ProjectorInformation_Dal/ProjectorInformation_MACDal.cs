using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProjectorInformation_Model;

namespace ProjectorInformation_Dal
{
    public class ProjectorInformation_MACDal
    {
        public List<ProjectorInformation_MAC> selectProjectorInformation_MAC(string MAC, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            List<ProjectorInformation_MAC> list = new List<ProjectorInformation_MAC>();

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@MAC",SqlDbType.VarChar,80){Value=MAC}
            };
            try
            {
                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text, pms))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProjectorInformation_MAC()
                            {
                                MAC=reader.GetString(0),
                                selectedTab=reader.GetString(1),
                                LHSX=reader.GetBoolean(2)
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

        public int insertProjectorInformation_MACDal(ProjectorInformation_MAC pmac,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@MAC",SqlDbType.VarChar,80){Value=pmac.MAC},
                new SqlParameter("@selectedTab",SqlDbType.VarChar,50){Value=pmac.selectedTab},
                new SqlParameter("@LHSX",SqlDbType.Bit){Value=pmac.LHSX}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        public int updateProjectorInformation_MACDal(ProjectorInformation_MAC pmac, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@MAC",SqlDbType.VarChar,80){Value=pmac.MAC},
                new SqlParameter("@selectedTab",SqlDbType.VarChar,50){Value=pmac.selectedTab},
                new SqlParameter("@LHSX",SqlDbType.Bit){Value=pmac.LHSX}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
    }
}
