using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_LHX
    {
        /// <summary>
        /// 机身码
        /// </summary>
        public string FuselageCode { get; set; }
        /// <summary>
        /// 老化（下架时间）结束老化时间
        /// </summary>
        public DateTime AgeingEndTime { get; set; }
    }
}
