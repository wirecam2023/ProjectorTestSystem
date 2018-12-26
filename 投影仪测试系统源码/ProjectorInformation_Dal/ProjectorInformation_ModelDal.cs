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
    public class ProjectorInformation_ModelDal
    {
        /// <summary>
        /// 主板表查询是否存在主板编码
        /// </summary>
        /// <param name="MainBoardCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int selectCountProjectorInformation_MainBoardDal(string MainBoardCode,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
              new SqlParameter("@MainBoardCode",SqlDbType.VarChar,50){Value=MainBoardCode}  
            };

            return (int)SQLhelp.ExecuteScalar(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 主板表插入数据
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertProjectorInformation_MainBoardDal(ProjectorInformation_MainBoard proje, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@MainBoardCode",SqlDbType.VarChar,50){Value=proje.MainBoardCode},
                new SqlParameter("@MainBoardTime",SqlDbType.DateTime){Value=proje.MainBoardTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 主板表更新数据
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateProjectorInformation_MainBoardDal(ProjectorInformation_MainBoard proje, string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@MainBoardCode",SqlDbType.VarChar,50){Value=proje.MainBoardCode},
                new SqlParameter("@MainBoardTime",SqlDbType.DateTime){Value=proje.MainBoardTime}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

    }
}
