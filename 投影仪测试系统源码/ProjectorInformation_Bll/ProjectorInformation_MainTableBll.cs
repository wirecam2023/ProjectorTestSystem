using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectorInformation_Dal;
using ProjectorInformation_Model;

namespace ProjectorInformation_Bll
{
    public class ProjectorInformation_MainTableBll
    {
        ProjectorInformation_MainTableDal pmtd = new ProjectorInformation_MainTableDal();
        /// <summary>
        /// 查询有没有存在机身编码
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool selectCountProjectorInformation_MainTableBll(string FuselageCode, string ZhiDan, string SQLCommand)
        {
            if (pmtd.selectCountProjectorInformation_MainTableDal(FuselageCode, ZhiDan, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 打光机插入
        /// </summary>
        /// <param name="pdg"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTableBll(ProjectorInformation_DG pdg, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTableDal(pdg, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 打光机更新
        /// </summary>
        /// <param name="pdg"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTableBll(ProjectorInformation_DG pdg, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTableDal(pdg, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化前测试，如果主表没有该机身码，则插入
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_LHQBll(ProjectorInformation_LHQ proje, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_LHQDal(proje, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查找老化前测试是否是第二次测试
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public ProjectorInformation_LHQ selectProjectorInformation_MainTable_LHQBll(string FuselageCode, string SQLCommand)
        {
            return pmtd.selectProjectorInformation_MainTable_LHQDal(FuselageCode, SQLCommand);
        }
        /// <summary>
        /// 老化前测试更新
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="PreAgingTestTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_LHQBll(string FuselageCode, DateTime PreAgingTestTime, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LHQDal(FuselageCode,PreAgingTestTime, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化后测试更新
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="PreAgingTestTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_LHHBll(string FuselageCode, DateTime PreAgingTestTime, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LHHDal(FuselageCode, PreAgingTestTime, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化上架时间插入数据
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_LHSBll(ProjectorInformation_LHS lhs, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_LHSDal(lhs, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化上架时间更新
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_LHSBll(ProjectorInformation_LHS lhs, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LHSDal(lhs, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化下架时间更新
        /// </summary>
        /// <param name="lhx"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_LHXBll(ProjectorInformation_LHX lhx, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LHXDal(lhx, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化下架时间插入
        /// </summary>
        /// <param name="lhx"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_LHXBll(ProjectorInformation_LHX lhx, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_LHXDal(lhx, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化后插入数据
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_LHHBll(ProjectorInformation_LHH proje, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_LHHDal(proje, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 老化后测试查找是否是第二次测试
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public ProjectorInformation_LHH selectProjectorInformation_MainTable_LHHBll(string FuselageCode, string SQLCommand)
        {
            return pmtd.selectProjectorInformation_MainTable_LHHDal(FuselageCode, SQLCommand);
        }
        /// <summary>
        /// 亮度测试插入
        /// </summary>
        /// <param name="piLD"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_LDBll(ProjectorInformation_LD piLD, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_LDDal(piLD, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 亮度测试更新
        /// </summary>
        /// <param name="piLD"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_LDBll(ProjectorInformation_LD piLD, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LDDal(piLD, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateProjectorInformation_MainTable_LDZBll(ProjectorInformation_LD piLD, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LDZDal(piLD, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateProjectorInformation_MainTable_LDZ2Bll(ProjectorInformation_LD piLD, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_LDZ2Dal(piLD, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 维修插入
        /// </summary>
        /// <param name="piWX"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_WXBll(ProjectorInformation_WX piWX, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_WXDal(piWX, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 维修更新
        /// </summary>
        /// <param name="piWX"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_WXBll(ProjectorInformation_WX piWX, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_WXDal(piWX, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 包装插入
        /// </summary>
        /// <param name="piBZ"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertProjectorInformation_MainTable_BZDal(ProjectorInformation_BZ piBZ, string SQLCommand)
        {
            if (pmtd.insertProjectorInformation_MainTable_BZDal(piBZ, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 包装更新
        /// </summary>
        /// <param name="piBZ"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateProjectorInformation_MainTable_BZDal(ProjectorInformation_BZ piBZ, string SQLCommand)
        {
            if (pmtd.updateProjectorInformation_MainTable_BZDal(piBZ, SQLCommand) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 主表按机身码查询
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<ProjectorInformation_MainTable_STR> selectProjectorInformation_MainTableBll(string FuselageCode, string ZhiDan, string OpticalCode, string MainBoardCode, string WiredMAC, string wirelessMAC, string SQLCommand)
        {
            return pmtd.selectProjectorInformation_MainTableDal(FuselageCode,ZhiDan,OpticalCode,MainBoardCode,WiredMAC,wirelessMAC, SQLCommand);
        }
        /// <summary>
        /// 多条件删除
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="ZhiDan"></param>
        /// <param name="OpticalCode"></param>
        /// <param name="MainBoardCode"></param>
        /// <param name="WiredMAC"></param>
        /// <param name="wirelessMAC"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool deleteProjectorInformation_MainTableBll(string FuselageCode, string ZhiDan, string OpticalCode, string MainBoardCode, string WiredMAC, string wirelessMAC, string SQLCommand)
        {
            if (pmtd.deleteProjectorInformation_MainTableDal(FuselageCode, ZhiDan, OpticalCode, MainBoardCode, WiredMAC, wirelessMAC, SQLCommand) > 0)
            {
                return true;
            }
            else return false;
        }
                /// <summary>
        /// 按机身码删除数据
        /// </summary>
        /// <param name="FuselageCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool deleteProjectorInformation_MainTableFuselageCodeBll(string FuselageCode, string SQLCommand)
        {
            if (pmtd.deleteProjectorInformation_MainTableFuselageCodeDal(FuselageCode, SQLCommand) > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
