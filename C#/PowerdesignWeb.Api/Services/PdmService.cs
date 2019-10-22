using PowerdesignWeb.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace PowerdesignWeb.Api.Services
{
    public class PdmService
    {
        private const string cTables = "c:Tables";


        /// <summary>
        /// 读取指定Pdm文件的实体集合
        /// </summary>
        /// <param name="PdmFile">Pdm文件名(全路径名)</param>
        /// <returns>实体集合</returns>
        public PdmModels ReadFromFile(string PdmFile)
        {
            XmlDocument xmlDoc;
            //加载文件.
            xmlDoc = new XmlDocument();
            xmlDoc.Load(PdmFile);
            //必须增加xml命名空间管理，否则读取会报错.
            XmlNamespaceManager xmlnsManager;
            xmlnsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlnsManager.AddNamespace("a", "attribute");
            xmlnsManager.AddNamespace("c", "collection");
            xmlnsManager.AddNamespace("o", "object");
            PdmModels theModels = new PdmModels();
            theModels.DataBaseName = xmlDoc.SelectSingleNode("//c:DBMS//o:Shortcut/a:Name", xmlnsManager).InnerText;
            //读取所有表节点
            XmlNodeList xnTablesList = xmlDoc.SelectNodes("//" + cTables, xmlnsManager);
            List<TableInfo> tables = new List<TableInfo>();
            foreach (XmlNode xmlTables in xnTablesList)
            {
                foreach (XmlNode xnTable in xmlTables.ChildNodes)
                {
                    //排除快捷对象.
                    if (xnTable.Name != "o:Shortcut")
                    {
                        tables.Add(GetTable(xnTable));
                    }
                }
            }
            theModels.Tables = tables;
            return theModels;

        }
        //初始化"o:Table"的节点
        private TableInfo GetTable(XmlNode xnTable)
        {
            TableInfo mTable = new TableInfo();
            XmlElement xe = (XmlElement)xnTable;
            //mTable.TableId = xe.GetAttribute("Id");
            XmlNodeList xnTProperty = xe.ChildNodes;
            foreach (XmlNode xnP in xnTProperty)
            {
                switch (xnP.Name)
                {                    
                    case "a:Name":
                        mTable.Name = xnP.InnerText;
                        break;
                    case "a:Code":
                        mTable.Code = xnP.InnerText;
                        break;                  
                    case "a:Comment":
                        mTable.Comment = xnP.InnerText;
                        break;
                    case "c:Keys":
                        InitKeys(xnP, mTable);
                        break;
                    case "c:Columns":
                        InitColumns(xnP, mTable);
                        break;
                    case "a:Description":
                        mTable.Description = xnP.InnerText;
                        break;
                }
            }
            mTable.Columns.ForEach(a => {
                a.IsPrimaryKey = mTable.Keys.Any(b => b.ColumnObjCodes.Contains(a.ColumnId));
            });
            return mTable;
        }

        //PDM文件中的日期格式采用的是当前日期与1970年1月1日8点之差的秒树来保存.
        private DateTime _BaseDateTime = new DateTime(1970, 1, 1, 8, 0, 0);
        private DateTime String2DateTime(string DateString)
        {
            Int64 theTicker = Int64.Parse(DateString);
            return _BaseDateTime.AddSeconds(theTicker);
        }

        //初始化"c:Columns"的节点
        private void InitColumns(XmlNode xnColumns, TableInfo pTable)
        {
            List<ColumnInfo> columnInfos = new List<ColumnInfo>();
            foreach (XmlNode xnColumn in xnColumns)
            {
                columnInfos.Add(GetColumn(xnColumn, pTable));
            }
            pTable.Columns = columnInfos;
        }

        //初始化c:Keys"的节点
        private void InitKeys(XmlNode xnKeys, TableInfo pTable)
        {
            List<PdmKey> list = new List<PdmKey>();
            foreach (XmlNode xnKey in xnKeys)
            {
                list.Add(GetKey(xnKey, pTable));
            }
            pTable.Keys = list;
        }
        
        private static Boolean ConvertToBooleanPG(Object obj)
        {
            if (obj != null)
            {
                string mStr = obj.ToString();
                mStr = mStr.ToLower();
                if ((mStr.Equals("y") || mStr.Equals("1")) || mStr.Equals("true"))
                {
                    return true;
                }
            }
            return false;
        }

        private ColumnInfo GetColumn(XmlNode xnColumn, TableInfo OwnerTable)
        {
            ColumnInfo mColumn = new ColumnInfo();
            XmlElement xe = (XmlElement)xnColumn;
            mColumn.ColumnId = xe.GetAttribute("Id");
            XmlNodeList xnCProperty = xe.ChildNodes;
            foreach (XmlNode xnP in xnCProperty)
            {
                switch (xnP.Name)
                {                  
                    case "a:Name":
                        mColumn.Name = xnP.InnerText;
                        break;
                    case "a:Code":
                        mColumn.Code = xnP.InnerText;
                        break;  
                    case "a:Comment":
                        mColumn.Comment = xnP.InnerText;
                        break;
                    case "a:DataType":
                        mColumn.DataType = xnP.InnerText;
                        break;
                    case "a:Length":
                        mColumn.Length = xnP.InnerText;
                        break;
                    case "a:Identity":
                        mColumn.Identity = ConvertToBooleanPG(xnP.InnerText);
                        break;
                    case "a:Mandatory":
                        mColumn.Mandatory = ConvertToBooleanPG(xnP.InnerText);
                        break;                   
                    case "a:Precision":
                        mColumn.Precision = xnP.InnerText;
                        break;
                }
            }
            return mColumn;
        }
     
        private void InitKeyColumns(XmlNode xnKeyColumns, PdmKey Key)
        {
            XmlElement xe = (XmlElement)xnKeyColumns;
            XmlNodeList xnKProperty = xe.ChildNodes;
            List<string> columnObjCode = new List<string>();
            foreach (XmlNode xnP in xnKProperty)
            {
                string theRef = ((XmlElement)xnP).GetAttribute("Ref");
                columnObjCode.Add(theRef);
            }
            Key.ColumnObjCodes = columnObjCode;
        }
        private PdmKey GetKey(XmlNode xnKey, TableInfo OwnerTable)
        {
            PdmKey mKey = new PdmKey();
            XmlElement xe = (XmlElement)xnKey;
            mKey.KeyId = xe.GetAttribute("Id");
            XmlNodeList xnKProperty = xe.ChildNodes;
            foreach (XmlNode xnP in xnKProperty)
            {
                switch (xnP.Name)
                {
                    
                    case "a:Name":
                        mKey.Name = xnP.InnerText;
                        break;
                    case "a:Code":
                        mKey.Code = xnP.InnerText;
                        break;                  
                 
                    case "c:Key.Columns":
                        InitKeyColumns(xnP, mKey);
                        break;
                }
            }
            return mKey;
        }
    }
}
