using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ProjectorInformation_Dal
{
    public class ProjectorInformation_AdminiDal
    {
        /// <summary>
        /// 查询登录密码
        /// </summary>
        /// <param name="txt_Password"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int selectCountProjectorInformation_AdminiDal(string txt_Password,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);

            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@AdminiPassword",SqlDbType.VarChar,50){Value=txt_Password}
            };

            return (int)SQLhelp.ExecuteScalar(sql, CommandType.Text, pms);
        }
    }
}
