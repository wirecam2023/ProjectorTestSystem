using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectorInformation_Dal;

namespace ProjectorInformation_Bll
{
    public class ProjectorInformation_AdminiBll
    {
        ProjectorInformation_AdminiDal pad = new ProjectorInformation_AdminiDal();
        /// <summary>
        /// 登录管理员密码
        /// </summary>
        /// <param name="txt_Password"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool selectCountProjectorInformation_AdminiBll(string txt_Password, string SQLCommand)
        {
            if (pad.selectCountProjectorInformation_AdminiDal(txt_Password, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
