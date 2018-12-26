using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_SelectPrefix
    {
        /// <summary>
        /// 前缀类型名
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 机身码前缀
        /// </summary>
        public string Prefix_BodyCode { get; set; }
        /// <summary>
        /// 光机码前缀
        /// </summary>
        public string Prefix_OpticalCode { get; set; }
        /// <summary>
        /// 主板编码前缀
        /// </summary>
        public string Prefix_MotherboardEncoding { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ZhiDan { get; set; }
    }
}
