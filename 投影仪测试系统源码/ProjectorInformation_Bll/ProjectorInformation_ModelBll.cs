using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectorInformation_Dal;
using ProjectorInformation_Model;

namespace ProjectorInformation_Bll
{
    public class ProjectorInformation_ModelBll
    {
        ProjectorInformation_ModelDal pimd = new ProjectorInformation_ModelDal();
        /// <summary>
        /// 主板表查询是否存在主板编码
        /// </summary>
        /// <param name="MainBoardCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool selectCountProjectorInformation_MainBoardBll(string MainBoardCode, string SQLCommand)
        {
            if (pimd.selectCountProjectorInformation_MainBoardDal(MainBoardCode, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 主板表插入数据
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainBoardBll(ProjectorInformation_MainBoard proje, string SQLCommand)
        {
            if (pimd.insertProjectorInformation_MainBoardDal(proje, SQLCommand) > 0)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 主板表更新数据
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainBoardBll(ProjectorInformation_MainBoard proje, string SQLCommand)
        {
            if (pimd.updateProjectorInformation_MainBoardDal(proje, SQLCommand) > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
