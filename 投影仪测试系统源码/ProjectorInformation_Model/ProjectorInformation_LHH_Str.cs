using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_LHH_Str
    {
        /// <summary>
        /// 机身码
        /// </summary>
        public string FuselageCode { get; set; }
        /// <summary>
        /// 老化后测试时间
        /// </summary>
        public string PostAgingTestTime { get; set; }
        /// <summary>
        /// 老化后第二次测试时间
        /// </summary>
        public string PostAgingTestTime2 { get; set; }
    }
}
