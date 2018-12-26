using ProjectorInformation_Dal;
using ProjectorInformation_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Bll
{
    public class ProjectorInformation_EncodingRulesBll
    {
        ProjectorInformation_EncodingRulesDal pieDal = new ProjectorInformation_EncodingRulesDal();
        /// <summary>
        /// 查询前缀表
        /// </summary>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<ProjectorInformation_EncodingRules> selectProjectorInformation_EncodingRulesBll(string SQLCommand)
        {
            return pieDal.selectProjectorInformation_EncodingRulesDal(SQLCommand);
        }
        /// <summary>
        /// 插入前缀表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_EncodingRulesBll(List<ProjectorInformation_EncodingRules> list, string SQLCommand)
        {
            if (pieDal.insertProjectorInformation_EncodingRulesDal(list, SQLCommand) > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
