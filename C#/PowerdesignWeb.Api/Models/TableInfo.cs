using System;
using System.Collections.Generic;
using System.Text;

namespace PowerdesignWeb.Api.Models
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
       
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表代码,对应数据库表名
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { get; set; }

      
        /// <summary>
        /// 表列集合
        /// </summary>
        public List<ColumnInfo> Columns { get; set; }
       
       
        /// <summary>
        /// 表的描述=>PDM Notes.
        /// </summary>
        public string Description { get; set; }
        public string PrimaryKeyRefCode { get; set; }
        public List<PdmKey> Keys { get; set; }
    }
}
