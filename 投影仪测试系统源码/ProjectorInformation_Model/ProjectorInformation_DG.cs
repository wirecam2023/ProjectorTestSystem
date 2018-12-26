using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_DG
    { 
        /// <summary>
        /// 机身码
        /// </summary>
        public string FuselageCode { get; set; }
        /// <summary>
        /// 光机编码
        /// </summary>
        public string OpticalCode { get; set; }
        /// <summary>
        /// 主板编码
        /// </summary>
        public string MainBoardCode { get; set; }
        /// <summary>
        /// 打光机作业时间
        /// </summary>
        public DateTime PolishingMachineTime { get; set; }
        /// <summary>
        /// 打主板作业时间
        /// </summary>
        public DateTime MainBoardTime { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ZhiDan { get; set; }
    }
}
