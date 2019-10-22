using System;
using System.Collections.Generic;
using System.Text;

namespace PowerdesignWeb.Api.Models
{
    /// <summary>
    /// PDM实体集合
    /// </summary>
    public class PdmModels
    {
        /// <summary>
        /// 数据类型名称
        /// </summary>
        public string DataBaseName { get; set; }
        /// <summary>
        /// 表集合
        /// </summary>
        public List<TableInfo> Tables { get; set; }
    }
}
