using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectorInformation_Dal;
using ProjectorInformation_Model;

namespace ProjectorInformation_Bll
{
    public class ProjectorInformation_MACBll
    {
        ProjectorInformation_MACDal pimac = new ProjectorInformation_MACDal();
        /// <summary>
        /// 查询MAC地址对应的选项卡
        /// </summary>
        /// <param name="MAC"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<ProjectorInformation_MAC> selectProjectorInformation_MAC(string MAC, string SQLCommand)
        {
            return pimac.selectProjectorInformation_MAC(MAC, SQLCommand);
        }

        public bool insertProjectorInformation_MACBll(ProjectorInformation_MAC pmac, string SQLCommand)
        {
            if (pimac.insertProjectorInformation_MACDal(pmac, SQLCommand)>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateProjectorInformation_MACBll(ProjectorInformation_MAC pmac, string SQLCommand)
        {
            if (pimac.updateProjectorInformation_MACDal(pmac, SQLCommand) > 0)
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
