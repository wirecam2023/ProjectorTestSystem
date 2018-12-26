using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_LHS
    {
        /// <summary>
        /// 机身码
        /// </summary>
        public string FuselageCode { get; set; }
        /// <summary>
        /// 老化（上架时间）开始老化时间
        /// </summary>
        public DateTime AgeingBeginTime { get; set; }
    }
}
