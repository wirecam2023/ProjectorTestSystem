using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_LD
    {
        /// <summary>
        /// 机身码
        /// </summary>
        public string FuselageCode { get; set; }
        /// <summary>
        /// 照度值
        /// </summary>
        public string IlluminationValue { get; set; }
        /// <summary>
        /// 无线MAC
        /// </summary>
        public string wirelessMAC { get; set; }
        /// <summary>
        /// 有线MAC
        /// </summary>
        public string WiredMAC { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ZhiDan { get; set; }
        /// <summary>
        /// 亮度测试
        /// </summary>
        public DateTime LuminanceTestTime { get; set; }
    }
}
