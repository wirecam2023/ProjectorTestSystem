using ProjectorInformation_Dal;
using ProjectorInformation_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Bll
{
    public class ProjectorInformation_SelectPrefixBll
    {
        ProjectorInformation_SelectPrefixDal projeSelectDal = new ProjectorInformation_SelectPrefixDal();
        /// <summary>
        /// 前缀订单号更新表
        /// </summary>
        /// <param name="projeSelect"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_SelectPrefixBll(ProjectorInformation_SelectPrefix projeSelect, string SQLCommand)
        {
            if (projeSelectDal.updateProjectorInformation_SelectPrefixDal(projeSelect, SQLCommand) > 0)
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 查询前缀订单号更新表
        /// </summary>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public ProjectorInformation_SelectPrefix selecctProjectorInformation_SelectPrefixBll(string SQLCommand)
        {
            return projeSelectDal.selecctProjectorInformation_SelectPrefixDal(SQLCommand);
        }
    }
}
