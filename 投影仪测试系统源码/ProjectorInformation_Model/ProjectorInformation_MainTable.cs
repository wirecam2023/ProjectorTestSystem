using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_MainTable
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
        /// 打光机作业时间
        /// </summary>
        public DateTime PolishingMachineTime { get; set; }
        /// <summary>
        /// 主板编码
        /// </summary>
        public string MainBoardCode { get; set; }
        /// <summary>
        /// 打主板作业时间
        /// </summary>
        public DateTime MainBoardTime { get; set; }
        /// <summary>
        /// 老化前测试时间
        /// </summary>
        public DateTime PreAgingTestTime { get; set; }
        /// <summary>
        /// 老化前第二次测试时间
        /// </summary>
        public DateTime PreAgingTestTime2 { get; set; }
        /// <summary>
        /// 老化（上架时间）开始老化时间
        /// </summary>
        public DateTime AgeingBeginTime { get; set; }
        /// <summary>
        /// 老化（下架时间）结束老化时间
        /// </summary>
        public DateTime AgeingEndTime { get; set; }
        /// <summary>
        /// 老化后测试时间
        /// </summary>
        public DateTime PostAgingTestTime { get; set; }
        /// <summary>
        /// 老化后第二次测试时间
        /// </summary>
        public DateTime PostAgingTestTime2 { get; set; }
        /// <summary>
        /// 照度值
        /// </summary>
        public string IlluminationValue { get; set; }
        /// <summary>
        /// 有线MAC
        /// </summary>
        public string WiredMAC { get; set; }
        /// <summary>
        /// 无线MAC
        /// </summary>
        public string wirelessMAC { get; set; }
        /// <summary>
        /// 亮度测试
        /// </summary>
        public DateTime LuminanceTestTime { get; set; }
        /// <summary>
        /// 维修描述
        /// </summary>
        public string RepairText { get; set; }
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime RepairTime { get; set; }
        /// <summary>
        /// 包装时间
        /// </summary>
        public DateTime PackingTime { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ZhiDan { get; set; }
    }
}
