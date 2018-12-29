using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommUtils.Data
{
    /// <summary>
    /// 闭合符对位置模型
    /// </summary>
    public class ClosSignLocationModel
    {
        /// <summary>
        /// 起始符位置
        /// </summary>
        public int BPos { get; set; }
        /// <summary>
        /// 结束符位置
        /// </summary>
        public int EPos { get; set; }
        /// <summary>
        /// 是否最里层标志
        /// </summary>
        public bool InnerMostFlag { get; set; }

    }
}
