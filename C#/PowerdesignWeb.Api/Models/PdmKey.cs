using System;
using System.Collections.Generic;
using System.Text;

namespace PowerdesignWeb.Api.Models
{
    /// <summary>
    /// 关键字
    /// </summary>
    public class PdmKey
    {

        public string KeyId { get; set; }
        /// <summary>
        /// Key名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Key代码,对应数据库中的Key.
        /// </summary>
        public string Code { get; set; }

        private List<string> _ColumnObjCodes = new List<string>();
        /// <summary>
        /// Key涉及的列代码，根据辞可访问到列信息.对应列的ColumnId
        /// </summary>
        public List<string> ColumnObjCodes { get; set; }
    }
}
