using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_WX
    {
        /// <summary>
        /// 机身码
        /// </summary>
        public string FuselageCode { get; set; }
        /// <summary>
        /// 维修描述
        /// </summary>
        public string RepairText { get; set; }

        /// <summary>
        /// 维修后光机编码
        /// </summary>
        public string AfterMaintenanceOpticalCode { get; set; }
        /// <summary>
        /// 维修后主板编码
        /// </summary>
        public string AfterMaintenanceMainBoardCode { get; set; }
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime RepairTime { get; set; }
    }
}
