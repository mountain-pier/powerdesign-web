using System;
using System.Collections.Generic;
using System.Text;

namespace PowerdesignWeb.Api.Models
{
    /// <summary>
    /// 表列信息
    /// </summary>
    public class ColumnInfo
    {

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 列标识
        /// </summary>
        public string ColumnId { get; set; }

        public string Name { get; set; }
        
        public string Code { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
     
        public string Length { get; set; }

        /// <summary>
        /// 是否自增量
        /// </summary>
        public bool Identity { get; set; }
        public bool Mandatory { get; set; }       
        
        /// <summary>
        /// 精度
        /// </summary>
        public string Precision { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
