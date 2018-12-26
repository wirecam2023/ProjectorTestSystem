using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectorInformation_Model
{
    public class ProjectorInformation_MAC
    {
        /// <summary>
        /// MAC地址
        /// </summary>
        public string MAC { get; set; }
        /// <summary>
        /// TAB选项卡的nama
        /// </summary>
        public string selectedTab { get; set; }
        /// <summary>
        /// 是否是老化上下架时间选项卡
        /// </summary>
        public bool LHSX { get; set; }
    }
}
