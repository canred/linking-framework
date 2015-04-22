using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Data.Common;
using Microsoft.Win32;



namespace Gencode
{
    public partial class Form1 : Form
    {
        List<XDBSource> XDBs = new List<XDBSource>();
        string PATH_RECORD = "";
        string PATH_TABLE = "";
        string PATH_MODEL = "";
        string MODEL_NAME = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        { 
        }
        private void builder()
        {
            //string xsdPath = @"D:\TestAddIn\xsd.xsd";
            //string xsdPath = @"D:\TestAddIn\DataSet2.xsd";
            
            string xsdPath = this.txtModelPath.Text;
            if (xsdPath.Trim().Length == 0) {
                MessageBox.Show("Please choose XSD File Path , First.");
                return;
            }
            //string xsdPath = @"D:\TestAddIn\Test3.xsd";
            //string xsdPath = @"D:\TestAddIn\Test4.xsd";
            System.IO.StreamReader sr = new System.IO.StreamReader(xsdPath);
            string newXml = "";
            string tmp = "";
            #region paser
            try
            {
                int readLine = 0;
                
                while ((tmp = sr.ReadLine()) != null)
                {
                    readLine++;
                    Application.DoEvents();
                    this.toolStripStatusLabel1.Text = "正在解析xsd資料行…【" + readLine.ToString() + "】";                   
                    Application.DoEvents();

                    tmp = tmp.Replace("xs:", "");
                    string endFlag = "";
                    if (tmp.Trim().EndsWith("/>"))
                    {
                        endFlag = "/>";
                    }
                    else if (tmp.Trim().EndsWith(">"))
                    {
                        endFlag = ">";
                    }
                    if (tmp.IndexOf(" targetNamespace") >= 0)
                    {
                        tmp = tmp.Substring(0, tmp.IndexOf(" targetNamespace")) + endFlag;
                    }

                    if (tmp.IndexOf("xmlns=") >= 0)
                    {
                        tmp = tmp.Substring(0, tmp.IndexOf("xmlns=")) + endFlag;
                    }



                    if (tmp.IndexOf(" msdata:") >= 0)
                    {                      
                        tmp = tmp.Replace(" msdata:", " ");
                    }



                    if (tmp.IndexOf("<msdata:Relationship") >= 0)
                    {
                        tmp = tmp.Replace("<msdata:Relationship", "<Relationship");
                    }
                    tmp = tmp.Replace(" msprop:"," ");
               

                    newXml += tmp;


                }
                sr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            sr = null;
            System.Xml.XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(newXml);
            var aa = xmlDocument.DocumentElement;
            XPathNavigator nav = xmlDocument.CreateNavigator();
            XPathExpression expr;
            expr = nav.Compile("/schema/annotation/appinfo/DataSource/Tables/TableAdapter");
            XPathNodeIterator iterator = nav.Select("//DbSource");
            XDBs = new List<XDBSource>();
            //build table
            #region build table
            while (iterator.MoveNext())
            {
                XDBSource xdb = new XDBSource();
                
                xdb.objectName = iterator.Current.GetAttribute("DbObjectName", "").ToString();
                xdb.objectName = xdb.objectName.Replace("\"", "");
                xdb.objectName = xdb.objectName.Replace("[", "");
                xdb.objectName = xdb.objectName.Replace("]", "");
                xdb.objectView = iterator.Current.GetAttribute("DbObjectType", "").ToString();
                if (xdb.objectName == "" || xdb.objectView.ToUpper() == "UNKNOWN") {
                    var N1 = iterator.Clone();
                    N1.Current.MoveToFirstChild();//SelectCommand
                    N1.Current.MoveToFirstChild();//DbCommand
                    N1.Current.MoveToFirstChild();//CommandText
                    var SQL_SELECT = N1.Current.InnerXml;
                    var s = SQL_SELECT.LastIndexOf(" FROM ");

                    string[] info = new string[2];
                    if(SQL_SELECT.IndexOf(".")!=-1){
                        info = SQL_SELECT.Substring(s + 6).Split('.');                        
                    }else{
                        /*MySQL*/
                        //xdb.objectName = info[0] + "." + info[1];
                        info[0] = txtNamespace.Text;
                        info[1] = SQL_SELECT.Substring(s + 6).Replace("`", "");
                    }
                    xdb.objectName = info[0] + "." + info[1];
                    xdb.objectView = "View";
                }

                if (xdb.objectName.Split('.').Length > 2) {
                    xdb.objectName = xdb.objectName.Split('.')[0] + "." + xdb.objectName.Split('.')[2];
                }
               
                Application.DoEvents();
                this.toolStripStatusLabel1.Text = "正在準備程式相關物件【" + xdb.objectName + "】";
                Application.DoEvents();

                XDBs.Add(xdb);
            }
            #endregion
            //build column
            #region build column
            foreach (XDBSource table in XDBs)
            {
                string tableName = table.objectName;

                Application.DoEvents();
                this.toolStripStatusLabel1.Text = "正在建立資料欄位資訊【" + tableName + "】\n";
                Application.DoEvents();

                if (tableName.IndexOf(".") > 0)
                {
                    tableName = tableName.Split('.')[1];
                }
                iterator = nav.Select("//element[@name='" + tableName + "']");
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                bool genColumn = false;
                #region GendColumn 1
                while (iterator.MoveNext())
                {
                    genColumn = true;
                    iterator.Current.MoveToFirstChild();//complextType
                    iterator.Current.MoveToFirstChild();//sequence
                    iterator.Current.MoveToChild(XPathNodeType.All);//element
                    string newTag = "", oldTag = "";
                    while (iterator.Count >= 1)
                    {
                        newTag = iterator.Current.GetAttribute("name", "");
                        if (ht.ContainsKey(newTag) == true)
                        {
                            break;
                        }
                        else {
                            ht.Add(newTag,newTag);
                        }
                        if (newTag != oldTag)
                        {
                            XDBSource.Xcolumn col = new XDBSource.Xcolumn();
                            string itemType = iterator.Current.GetAttribute("type", "");
                            if (itemType != null && itemType != "")
                            {
                                col.columnName = newTag;
                                col.columnType = itemType;
                            }
                            else
                            {
                                col.columnName = newTag;
                                var navClone = iterator.Current.Clone();
                                navClone.MoveToChild(XPathNodeType.All);//simpleType
                                navClone.MoveToChild(XPathNodeType.All);//restriction
                                col.columnType = navClone.GetAttribute("base", "");
                                while (col.columnType =="") {
                                    navClone.MoveToChild(XPathNodeType.All);
                                    col.columnType = navClone.GetAttribute("base", "");
                                }
                                navClone.MoveToChild(XPathNodeType.All);//maxLength
                                col.columnLimit = Convert.ToInt32(navClone.GetAttribute("value", ""));

                            }
                            table.cols.Add(col);
                            Console.WriteLine(newTag);
                            oldTag = newTag;
                            iterator.Current.MoveToNext();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                #endregion

               #region genColumn 2
                if (genColumn == false)
                {
                    iterator = nav.Select("//element[@Generator_UserTableName='" + tableName + "']");
                    while (iterator.MoveNext())
                    {
                        genColumn = true;
                        iterator.Current.MoveToFirstChild();//complextType
                        iterator.Current.MoveToFirstChild();//sequence
                        iterator.Current.MoveToChild(XPathNodeType.All);//element
                        string newTag = "", oldTag = "";
                        while (iterator.Count >= 1)
                        {
                            newTag = iterator.Current.GetAttribute("name", "");
                            if (ht.ContainsKey(newTag) == true)
                            {
                                break;
                            }
                            else
                            {
                                ht.Add(newTag, newTag);
                            }
                            if (newTag != oldTag)
                            {
                                XDBSource.Xcolumn col = new XDBSource.Xcolumn();
                                string itemType = iterator.Current.GetAttribute("type", "");
                                if (itemType != null && itemType != "")
                                {
                                    col.columnName = newTag;
                                    col.columnType = itemType;
                                }
                                else
                                {
                                    col.columnName = newTag;
                                    var navClone = iterator.Current.Clone();
                                    navClone.MoveToChild(XPathNodeType.All);//simpleType
                                    navClone.MoveToChild(XPathNodeType.All);//restriction
                                    col.columnType = navClone.GetAttribute("base", "");
                                    navClone.MoveToChild(XPathNodeType.All);//maxLength
                                    col.columnLimit = Convert.ToInt32(navClone.GetAttribute("value", ""));

                                }
                                table.cols.Add(col);
                                Console.WriteLine(newTag);
                                oldTag = newTag;
                                iterator.Current.MoveToNext();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
               #endregion

            }
            #endregion
            //build pk
            #region pk
            foreach (XDBSource table in XDBs)
            {
                string tableName = table.objectName;
                Application.DoEvents();
                this.toolStripStatusLabel1.Text = "正在處理資料PK欄位資訊【" + tableName + "】\n";
                Application.DoEvents();
                if (tableName.IndexOf(".") > 0)
                {
                    tableName = tableName.Split('.')[1];
                }
                iterator = nav.Select("//unique");
                while (iterator.MoveNext())
                {
                    string constraintName = iterator.Current.GetAttribute("name", "");
                    var navSelect = iterator.Current.Clone();
                    navSelect.MoveToChild(XPathNodeType.All);//selector
                    string selector = navSelect.GetAttribute("xpath", "");
                    if (selector != null && selector != "")
                    {
                        selector = selector.Split(':')[1];
                    }

                    while (selector.IndexOf("_x0024_") >= 0) {
                        selector = selector.Replace("_x0024_", "$");
                    }

                    while (selector.IndexOf("_x0023_") >= 0)
                    {
                        selector = selector.Replace("_x0023_", "$");
                    }

                    while (navSelect.MoveToNext(XPathNodeType.All))
                    {
                        string column = navSelect.GetAttribute("xpath", "");
                        if (column != null && column != "")
                        {
                            column = column.Split(':')[1];
                        }
                        if (selector.ToUpper() == tableName.ToUpper())
                        {
                            XDBSource.PKcolumn pk = new XDBSource.PKcolumn();
                            pk.columnName = column;
                            pk.constraintName = constraintName;
                            table.ConstraintCol.Add(pk);
                        }
                    }
                }

            }
            #endregion
            //build relation
            #region relation
            foreach (XDBSource table in XDBs)
            {
                string tableName = table.objectName;
                Application.DoEvents();
                this.toolStripStatusLabel1.Text = "正在資料Relation資訊【" + tableName + "】";
                Application.DoEvents();
               
                if (tableName.IndexOf(".") > 0)
                {
                    tableName = tableName.Split('.')[1];
                }
                iterator = nav.Select("//Relationship");
                while (iterator.MoveNext())
                {

                    string relationshipName = iterator.Current.GetAttribute("name", "");
                    string parent = iterator.Current.GetAttribute("parent", "");
                    string child = iterator.Current.GetAttribute("child", "");
                    /*特殊字元過慮*/
                    while (parent.IndexOf("_x0024_") >= 0) {
                        parent = parent.Replace("_x0024_", "$");    
                    }
                    while (child.IndexOf("_x0024_") >= 0)
                    {
                        child = child.Replace("_x0024_", "$");
                    }

                    while (parent.IndexOf("_x0023_") >= 0)
                    {
                        parent = parent.Replace("_x0023_", "$");
                    }
                    while (child.IndexOf("_x0023_") >= 0)
                    {
                        child = child.Replace("_x0023_", "$");
                    }
                    string parentkey = iterator.Current.GetAttribute("parentkey", "");
                    string childkey = iterator.Current.GetAttribute("childkey", "");


                    if (parent.ToUpper() == tableName.ToUpper())
                    {
                        XDBSource.Relation rel = new XDBSource.Relation();
                        rel.RelationName = relationshipName;
                        rel.Parent = parent;
                        rel.Child = child;

                        XDBSource.RelationColumn relParent = new XDBSource.RelationColumn();
                        XDBSource.RelationColumn relchild = new XDBSource.RelationColumn();

                        foreach (string p in parentkey.Split(' '))
                        {
                            relParent = new XDBSource.RelationColumn();
                            relParent.columnName = p;
                            rel.ParentColumnName.Add(relParent);

                            Application.DoEvents();
                            this.toolStripStatusLabel1.Text = "正在資料Relation資訊【" + tableName + "】"+p+"完成";
                            Application.DoEvents();
                        }

                        foreach (string p in childkey.Split(' '))
                        {
                            relParent = new XDBSource.RelationColumn();
                            relParent.columnName = p;
                            rel.ChildColumnName.Add(relParent);

                            Application.DoEvents();
                            this.toolStripStatusLabel1.Text = "正在資料Relation資訊【" + tableName + "】" + p + "完成";
                            Application.DoEvents();
                        }
                        table.rels.Add(rel);
                    }
                }
            }
            #endregion

            foreach (var db in XDBs) {
                db.objectName = db.objectName.Replace("\"", "");
            }
            System.IO.FileInfo fi = new System.IO.FileInfo(xsdPath);
            string strModelName = fi.Name;
            MODEL_NAME = strModelName.Split('.')[0];
            txtOutputPath.Text = txtProjectRootPath.Text + "\\Model\\" + strModelName.Split('.')[0] + "\\";
            fi = null;
            //DateTime sTime = DateTime.Now;            
            Application.DoEvents();
            this.toolStripStatusLabel1.Text = "CodeBuilder...";
            Application.DoEvents();
            builderCode();
            //DateTime eTime = DateTime.Now;

            //var totalTime = eTime - sTime;
            
            Application.DoEvents();
            this.toolStripStatusLabel1.Text = "Finish...";
            Application.DoEvents();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            builderCode();
        }
        private void builderCode()
        {

            rtbResult.Text += "在解析XSD中發現了" + XDBs.Count.ToString() + "個物件(Table/view)\n";
            if (XDBs.Count == 0)
            {
                rtbResult.Text += "解析不到任何模型不產生程式!";
                return;
            }
            rtbResult.Text += "物件清單：\n";
            rtbResult.Text += "------------------------------------------------------------------\n";
            foreach (var item in XDBs)
            {
                rtbResult.Text += "【" + item.objectView + "】" + item.objectName + "\n";
            }
            rtbResult.Text += "------------------------------------------------------------------\n";

            rtbResult.Text += "目錄檢查\n";
            rtbResult.Text += "------------------------------------------------------------------\n";
            rtbResult.Text += txtProjectRootPath.Text + "存在嗎?\n";
            PATH_MODEL = this.txtOutputPath.Text;
            if (System.IO.Directory.Exists(this.txtOutputPath.Text))
            {
                rtbResult.Text += "OK\n";
            }
            else
            {
                System.IO.Directory.CreateDirectory(txtOutputPath.Text);
                rtbResult.Text += "建立新資料夾【" + txtOutputPath.Text + "】--OK\n";
            }
            string path = txtOutputPath.Text;
            path += "Table\\";
            PATH_TABLE = path;
            rtbResult.Text += path + "存在嗎?\n";
            if (System.IO.Directory.Exists(path))
            {
                rtbResult.Text += "OK\n";
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
                rtbResult.Text += "建立新資料夾【" + path + "】--OK\n";
            }

            path += "Record\\";
            rtbResult.Text += path + "存在嗎?\n";
            PATH_RECORD = path;
            if (System.IO.Directory.Exists(path))
            {
                rtbResult.Text += "OK\n";
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
                rtbResult.Text += "建立新資料夾【" + path + "】--OK\n";
            }

            rtbResult.Text += "\n\n開始產出程式：\n";
            rtbResult.Text += "==================================================================\n";
            foreach (var item in XDBs)
            {
                Application.DoEvents();
                this.toolStripStatusLabel1.Text = "CodeBuilder...【"+item.objectName+"】";
                Application.DoEvents();

                rtbResult.Text += "【" + item.objectView + "】" + item.objectName + "\n";
                genCode(item, XDBs);
            }
            Application.DoEvents();
            this.toolStripStatusLabel1.Text = "CodeBuilder...Model Object";
            Application.DoEvents();
            genCodeModel(XDBs);
            genCodeModelCust(XDBs);
            Application.DoEvents();
            this.toolStripStatusLabel1.Text = "Finish...";
            Application.DoEvents();
        }

        private void genCodeModel(IList<XDBSource> xdb) {
            var fileFullPath = PATH_MODEL + getModelFileName(MODEL_NAME);
            string modelName = getModelFileName(MODEL_NAME).Split('.')[0];
            var modelName_short = modelName.Substring(0, modelName.Length - 5);
            modelName = modelName.Substring(0, modelName.Length - 5);
            string classTableNameFix = txtNamespace.Text + ".Model." + modelName_short + ".Table";

            string objectName = "";
            foreach (var item in xdb) {
                objectName = item.objectName.ToUpper().Split('.')[0].ToUpper();
            }
            

            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileFullPath);
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Linq;");
            sw.WriteLine("using System.Text;");
            sw.WriteLine("using LK.Attribute;");
            sw.WriteLine("using LK.DB;  ");
            sw.WriteLine("using log4net;  ");
            sw.WriteLine("using System.Reflection;  ");
            sw.WriteLine("using LK.DB.SQLCreater;  ");


            sw.WriteLine("using " + txtNamespace.Text + ".Model." + modelName_short + ".Table;");
            sw.WriteLine("namespace " + txtNamespace.Text + ".Model." + modelName);
            sw.WriteLine("{");
            sw.WriteLine("\t[ModelName(\"" + modelName + "\")]");
            sw.WriteLine("\t[LkDataBase(\"" + objectName + "\")]");
            sw.WriteLine("\tpublic partial class " + modelName+"Model");
            sw.WriteLine("\t{");
            sw.WriteLine("\t\tpublic new static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);");
            
            sw.WriteLine("\t\tprivate LK.Config.DataBase.IDataBaseConfigInfo dbc = null;");
            sw.WriteLine("\t\tpublic " + modelName + "Model(){}");

            /*********************************************************************/
            System.Collections.Hashtable htMethodName = new System.Collections.Hashtable();
            foreach (var table in xdb)
            {
                string pkString="";
                string strTableName = this.getTableFileName(table.objectName).Split('.')[0];
                foreach (var tmp1 in table.ConstraintCol)
                { 
                  //table tmp1
                    pkString += this.getFormatString(tmp1.columnName) + "_And_";                  
                }
                if (pkString.EndsWith("_And_"))
                {
                    pkString = pkString.Substring(0, pkString.Length - 5);
                }

                #region 產生PK相關的查詢
                if (pkString != "")
                {
                    string parameterString = "";
                    string parameterForMethod = "";
                    foreach (var pk in table.ConstraintCol)
                    {

                        string conditionType = "";
                        foreach (var col in table.cols)
                        {
                            if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                            {
                                conditionType = columnType2objectType(col.columnType);
                                parameterString += conditionType + " p" + pk.columnName.ToUpper() + ",";
                                parameterForMethod += "p" + pk.columnName.ToUpper() + ",";
                            }

                        }
                    }
                    if (parameterString.EndsWith(","))
                    {
                        parameterString = parameterString.Substring(0, parameterString.Length - 1);
                    }
                    if (parameterForMethod.EndsWith(","))
                    {
                        parameterForMethod = parameterForMethod.Substring(0, parameterForMethod.Length - 1);
                    }

                    htMethodName.Add(this.getTableMethodName(table.objectName, pkString), "");
                    sw.WriteLine("\t\t/*Templete Model A001*/");
                    sw.WriteLine("\t\tpublic " + classTableNameFix +"."+ this.FormatClassName( strTableName) + " " + this.FormatClassName( this.getTableMethodName(table.objectName, pkString)) + "(" + parameterString + "){");
                    sw.WriteLine("\t\t\ttry{");
                    sw.WriteLine("\t\t\t\tdbc = LK.Config.DataBase.Factory.getInfo();");
                    sw.WriteLine("\t\t\t\t" + classTableNameFix + "." + this.FormatClassName(strTableName )+ " " + this.FormatClassName(strTableName.ToLower()) + " = new " + classTableNameFix + "." + this.FormatClassName(strTableName) + "(dbc);");

                    sw.WriteLine("\t\t\t\t" + this.FormatClassName(strTableName.ToLower()) + ".Fill_By_PK(" + parameterForMethod + ");");
                    sw.WriteLine("\t\t\t\treturn " + this.FormatClassName(strTableName.ToLower() )+ ";");                  
                    sw.WriteLine("\t\t\t}");
                    sw.WriteLine("\t\t\tcatch (Exception ex){");
                    sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);"); 
                    sw.WriteLine("\t\t\t\tthrow ex;");
                    sw.WriteLine("\t\t\t}"); 
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("");
                }
                #endregion
                
                /*getTableNameBy_Uuid_And_Name*/
                foreach (var fkTable in table.rels)
                {
                    string parameterString = "";
                    string parameterForMethod = "";
                    pkString = "";
                    foreach (var tmp1 in fkTable.ParentColumnName)
                    {
                        //table tmp1
                        pkString += tmp1.columnName + "_AND_";
                    }
                    if (pkString.EndsWith("_AND_"))
                    {
                        pkString = pkString.Substring(0, pkString.Length - 5);
                    }
                    foreach (var pk in fkTable.ParentColumnName)
                    {
                        string conditionType = "";
                        foreach (var col in table.cols)
                        {
                            if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                            {
                                conditionType = columnType2objectType(col.columnType);
                                parameterString += conditionType + " p" + pk.columnName.ToUpper() + ",";
                                parameterForMethod += "p" + pk.columnName.ToUpper() + ",";
                            }

                        }
                    }
                    if (parameterString.EndsWith(","))
                    {
                        parameterString = parameterString.Substring(0, parameterString.Length - 1);
                    }
                    if (parameterForMethod.EndsWith(","))
                    {
                        parameterForMethod = parameterForMethod.Substring(0, parameterForMethod.Length - 1);
                    }
                }
            }          
            sw.WriteLine("\t}");
            sw.WriteLine("}");
            sw.Flush();
            sw.Close();
            sw = null;
        }



        private void genCodeModelCust(IList<XDBSource> xdb)
        {
            var fileFullPath = PATH_MODEL + getModelFileNameCust(MODEL_NAME);

            bool needGen = false;
            System.IO.StreamReader sr = new System.IO.StreamReader(fileFullPath);
            if (sr.ReadToEnd().Trim().Length == 0) {
                needGen = true;
            }
            sr.Close();
            sr = null;
            if (needGen == true)
            {


                string modelName = getModelFileName(MODEL_NAME).Split('.')[0];
                var modelName_short = modelName.Substring(0, modelName.Length - 5);
                modelName = modelName.Substring(0, modelName.Length - 5);


                string objectName = "";
                foreach (var item in xdb)
                {
                    objectName = item.objectName.ToUpper().Split('.')[0].ToUpper();
                }


                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileFullPath);
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("using System.Linq;");
                sw.WriteLine("using System.Text;");
                sw.WriteLine("using LK.Attribute;");
                sw.WriteLine("using LK.DB;  ");
                sw.WriteLine("using LK.DB.SQLCreater;  ");


                sw.WriteLine("using " + txtNamespace.Text + ".Model." + modelName_short + ".Table;");
                sw.WriteLine("namespace " + txtNamespace.Text + ".Model." + modelName);
                sw.WriteLine("{");
                sw.WriteLine("\tpublic partial class " + modelName + "Model");
                sw.WriteLine("\t{");
              
                sw.WriteLine("\t}");
                sw.WriteLine("}");
                sw.Flush();
                sw.Close();
                sw = null;
            }
        }
        private void genCode(XDBSource source, List<XDBSource> xdb)
        {
            /*1建立檔案的結構Record*/
            //rtbResult.Text += getRecordFileName(source.objectName)+"\n";
            if (System.IO.File.Exists(PATH_RECORD + getRecordFileName(source.objectName)) == false)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(PATH_RECORD + getRecordFileName(source.objectName),false, Encoding.ASCII);
                sw.Flush();
                sw.Close();
                sw = null;
                          
            }
            /*2建立檔案的結構Table*/
            if (System.IO.File.Exists(PATH_TABLE + getTableFileName(source.objectName)) == false)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(PATH_TABLE + getTableFileName(source.objectName), false, Encoding.ASCII);
                sw.Flush();
                sw.Close();
                sw = null;
            }
            /*3建立檔案的結構Table*/
            if (System.IO.File.Exists(PATH_TABLE + getTableFileNameCust(source.objectName)) == false)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(PATH_TABLE + getTableFileNameCust(source.objectName), false, Encoding.ASCII);
                sw.Flush();
                sw.Close();
                sw = null;
            }
            /*4建立檔案的結構Model*/
            if (System.IO.File.Exists(PATH_MODEL + getModelFileName(MODEL_NAME)) == false)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(PATH_MODEL + getModelFileName(MODEL_NAME), false, Encoding.ASCII);
                sw.Flush();
                sw.Close();
                sw = null;
            }
            /*5建立檔案的結構Model*/
            if (System.IO.File.Exists(PATH_MODEL + getModelFileNameCust(MODEL_NAME)) == false)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(PATH_MODEL + getModelFileNameCust(MODEL_NAME), false, Encoding.ASCII);
                sw.Flush();
                sw.Close();
                sw = null;

            }

            /*開始輸入程式*/
            /*Record*/
            genCodeRecord(
                            source,
                            PATH_RECORD + getRecordFileName(source.objectName),
                            getModelFileName(MODEL_NAME).Split('.')[0],
                            getRecordFileName(source.objectName).Split('.')[0],
                            getTableFileName(source.objectName).Split('.')[0]);            
          

            /*Table*/
            genCodeTable(
                source,
                PATH_TABLE + getTableFileName(source.objectName),
                getModelFileName(MODEL_NAME).Split('.')[0],
                getRecordFileName(source.objectName).Split('.')[0],
                getTableFileName(source.objectName).Split('.')[0]);

            genCodeTableCust(
                source,
                PATH_TABLE + getTableFileNameCust(source.objectName),
                getModelFileName(MODEL_NAME).Split('.')[0],
                getRecordFileName(source.objectName).Split('.')[0],
                getTableFileName(source.objectName).Split('.')[0]);

            ///*Mode*/
            //genCodeModel(
            //    source,
            //    PATH_MODEL + getModelFileNameCust(MODEL_NAME),
            //    getModelFileName(MODEL_NAME).Split('.')[0],
            //    getRecordFileName(source.objectName).Split('.')[0],
            //    getTableFileName(source.objectName).Split('.')[0]);
        }

        private string getRecordFileName(string name)
        {
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            output = output.Replace("\\\"", "");
            output = output.Replace("\"", "");
            return output + "_Record.cs";
        }
    
        private string formatMethodString(string name) {
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            return output ;
        }
        private string getTableFileName(string name)
        {
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            output = output.Replace("\\\"", "");
            output = output.Replace("\"", "");
            return output + ".cs";
        }
        private string getTableFileNameCust(string name)
        {
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            output = output.Replace("\\\"", "");
            output = output.Replace("\"", "");
            return output + "@.cs";
        }
        private string getModelFileName(string name)
        {
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            output = output.Replace("\\\"", "");
            output = output.Replace("\"", "");
            return output + "Model.cs";
        }

        private string getModelFileNameCust(string name)
        {
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            output = output.Replace("\\\"", "");
            output = output.Replace("\"", "");
            return output + "Model@.cs";
        }

        private string getFormatString(string name)
        {
            
            string output = "";
            bool isFirst = true;
            string tmpName = name;
            if (tmpName.Split('.').Length > 1)
            {
                tmpName = tmpName.Split('.')[tmpName.Split('.').Length - 1];
            }
            char[] all = tmpName.ToCharArray();
            foreach (var A in all)
            {
                if (A == '_')
                {
                    isFirst = true;
                    continue;
                }

                if (isFirst == true)
                {
                    output += A.ToString().ToUpper();
                    isFirst = false;
                }
                else
                {
                    output += A.ToString().ToLower();
                }
            }
            return output;
        }

        private string columnType2objectType(string typeName)
        {
            if (typeName.ToUpper() == "DATETIME")
            {
                return "DateTime?";
            }
            else if (typeName.ToUpper() == "DECIMAL") {
                return "decimal?";
            }
            else if (typeName.ToUpper() == "INT") {
                return "int?";
            }
            else if (typeName.ToUpper() == "FLOAT") {
                return "float?";
            }
            else if (typeName.ToUpper() == "SHORT") {
                return "short?";
            }
            else if (typeName.ToUpper() == "UNSIGNEDBYTE")
            {
                return "byte?";
            }
            else if (typeName.ToUpper() == "unsignedShort".ToUpper())
            {
                return "ushort?";
            }
            else if (typeName.ToUpper() == "unsignedInt".ToUpper())
            {
                return "uint?";
            }
            else if (typeName.ToUpper() == "base64Binary".ToUpper())
            {
                return "byte[]";
            }
            else if (typeName.ToUpper() == "boolean".ToUpper())
            {
                return "bool?";
            }
            else if (typeName.ToUpper() == "duration".ToUpper())
            {
                return "TimeSpan?";
            }
            else if (typeName.ToUpper() == "double".ToUpper())
            {
                return "double?";
            }   


                

            else
            {
                return typeName;
            }
        }
        private string FormatClassName(string name) {
            name = name.Replace("$", "_");
            name = name.Replace("!", "_");
            name = name.Replace("#", "_");
            name = name.Replace("`", "_");
            return name;
        }
        private void genCodeRecord(
                    XDBSource source,
                    string fileFullPath,
                    string modelName,
                    string className,
                    string objTableName)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileFullPath);
            string _tableName = "";
            string isTable = "true";
            modelName = modelName.Substring(0, modelName.Length - 5);
            if (source.objectName.IndexOf(".") >= 0)
            {
                _tableName = source.objectName.Split('.')[1].ToUpper();
            }
            if (source.objectView.ToUpper() == "VIEW")
            {
                isTable = "false";
            }
            _tableName = _tableName.Replace("\"", "");

            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Linq;");
            sw.WriteLine("using System.Text;");
            sw.WriteLine("using LK.Attribute;");
            sw.WriteLine("using LK.DB;  ");

            sw.WriteLine("using LK.DB.SQLCreater;  ");
            
            sw.WriteLine("using " + txtNamespace.Text + ".Model." + modelName + ".Table;");
            sw.WriteLine("namespace " + txtNamespace.Text + ".Model." + modelName + ".Table.Record");
            sw.WriteLine("{");
            sw.WriteLine("\t[LkRecord]");
            sw.WriteLine("\t[TableView(\"" + _tableName + "\", " + isTable + ")]");
            sw.WriteLine("\t[LkDataBase(\"" + source.objectName.ToUpper().Split('.')[0].Replace("\"", "") + "\")]");
            sw.WriteLine("\t[Serializable]");            
            sw.WriteLine("\tpublic class " + FormatClassName(className) + " : RecordBase{");
            sw.WriteLine("\t\tpublic " + FormatClassName(className) + "(){}");
            sw.WriteLine("\t\t/*欄位資訊 Start*/");
            foreach (var item in source.cols)
            {
                sw.WriteLine("\t\t" + columnType2objectType(item.columnType) + " " + "_" + item.columnName.ToUpper() + "=null;");
            }
            sw.WriteLine("\t\t/*欄位資訊 End*/");

            foreach (var item in source.cols)
            {
                sw.WriteLine("");
                bool isPK = false;
                foreach (var pk in source.ConstraintCol)
                {
                    if (item.columnName.ToUpper() == pk.columnName.ToUpper())
                    {
                        isPK = true;
                    }
                }
                sw.WriteLine("\t\t[ColumnName(\"" + item.columnName.ToUpper() + "\"," + (isPK ? "true" : "false") + ",typeof(" + columnType2objectType(item.columnType) + "))]");
                sw.WriteLine("\t\tpublic " + columnType2objectType(item.columnType) + " " + item.columnName.ToUpper());
                sw.WriteLine("\t\t{");
                sw.WriteLine("\t\t\tset");
                sw.WriteLine("\t\t\t{");
                sw.WriteLine("\t\t\t\t_" + item.columnName.ToUpper() + "=value;");
                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tget");
                sw.WriteLine("\t\t\t{");
                sw.WriteLine("\t\t\t\treturn _" + item.columnName.ToUpper() + ";");
                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t}");
            }

            sw.WriteLine("\t\tpublic " + this.FormatClassName(className) + " Clone(){");
            sw.WriteLine("\t\t\ttry{");
            sw.WriteLine("\t\t\t\treturn this.Clone<" + FormatClassName(className) + ">(this);");
            sw.WriteLine("\t\t\t}");
            sw.WriteLine("\t\t\tcatch (Exception ex){");
            sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
            sw.WriteLine("\t\t\t\tthrow ex;");
            sw.WriteLine("\t\t\t}");    

            

            sw.WriteLine("\t\t}");

            /*產生gotoXXXTable*/
            sw.WriteLine("\t\tpublic " + FormatClassName(objTableName) + " gotoTable(){");
            sw.WriteLine("\t\t\ttry{");
            sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
            sw.WriteLine("\t\t\t\t" + FormatClassName(objTableName) + " ret = new " + FormatClassName(objTableName) + "(dbc,this);");
            sw.WriteLine("\t\t\t\treturn ret;");

            sw.WriteLine("\t\t\t}");
            sw.WriteLine("\t\t\tcatch (Exception ex){");
            sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
            sw.WriteLine("\t\t\t\tthrow ex;");
            sw.WriteLine("\t\t\t}");

            sw.WriteLine("\t\t}");

            /*產生Link_XXXXX 我是別人的父項*/
            System.Collections.Hashtable htMethod = new System.Collections.Hashtable();
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    

                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_")) {
                            byName = byName.Substring(0, byName.Length - 5);
                        }
                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                       

                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Child, byName)))
                        {
                            continue;
                        }
                        else {
                            htMethod.Add(getLinkMethodName(tmp2.Child, byName),"");
                        }
                        sw.WriteLine("\t\t/*201303180347*/");
                        sw.WriteLine("\t\tpublic List<" +this.FormatClassName( toRecordClassName )+ "> " + this.FormatClassName(getLinkMethodName(tmp2.Child, byName)) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + this.FormatClassName(toRecordClassName) + "> ret= new List<" + this.FormatClassName(toRecordClassName) + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + this.FormatClassName(getTableFileName(tmp2.Child).Split('.')[0] )+ " ___table = new " + this.FormatClassName(getTableFileName(tmp2.Child).Split('.')[0]) + "(dbc);");
                        sw.WriteLine("\t\t\t\tret=(List<" + this.FormatClassName(toRecordClassName) + ">)");
                        sw.WriteLine("\t\t\t\t\t\t\t\t\t\t___table.Where(new SQLCondition(___table)");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t\t\t\t\t.Equal(___table." + tmp2.ChildColumnName[i].columnName.ToUpper() + ",this." + tmp2.ParentColumnName[i].columnName.ToUpper() + ")");
                            if ((i + 1) < tmp2.ParentColumnName.Count)
                            {
                                sw.Write(".And()");
                            }
                        }
                        sw.Write(")\n");
                        sw.WriteLine("\t\t\t\t\t.FetchAll<" + this.FormatClassName(toRecordClassName) + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");   

                        sw.WriteLine("\t\t}");
                    }
                }
            }
            /*產生Link_XXXXX 我是別人的父項*/           
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                   

                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }
                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];

                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Child+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkMethodName(tmp2.Child+"limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180348*/");
                        sw.WriteLine("\t\tpublic List<" + this.FormatClassName(toRecordClassName) + "> " + this.FormatClassName(getLinkMethodName(tmp2.Child, byName)) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + this.FormatClassName(toRecordClassName) + "> ret= new List<" + this.FormatClassName(toRecordClassName) + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + this.FormatClassName(getTableFileName(tmp2.Child).Split('.')[0]) + " ___table = new " + this.FormatClassName(getTableFileName(tmp2.Child).Split('.')[0] )+ "(dbc);");
                        sw.WriteLine("\t\t\t\tret=(List<" +this.FormatClassName( toRecordClassName )+ ">)");
                        sw.WriteLine("\t\t\t\t\t\t\t\t\t\t___table.Where(new SQLCondition(___table)");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t\t\t\t\t.Equal(___table." + tmp2.ChildColumnName[i].columnName.ToUpper() + ",this." + tmp2.ParentColumnName[i].columnName.ToUpper() + ")");
                            if ((i + 1) < tmp2.ParentColumnName.Count)
                            {
                                sw.Write(".And()");
                            }
                        }
                        sw.Write(")\n");
                        sw.WriteLine("\t\t\t\t\t.Order(limit)");
                        sw.WriteLine("\t\t\t\t\t.Limit(limit)"); 

                        sw.WriteLine("\t\t\t\t\t.FetchAll<" + this.FormatClassName(toRecordClassName) + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }
            /*產生Link_XXXXX 別人是我的父項時 */
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    /*別人是我的父項時*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }
                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];

                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Parent, byName)))
                        {
                            continue;
                        }
                        else {
                            htMethod.Add(getLinkMethodName(tmp2.Parent, byName),"");
                        }
                        sw.WriteLine("\t\tpublic List<" + toRecordClassName + "> " + getLinkMethodName(tmp2.Parent, byName) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + toRecordClassName + "> ret= new List<" + toRecordClassName + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + getTableFileName(tmp2.Parent).Split('.')[0] + " ___table = new " + getTableFileName(tmp2.Parent).Split('.')[0] + "(dbc);");
                        sw.WriteLine("\t\t\t\tret=(List<" + toRecordClassName + ">)");
                        sw.WriteLine("\t\t\t\t\t\t\t\t\t\t___table.Where(new SQLCondition(___table)");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t\t\t\t\t.Equal(___table." + tmp2.ParentColumnName[i].columnName.ToUpper() + ",this." + tmp2.ChildColumnName[i].columnName.ToUpper() + ")");
                            if ((i + 1) < tmp2.ChildColumnName.Count)
                            {
                                sw.Write(".And()");
                            }
                        }
                        sw.Write(")\n");
                        sw.WriteLine("\t\t\t\t\t.FetchAll<" + toRecordClassName + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生Link_XXXXX 別人是我的父項時 */
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    /*別人是我的父項時*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }
                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];

                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Parent+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkMethodName(tmp2.Parent+"limit", byName), "");
                        }
                        
                        sw.WriteLine("\t\t/*201303180404*/");
                        sw.WriteLine("\t\tpublic List<" + toRecordClassName + "> " + getLinkMethodName(tmp2.Parent, byName) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + toRecordClassName + "> ret= new List<" + toRecordClassName + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + getTableFileName(tmp2.Parent).Split('.')[0] + " ___table = new " + getTableFileName(tmp2.Parent).Split('.')[0] + "(dbc);");
                        sw.WriteLine("\t\t\t\tret=(List<" + toRecordClassName + ">)");
                        sw.WriteLine("\t\t\t\t\t\t\t\t\t\t___table.Where(new SQLCondition(___table)");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t\t\t\t\t.Equal(___table." + tmp2.ParentColumnName[i].columnName.ToUpper() + ",this." + tmp2.ChildColumnName[i].columnName.ToUpper() + ")");
                            if ((i + 1) < tmp2.ChildColumnName.Count)
                            {
                                sw.Write(".And()");
                            }
                        }
                        sw.Write(")\n");
                        sw.WriteLine("\t\t\t\t\t.Order(limit)");
                        sw.WriteLine("\t\t\t\t\t.Limit(limit)");   
                        sw.WriteLine("\t\t\t\t\t.FetchAll<" + toRecordClassName + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }




            /*產生LinkFill_XXXXX 我到別人家*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {

                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Child).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Child, byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Child, byName), "");
                        }
                        
                        sw.WriteLine("\t\t/*201303180357*/");   
                        sw.WriteLine("\t\tpublic " + this.FormatClassName(gotoClassName) + " " + this.FormatClassName(getLinkFillMethodName(tmp2.Child, byName)) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " +  this.FormatClassName(getLinkMethodName(tmp2.Child, byName)) + "();");

                        sw.WriteLine("\t\t\t\t" + this.FormatClassName( gotoClassName) + " ret=new " +  this.FormatClassName(gotoClassName) + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生LinkFill_XXXXX 我到別人家Limit*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {

                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Child).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Child+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Child+"limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180358*/");
                        sw.WriteLine("\t\tpublic " + this.FormatClassName(gotoClassName) + " " + this.FormatClassName(getLinkFillMethodName(tmp2.Child, byName) )+ "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " + this.FormatClassName(getLinkMethodName(tmp2.Child, byName) )+ "(limit);");

                        sw.WriteLine("\t\t\t\t" + this.FormatClassName(gotoClassName) + " ret=new " + this.FormatClassName(gotoClassName) + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            } 


            /********************/

            /*產生LinkFill_XXXXX 別人到我家*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {

                    /*由我去別人家的*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Parent).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Parent, byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Parent, byName), "");
                        }

                        sw.WriteLine("\t\t/*2013031800428*/");
                        sw.WriteLine("\t\tpublic " + gotoClassName + " " + getLinkFillMethodName(tmp2.Parent, byName) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " + getLinkMethodName(tmp2.Parent, byName) + "();");

                        sw.WriteLine("\t\t\t\t" + gotoClassName + " ret=new " + gotoClassName + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生LinkFill_XXXXX 別人到我家Limit*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {

                    /*由我去別人家的*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Parent).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Parent + "limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Parent + "limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180429*/");
                        sw.WriteLine("\t\tpublic " + gotoClassName + " " + getLinkFillMethodName(tmp2.Parent, byName) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " + getLinkMethodName(tmp2.Parent, byName) + "(limit);");

                        sw.WriteLine("\t\t\t\t" + gotoClassName + " ret=new " + gotoClassName + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            } 

            sw.WriteLine("\t}");
            sw.WriteLine("}");
            sw.Flush();
            sw = null;
            /*
            using System;
            using ;
            using ;
            using ;
            using ;
            using ;
             * */
        }


        private void genCodeTable(XDBSource source,
                    string fileFullPath,
                    string modelName,
                    string className,
                    string objTableName)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileFullPath);
            string _tableName = "";
            string isTable = "true";
            modelName = modelName.Substring(0, modelName.Length - 5);
            if (source.objectName.IndexOf(".") >= 0)
            {
                _tableName = source.objectName.Split('.')[1].ToUpper();
            }
            if (source.objectView.ToUpper() == "VIEW")
            {
                isTable = "false";
            }
            _tableName = _tableName.Replace("\"", "");
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Linq;");
            sw.WriteLine("using System.Text;  ");
            sw.WriteLine("using LK.Attribute;  ");
            sw.WriteLine("using LK.DB;  ");
            sw.WriteLine("using LK.Config.DataBase;  ");
            sw.WriteLine("using LK.DB.SQLCreater;  ");

            sw.WriteLine("using " + txtNamespace.Text + ".Model." + modelName + ".Table.Record  ;  ");
            sw.WriteLine("namespace " + txtNamespace.Text + ".Model." + modelName + ".Table");
            sw.WriteLine("{");
            sw.WriteLine("\t[LkDataBase(\""+source.objectName.ToUpper().Split('.')[0].Replace("\"","")+"\")]");
            sw.WriteLine("\t[TableView(\"" + _tableName + "\", " + isTable + ")]");
            sw.WriteLine("\tpublic partial class " + this.FormatClassName(objTableName) + " : TableBase{");
            sw.WriteLine("\t/*固定物件*/");
            sw.WriteLine("\t//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;");
            sw.WriteLine("\t/*固定物件但名稱需更新*/");

            sw.WriteLine("\tprivate " + this.FormatClassName(className) + " _currentRecord = null;");
            sw.WriteLine("\tprivate IList<" + FormatClassName(className) + "> _All_Record = new List<" + FormatClassName(className) + ">();");
            sw.WriteLine("\t\t/*建構子*/");
            sw.WriteLine("\t\tpublic " + FormatClassName(objTableName) + "(){}");
            sw.WriteLine("\t\tpublic " + FormatClassName(objTableName) + "(IDataBaseConfigInfo dbc,string db): base(dbc,db){}");
            sw.WriteLine("\t\tpublic " + FormatClassName(objTableName) + "(IDataBaseConfigInfo dbc): base(dbc){}");
            sw.WriteLine("\t\tpublic " + FormatClassName(objTableName) + "(IDataBaseConfigInfo dbc," + FormatClassName(className) + " currenData){");
           
            sw.WriteLine("\t\t\tthis.setDataBaseConfigInfo(dbc);");
          
            sw.WriteLine("\t\t\tthis._currentRecord = currenData;");
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic " + FormatClassName(objTableName) + "(IList<" + this.FormatClassName( className) + "> currenData){");
            sw.WriteLine("\t\t\tthis._All_Record = currenData;");
            sw.WriteLine("\t\t}");
              
        
            //canred
            sw.WriteLine("\t\t/*欄位資訊 Start*/");
            foreach (var item in source.cols)
            {
                
                bool isPK = false;
                foreach (var pk in source.ConstraintCol)
                {
                    if (item.columnName.ToUpper() == pk.columnName.ToUpper())
                    {
                        isPK = true;
                    }
                }
                
                
                //sw.WriteLine("\t\tpublic string"  + " " + item.columnName.ToUpper() + " {get{return \""+item.columnName.ToUpper()+"\" ; }}");
                sw.WriteLine("\t\tpublic string" + " " + item.columnName.ToUpper() + " {");
                sw.WriteLine("\t\t\t[ColumnName(\"" + item.columnName.ToUpper() + "\"," + (isPK ? "true" : "false") + ",typeof(" + columnType2objectType(item.columnType) + "))]");
                sw.WriteLine("\t\t\tget{return \"" + item.columnName.ToUpper() + "\" ; }}")
                
                ;
            }
            sw.WriteLine("\t\t/*欄位資訊 End*/");

            sw.WriteLine("\t\t/*固定的方法，但名稱需變更 Start*/");
            sw.WriteLine("\t\tpublic " +this.FormatClassName( className) + " CurrentRecord(){");
            sw.WriteLine("\t\t\ttry{");

            sw.WriteLine("\t\t\t\tif (_currentRecord == null){");
            sw.WriteLine("\t\t\t\t\tif (this._All_Record.Count > 0){");
            sw.WriteLine("\t\t\t\t\t\t_currentRecord = this._All_Record.First();");
            sw.WriteLine("\t\t\t\t\t}");
            sw.WriteLine("\t\t\t\t}");


            sw.WriteLine("\t\t\t\treturn _currentRecord;");
            sw.WriteLine("\t\t\t}");
            sw.WriteLine("\t\t\tcatch (Exception ex){");
            sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
            sw.WriteLine("\t\t\t\tthrow ex;");
            sw.WriteLine("\t\t\t}");

            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic " + this.FormatClassName(className) + " CreateNew(){");

            sw.WriteLine("\t\t\ttry{");
            sw.WriteLine("\t\t\t\t" + this.FormatClassName(className) + " newData = new " + this.FormatClassName(className) + "();");
            sw.WriteLine("\t\t\t\treturn newData;");
            sw.WriteLine("\t\t\t}");
            sw.WriteLine("\t\t\tcatch (Exception ex){");
            sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
            sw.WriteLine("\t\t\t\tthrow ex;");
            sw.WriteLine("\t\t\t}");


            
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic IList<" +this.FormatClassName( className) + "> AllRecord(){");


            sw.WriteLine("\t\t\ttry{");
            sw.WriteLine("\t\t\t\treturn _All_Record;");
            sw.WriteLine("\t\t\t}");
            sw.WriteLine("\t\t\tcatch (Exception ex){");
            sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
            sw.WriteLine("\t\t\t\tthrow ex;");
            sw.WriteLine("\t\t\t}");            
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic void RemoveAllRecord(){");
            sw.WriteLine("\t\t\ttry{");
            sw.WriteLine("\t\t\t\t_All_Record = new List<" + this.FormatClassName(className) + ">();");
            sw.WriteLine("\t\t\t}");
            sw.WriteLine("\t\t\tcatch (Exception ex){");
            sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
            sw.WriteLine("\t\t\t\tthrow ex;");
            sw.WriteLine("\t\t\t}");           
                      
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\t/*固定的方法，但名稱需變更 End*/");

            sw.WriteLine("\t\t/*有關PK的方法*/");
            /*Fill_By_PK*/
            if (source.ConstraintCol.Count > 0) {
                string parameterString = "";
                foreach (var pk in source.ConstraintCol) {
                    
                    string conditionType = "";
                    foreach (var col in source.cols) {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper()) {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                        }                        
                    }                    
                }
                if (parameterString.EndsWith(",")) {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                sw.WriteLine("\t\t//TEMPLATE TABLE 201303180156");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(objTableName) + " Fill_By_PK(" +this.FormatClassName( parameterString) + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" +this.FormatClassName( className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                    int sourceCount = source.ConstraintCol.Count;
                    int sourceRun = 0;
                    foreach (var pk in source.ConstraintCol)
                    {
                        sourceRun++;
                        sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                        if(sourceRun < sourceCount){
                            sw.WriteLine("\t\t\t\t\t\t\t\t\t.And()");
                        }
                    }

                    sw.WriteLine("\t\t\t\t).FetchAll<" + this.FormatClassName(className) + ">()  ;  ");
                    sw.WriteLine("\t\t\t\t_All_Record = ret;");
                    //空值資料的處理 start
                    sw.WriteLine("\t\t\t\tif (_All_Record.Count > 0){");
                    sw.WriteLine("\t\t\t\t\t_currentRecord = ret.First();}");
                    sw.WriteLine("\t\t\t\telse{");
                    sw.WriteLine("\t\t\t\t\t_currentRecord = null;}");
                    //空值資料的處理 end
                    sw.WriteLine("\t\t\t\treturn this;");
                
                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");           
                sw.WriteLine("\t\t}"); 
            }

            /*Fill_By_PK Transcation.*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                        }
                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                parameterString += ",DB db";
                sw.WriteLine("\t\t//TEMPLATE TABLE 201303180156");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(objTableName) + " Fill_By_PK(" +parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" + this.FormatClassName(className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                int sourceCount = source.ConstraintCol.Count;
                int sourceRun = 0;
                foreach (var pk in source.ConstraintCol)
                {
                    sourceRun++;
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                    if (sourceRun < sourceCount)
                    {
                        sw.WriteLine("\t\t\t\t\t\t\t\t\t.And()");
                    }
                }

                sw.WriteLine("\t\t\t\t).FetchAll<" +this.FormatClassName( className) + ">(db)  ;  ");
                sw.WriteLine("\t\t\t\t_All_Record = ret;");
                //空值資料的處理 start
                sw.WriteLine("\t\t\t\tif (_All_Record.Count > 0){");
                sw.WriteLine("\t\t\t\t\t_currentRecord = ret.First();}");
                sw.WriteLine("\t\t\t\telse{");
                sw.WriteLine("\t\t\t\t\t_currentRecord = null;}");
                //空值資料的處理 end
                sw.WriteLine("\t\t\t\treturn this;");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t}");
            }
            /*Fetch_By_PK*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                        }
                        
                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                sw.WriteLine("\t\t//TEMPLATE TABLE 20130319042");
                sw.WriteLine("\t\tpublic " +this.FormatClassName( className) + " Fetch_By_PK(" + parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" + this.FormatClassName(className )+ "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t\t).FetchAll<" +this.FormatClassName( className) + ">()  ;  ");
                sw.WriteLine("\t\t\t\treturn ret.First();");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");              

                sw.WriteLine("\t\t}");
            }

            /*Fetch_By_PK Transaction*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                parameterString += ",DB db";
                sw.WriteLine("\t\t//TEMPLATE TABLE 20130319044");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(className) + " Fetch_By_PK(" + parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" + this.FormatClassName(className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t\t).FetchAll<" + this.FormatClassName(className) + ">(db)  ;  ");
                sw.WriteLine("\t\t\t\treturn ret.First();");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");
            }
            /*FrameHead Fill_By_UUID*/          
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                string pkColString = "";
                foreach (var pk in source.ConstraintCol)
                {
                    
                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                            pkColString +=  this.getFormatString(pk.columnName) + "_And_";
                        }
                        
                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                if (pkColString.EndsWith("_And_"))
                {
                    pkColString = pkColString.Substring(0, pkColString.Length - 5);
                }
                sw.WriteLine("\t\t//TEMPLATE TABLE 20130319045");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(objTableName) + " Fill_By_" + pkColString + "(" + parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" + this.FormatClassName(className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t\t).FetchAll<" + this.FormatClassName(className )+ ">()  ;  ");
                sw.WriteLine("\t\t\t\t_All_Record = ret;");
                sw.WriteLine("\t\t\t\t_currentRecord = ret.First();");
                sw.WriteLine("\t\t\t\treturn this;");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");
            }

            /*FrameHead Fill_By_UUID Transcation*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                string pkColString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                            pkColString += this.getFormatString(pk.columnName.ToUpper()) + "_And_";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                if (pkColString.EndsWith("_And_"))
                {
                    pkColString = pkColString.Substring(0, pkColString.Length - 5);
                }
                parameterString += ",DB db";
                sw.WriteLine("\t\t//TEMPLATE TABLE 20130319046");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(objTableName) + " Fill_By_" + pkColString + "(" + parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" + this.FormatClassName(className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t\t).FetchAll<" + this.FormatClassName(className) + ">(db)  ;  ");
                sw.WriteLine("\t\t\t\t_All_Record = ret;");
                sw.WriteLine("\t\t\t\t_currentRecord = ret.First();");
                sw.WriteLine("\t\t\t\treturn this;");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");
            }
            /*Fetch_By_UUID*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                string pkColString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                            pkColString += this.getFormatString(pk.columnName.ToUpper()) + "_And_";
                        }
                        
                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                if (pkColString.EndsWith("_And_"))
                {
                    pkColString = pkColString.Substring(0, pkColString.Length - 5);
                }
                sw.WriteLine("\t\t//TEMPLATE TABLE 20130319047");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(className) + " Fetch_By_" + pkColString + "(" + parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" + this.FormatClassName(className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t\t).FetchAll<" + this.FormatClassName(className) + ">()  ;  ");
                sw.WriteLine("\t\t\t\treturn ret.First();");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.ErrorNoThrowException(this, ex);");
                //sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t\treturn null;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");
            }


            /*Fetch_By_UUID Transcation*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                string pkColString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName + ",";
                            pkColString += this.getFormatString(pk.columnName.ToUpper()) + "_And_";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                if (pkColString.EndsWith("_And_"))
                {
                    pkColString = pkColString.Substring(0, pkColString.Length - 5);
                }
                parameterString += ",DB db";
                sw.WriteLine("\t\t//TEMPLATE TABLE 20130319048");
                sw.WriteLine("\t\tpublic " + this.FormatClassName(className) + " Fetch_By_" + pkColString + "(" + parameterString + "){");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tIList<" +  this.FormatClassName(className) + "> ret = null;");
                sw.WriteLine("\t\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine("\t\t\t\t\t\t\t\t\t.Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t\t).FetchAll<" +  this.FormatClassName(className) + ">(db)  ;  ");
                sw.WriteLine("\t\t\t\treturn ret.First();");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");
            }

            if (isTable.ToUpper()=="TRUE")
            {
                #region 是table才做的
                /*利用物件自已的AllRecord的資料來更新資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來更新資料行*/");
                sw.WriteLine("\t\tpublic void UpdateAllRecord() {");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tUpdateAllRecord<" + className + ">(this.AllRecord());   ");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");        
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");    
            
                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來更新資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來更新資料行*/");
                sw.WriteLine("\t\tpublic void UpdateAllRecord(DB db) {");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tUpdateAllRecord<" + className + ">(this.AllRecord(),db);   ");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");       

                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來新增資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來新增資料行*/");
                sw.WriteLine("\t\tpublic void InsertAllRecord() {");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tInsertAllRecord<" + className + ">(this.AllRecord());   ");
                
                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來新增資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來新增資料行*/");
                sw.WriteLine("\t\tpublic void InsertAllRecord(DB db) {");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tInsertAllRecord<" + className + ">(this.AllRecord(),db);   ");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來刪除資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來刪除資料行*/");
                sw.WriteLine("\t\tpublic void DeleteAllRecord() {");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tDeleteAllRecord<" + className + ">(this.AllRecord());   ");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來刪除資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來刪除資料行*/");
                sw.WriteLine("\t\tpublic void DeleteAllRecord(DB db) {");
                sw.WriteLine("\t\t\ttry{");

                sw.WriteLine("\t\t\t\tDeleteAllRecord<" + className + ">(this.AllRecord(),db);   ");

                sw.WriteLine("\t\t\t}");
                sw.WriteLine("\t\t\tcatch (Exception ex){");
                sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                sw.WriteLine("\t\t\t\tthrow ex;");
                sw.WriteLine("\t\t\t}");

                sw.WriteLine("\t\t}");
                #endregion
            }

            sw.WriteLine("\t\t/*依照資料表與資料表的關係，產生出來的方法*/");
            System.Collections.Hashtable htMethod = new System.Collections.Hashtable();
            /*產生Link_XXXXX 我到別人家*/
            foreach (var tmp in XDBs)
            {
                
                foreach (var tmp2 in tmp.rels)
                {
                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper())+"_And_";                            
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Child,byName)))
                        {
                            continue;
                        }
                        else {
                            htMethod.Add(getLinkMethodName(tmp2.Child, byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180320*/");
                        sw.WriteLine("\t\tpublic List<" + this.FormatClassName( toRecordClassName) + "> " + this.FormatClassName( getLinkMethodName(tmp2.Child,byName)) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + this.FormatClassName( toRecordClassName) + "> ret= new List<" + this.FormatClassName( toRecordClassName) + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + this.FormatClassName( getTableFileName(tmp2.Child).Split('.')[0]) + " ___table = new " + this.FormatClassName( getTableFileName(tmp2.Child).Split('.')[0] )+ "(dbc);");
                        sw.WriteLine("\t\t\t\tSQLCondition condition = new SQLCondition(___table) ;");

                        sw.WriteLine("\t\t\t\tforeach(var item in AllRecord()){");
                        sw.WriteLine("\t\t\t\t\t\tcondition");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t.L().Equal(___table." + tmp2.ChildColumnName[i].columnName.ToUpper() + ",item." + tmp2.ParentColumnName[i].columnName.ToUpper() + ").R().Or()");
                            if ((i + 1) < tmp2.ParentColumnName.Count)
                            {
                                sw.Write(".Or()");
                            }
                        }
                        sw.Write("  ; \n ");
                        sw.WriteLine("\t\t\t\t}");
                        sw.WriteLine("\t\t\t\tcondition.CheckSQL();");
                        sw.WriteLine("\t\t\t\tret=(List<" + this.FormatClassName( toRecordClassName) + ">)");
                        sw.WriteLine("\t\t\t\t\t\t___table.Where(condition)");
                        sw.WriteLine("\t\t\t\t\t\t.FetchAll<" + this.FormatClassName( toRecordClassName )+ ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}"); 

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生Link_XXXXX 我到別人家*/
            foreach (var tmp in XDBs)
            {
                
                foreach (var tmp2 in tmp.rels)
                {
                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Child+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkMethodName(tmp2.Child+"limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180321*/");
                        sw.WriteLine("\t\tpublic List<" + this.FormatClassName(toRecordClassName) + "> " +  this.FormatClassName(getLinkMethodName(tmp2.Child, byName)) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" +  this.FormatClassName(toRecordClassName) + "> ret= new List<" +  this.FormatClassName(toRecordClassName) + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + this.FormatClassName( getTableFileName(tmp2.Child).Split('.')[0] )+ " ___table = new " +  this.FormatClassName(getTableFileName(tmp2.Child).Split('.')[0] )+ "(dbc);");
                        sw.WriteLine("\t\t\t\tSQLCondition condition = new SQLCondition(___table) ;");

                        sw.WriteLine("\t\t\t\tforeach(var item in AllRecord()){");
                        sw.WriteLine("\t\t\t\t\t\tcondition");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t.L().Equal(___table." + tmp2.ChildColumnName[i].columnName.ToUpper() + ",item." + tmp2.ParentColumnName[i].columnName.ToUpper() + ").R().Or()");
                            if ((i + 1) < tmp2.ParentColumnName.Count)
                            {
                                sw.Write(".Or()");
                            }
                        }
                        sw.Write("  ; \n ");
                        sw.WriteLine("\t\t\t\t}");
                        sw.WriteLine("\t\t\t\tcondition.CheckSQL();");
                        sw.WriteLine("\t\t\t\tret=(List<" +  this.FormatClassName(toRecordClassName) + ">)");
                        sw.WriteLine("\t\t\t\t\t\t___table.Where(condition)");
                        sw.WriteLine("\t\t\t\t\t\t.Order(limit)");
                        sw.WriteLine("\t\t\t\t\t\t.Limit(limit)"); 
                        sw.WriteLine("\t\t\t\t\t\t.FetchAll<" +  this.FormatClassName(toRecordClassName) + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生Link_XXXXX 別人來我家*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    /*由別人來我家*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Parent, byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkMethodName(tmp2.Parent, byName), "");
                        }
                        sw.WriteLine("\t\tpublic List<" + toRecordClassName + "> " + getLinkMethodName(tmp2.Parent, byName) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + toRecordClassName + "> ret= new List<" + toRecordClassName + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ParentColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + getTableFileName(tmp2.Parent).Split('.')[0] + " ___table = new " + getTableFileName(tmp2.Parent).Split('.')[0] + "(dbc);");
                        sw.WriteLine("\t\t\t\tSQLCondition condition = new SQLCondition(___table) ;");

                        sw.WriteLine("\t\t\t\tforeach(var item in AllRecord()){");
                        sw.WriteLine("\t\t\t\t\t\tcondition");
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t.L().Equal(___table." + tmp2.ParentColumnName[i].columnName.ToUpper() + ",item." + tmp2.ChildColumnName[i].columnName.ToUpper() + ").R().Or()");
                            if ((i + 1) < tmp2.ChildColumnName.Count)
                            {
                                sw.Write(".Or()");
                            }
                        }
                        sw.Write("  ; \n ");
                        sw.WriteLine("\t\t\t\t}");
                        sw.WriteLine("\t\t\t\tcondition.CheckSQL();");
                        sw.WriteLine("\t\t\t\tret=(List<" + toRecordClassName + ">)");
                        sw.WriteLine("\t\t\t\t\t\t___table.Where(condition)");
                        sw.WriteLine("\t\t\t\t\t\t.FetchAll<" + toRecordClassName + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生Link_XXXXX 別人來我家*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                   
                    /*由別人來我家*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkMethodName(tmp2.Parent+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkMethodName(tmp2.Parent+"limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180340*/");
                        sw.WriteLine("\t\tpublic List<" + toRecordClassName + "> " + getLinkMethodName(tmp2.Parent, byName) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tList<" + toRecordClassName + "> ret= new List<" + toRecordClassName + ">();");
                        sw.WriteLine("\t\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ParentColumnName.Count;

                        sw.WriteLine("\t\t\t\t" + getTableFileName(tmp2.Parent).Split('.')[0] + " ___table = new " + getTableFileName(tmp2.Parent).Split('.')[0] + "(dbc);");
                        sw.WriteLine("\t\t\t\tSQLCondition condition = new SQLCondition(___table) ;");

                        sw.WriteLine("\t\t\t\tforeach(var item in AllRecord()){");
                        sw.WriteLine("\t\t\t\t\t\tcondition");
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            sw.Write("\t\t\t\t\t\t.L().Equal(___table." + tmp2.ParentColumnName[i].columnName.ToUpper() + ",item." + tmp2.ChildColumnName[i].columnName.ToUpper() + ").R().Or()");
                            if ((i + 1) < tmp2.ChildColumnName.Count)
                            {
                                sw.Write(".Or()");
                            }
                        }
                        sw.Write("  ; \n ");
                        sw.WriteLine("\t\t\t\t}");
                        sw.WriteLine("\t\t\t\tcondition.CheckSQL();");
                        sw.WriteLine("\t\t\t\tret=(List<" + toRecordClassName + ">)");
                        sw.WriteLine("\t\t\t\t\t\t___table.Where(condition)");

                        sw.WriteLine("\t\t\t\t\t\t.Order(limit)");
                        sw.WriteLine("\t\t\t\t\t\t.Limit(limit)");
                        						
						
                        sw.WriteLine("\t\t\t\t\t\t.FetchAll<" + toRecordClassName + ">() ; ");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }
            /*產生LinkFill_XXXXX 我到別人家*/
            
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    
                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString( tmp2.ChildColumnName[i].columnName.ToUpper()) +"_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Child).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Child, byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Child, byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180324*/");
                        sw.WriteLine("\t\tpublic " + this.FormatClassName(gotoClassName) + " " + this.FormatClassName(getLinkFillMethodName(tmp2.Child, byName) )+ "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " +this.FormatClassName( getLinkMethodName(tmp2.Child, byName)) + "();");

                        sw.WriteLine("\t\t\t\t" +this.FormatClassName( gotoClassName) + " ret=new " + this.FormatClassName(gotoClassName) + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");    

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生LinkFill_XXXXX 我到別人家*/
            
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    
                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ChildColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Child).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Child+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Child+"limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180325*/");
                        sw.WriteLine("\t\tpublic " + this.FormatClassName(gotoClassName) + " " + this.FormatClassName(getLinkFillMethodName(tmp2.Child, byName)) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " + this.FormatClassName(getLinkMethodName(tmp2.Child, byName) )+ "(limit);");

                        sw.WriteLine("\t\t\t\t" +this.FormatClassName( gotoClassName )+ " ret=new " + this.FormatClassName(gotoClassName) + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }
            /*產生LinkFill_XXXXX 別人到我家*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    
                    /*由我去別人家的*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Parent).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Parent, byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Parent, byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180336*/");
                        sw.WriteLine("\t\tpublic " + gotoClassName + " " + getLinkFillMethodName(tmp2.Parent, byName) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " + getLinkMethodName(tmp2.Parent, byName) + "();");

                        sw.WriteLine("\t\t\t\t" + gotoClassName + " ret=new " + gotoClassName + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            }

            /*產生LinkFill_XXXXX 別人到我家*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    
                    /*由我去別人家的*/
                    if (tmp2.Child.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ChildColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = this.getFormatString(tmp2.ParentColumnName[i].columnName.ToUpper()) + "_And_";
                        }

                        if (byName.EndsWith("_And_"))
                        {
                            byName = byName.Substring(0, byName.Length - 5);
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Parent).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Parent).Split('.')[0];
                        if (htMethod.ContainsKey(getLinkFillMethodName(tmp2.Parent+"limit", byName)))
                        {
                            continue;
                        }
                        else
                        {
                            htMethod.Add(getLinkFillMethodName(tmp2.Parent+"limit", byName), "");
                        }
                        sw.WriteLine("\t\t/*201303180337*/");
                        sw.WriteLine("\t\tpublic " + gotoClassName + " " + getLinkFillMethodName(tmp2.Parent, byName) + "(OrderLimit limit)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\ttry{");

                        sw.WriteLine("\t\t\t\tvar data = " + getLinkMethodName(tmp2.Parent, byName) + "(limit);");

                        sw.WriteLine("\t\t\t\t" + gotoClassName + " ret=new " + gotoClassName + "(data);");
                        sw.WriteLine("\t\t\t\treturn ret;");

                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcatch (Exception ex){");
                        sw.WriteLine("\t\t\t\tlog.Error(ex);LK.MyException.MyException.Error(this, ex);");
                        sw.WriteLine("\t\t\t\tthrow ex;");
                        sw.WriteLine("\t\t\t}");

                        sw.WriteLine("\t\t}");
                    }
                }
            } 
            sw.WriteLine("\t}");
            sw.WriteLine("}");
            sw.Flush();
            sw = null;
           
        }

        private void genCodeTableCust(XDBSource source,
             string fileFullPath,
             string modelName,
             string className,
             string objTableName)
        {
            modelName = modelName.Substring(0, modelName.Length - 5);
            bool needGen = false;
            System.IO.StreamReader sr = new System.IO.StreamReader(fileFullPath);
            if (sr.ReadToEnd().Trim().Length == 0) {
                needGen = true;
            }
            sr.Close();
            sr = null;
            if (needGen == true)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileFullPath);
                //string _tableName = "";
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("using System.Linq;");
                sw.WriteLine("using System.Text;  ");
                sw.WriteLine("using LK.Attribute;  ");
                sw.WriteLine("using LK.DB;  ");
                sw.WriteLine("using LK.Config.DataBase;  ");
                sw.WriteLine("using LK.DB.SQLCreater;  ");
                sw.WriteLine("using " + txtNamespace.Text + ".Model." + modelName + ".Table.Record  ;  ");
            
                sw.WriteLine("namespace " + txtNamespace.Text + ".Model." + modelName + ".Table");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic partial class " + this.FormatClassName( objTableName )+ " : TableBase{");
                sw.WriteLine("\t}");
                sw.WriteLine("}");
                sw.Flush();
                sw = null;
            }

        }

        private void genCodeModel(XDBSource source,
            string fileFullPath,
            string modelName,
            string className,
            string objTableName)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileFullPath);
            string _tableName = "";
            string isTable = "true";
            modelName = modelName.Substring(0, modelName.Length - 5);
            if (source.objectName.IndexOf(".") >= 0)
            {
                _tableName = source.objectName.Split('.')[1].ToUpper();
            }
            if (source.objectView.ToUpper() == "VIEW")
            {
                isTable = "false";
            }
            _tableName = _tableName.Replace("\"", "");
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Linq;");
            sw.WriteLine("using System.Text;  ");
            sw.WriteLine("using LK.Attribute;  ");
            sw.WriteLine("using LK.DB;  ");
            sw.WriteLine("using LK.Config.DataBase;  ");


            sw.WriteLine("using " + txtNamespace.Text + ".Model." + modelName + ".Table.Record  ;  ");
            sw.WriteLine("namespace " + txtNamespace.Text + ".Model." + modelName + ".Table");
            sw.WriteLine("{");
            sw.WriteLine("\t[LkDataBase(\"" + source.objectName.ToUpper().Split('.')[0] + "\")]");
            sw.WriteLine("\t[TableView(\"" + _tableName + "\", " + isTable + ")]");
            sw.WriteLine("\tpublic partial class " + objTableName + " : TableBase{");
            sw.WriteLine("\t/*固定物件*/");
            sw.WriteLine("\t//LK.DB.SQLCreater.ASQLCreater sqlCreater = null;");
            sw.WriteLine("\t/*固定物件但名稱需更新*/");

            sw.WriteLine("\tprivate " + className + " _currentRecord = null;");
            sw.WriteLine("\tprivate IList<" + className + "> _All_Record = new List<" + className + ">();");
            sw.WriteLine("\t\t/*建構子*/");
            sw.WriteLine("\t\tpublic " + objTableName + "(){}");
            sw.WriteLine("\t\tpublic " + objTableName + "(IDataBaseConfigInfo dbc): base(dbc){}");

            sw.WriteLine("\t\tpublic " + objTableName + "(" + className + " currenData){");
            sw.WriteLine("\t\t\tthis._currentRecord = currenData;");
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic " + objTableName + "(IList<" + className + "> currenData){");
            sw.WriteLine("\t\t\tthis._All_Record = currenData;");
            sw.WriteLine("\t\t}");



            sw.WriteLine("\t\t/*欄位資訊 Start*/");
            foreach (var item in source.cols)
            {
                sw.WriteLine("\t\tpublic string" + " " + item.columnName.ToUpper() + " {get{return \"" + item.columnName.ToUpper() + "\" ; }}");
            }
            sw.WriteLine("\t\t/*欄位資訊 End*/");

            sw.WriteLine("\t\t/*固定的方法，但名稱需變更 Start*/");
            sw.WriteLine("\t\tpublic " + className + " CurrentRecord(){");
            sw.WriteLine("\t\t\treturn _currentRecord;");
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic " + className + " CreateNew(){");
            sw.WriteLine("\t\t\t" + className + " newData = new " + className + "();");
            sw.WriteLine("\t\t\treturn newData;");
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic IList<" + className + "> AllRecord(){");
            sw.WriteLine("\t\t\treturn _All_Record;");
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\tpublic void RemoveAllRecord(){");
            sw.WriteLine("\t\t\t_All_Record = new List<" + className + ">();");
            sw.WriteLine("\t\t}");

            sw.WriteLine("\t\t/*固定的方法，但名稱需變更 End*/");

            sw.WriteLine("\t\t/*有關PK的方法*/");
            /*Fill_By_PK*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName.ToUpper() + ",";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }

                sw.WriteLine("\t\tpublic " + objTableName + " Fill_By_PK(" + parameterString + "){");
                sw.WriteLine("\t\t\tIList<" + className + "> ret = null;");
                sw.WriteLine("\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine(".Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t).FetchAll<" + className + ">()  ;  ");
                sw.WriteLine("\t\t\t_All_Record = ret;");
                sw.WriteLine("\t\t\t_currentRecord = ret.First();");
                sw.WriteLine("\t\t\treturn this;");
                sw.WriteLine("\t\t}");
            }
            /*Fetch_By_PK*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName.ToUpper() + ",";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }

                sw.WriteLine("\t\tpublic " + className + " Fetch_By_PK(" + parameterString + "){");
                sw.WriteLine("\t\t\tIList<" + className + "> ret = null;");
                sw.WriteLine("\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine(".Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t).FetchAll<" + className + ">()  ;  ");
                sw.WriteLine("\t\t\treturn ret.First();");
                sw.WriteLine("\t\t}");
            }
            /*FrameHead Fill_By_UUID*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                string pkColString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName.ToUpper() + ",";
                            pkColString += pk.columnName.ToUpper() + "_";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                if (pkColString.EndsWith("_"))
                {
                    pkColString = pkColString.Substring(0, pkColString.Length - 1);
                }

                sw.WriteLine("\t\tpublic " + objTableName + " Fill_By_" + pkColString + "(" + parameterString + "){");
                sw.WriteLine("\t\t\tIList<" + className + "> ret = null;");
                sw.WriteLine("\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine(".Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t).FetchAll<" + className + ">()  ;  ");
                sw.WriteLine("\t\t\t_All_Record = ret;");
                sw.WriteLine("\t\t\t_currentRecord = ret.First();");
                sw.WriteLine("\t\t\treturn this;");
                sw.WriteLine("\t\t}");
            }
            /*Fetch_By_UUID*/
            if (source.ConstraintCol.Count > 0)
            {
                string parameterString = "";
                string pkColString = "";
                foreach (var pk in source.ConstraintCol)
                {

                    string conditionType = "";
                    foreach (var col in source.cols)
                    {
                        if (col.columnName.ToUpper() == pk.columnName.ToUpper())
                        {
                            conditionType = columnType2objectType(col.columnType);
                            parameterString += conditionType + " p" + pk.columnName.ToUpper() + ",";
                            pkColString += pk.columnName.ToUpper() + "_";
                        }

                    }
                }
                if (parameterString.EndsWith(","))
                {
                    parameterString = parameterString.Substring(0, parameterString.Length - 1);
                }
                if (pkColString.EndsWith("_"))
                {
                    pkColString = pkColString.Substring(0, pkColString.Length - 1);
                }

                sw.WriteLine("\t\tpublic " + className + " Fetch_By_" + pkColString + "(" + parameterString + "){");
                sw.WriteLine("\t\t\tIList<" + className + "> ret = null;");
                sw.WriteLine("\t\t\tret = this.Where(");
                sw.WriteLine("\t\t\tnew SQLCondition(this)");

                foreach (var pk in source.ConstraintCol)
                {
                    sw.WriteLine(".Equal(this." + pk.columnName.ToUpper() + ",p" + pk.columnName + ")");
                }
                sw.WriteLine("\t\t\t).FetchAll<" + className + ">()  ;  ");
                sw.WriteLine("\t\t\treturn ret.First();");
                sw.WriteLine("\t\t}");
            }

            if (isTable.ToUpper() == "TRUE")
            {
                /*利用物件自已的AllRecord的資料來更新資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來更新資料行*/");
                sw.WriteLine("\t\tpublic void UpdateAllRecord() {");
                sw.WriteLine("\t\tUpdateAllRecord<" + className + ">(this.AllRecord());   ");
                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來更新資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來更新資料行*/");
                sw.WriteLine("\t\tpublic void UpdateAllRecord(DB db) {");
                sw.WriteLine("\t\tUpdateAllRecord<" + className + ">(this.AllRecord(),db);   ");
                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來新增資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來新增資料行*/");
                sw.WriteLine("\t\tpublic void InsertAllRecord() {");
                sw.WriteLine("\t\tInsertAllRecord<" + className + ">(this.AllRecord());   ");
                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來新增資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來新增資料行*/");
                sw.WriteLine("\t\tpublic void InsertAllRecord(DB db) {");
                sw.WriteLine("\t\tInsertAllRecord<" + className + ">(this.AllRecord(),db);   ");
                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來刪除資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來刪除資料行*/");
                sw.WriteLine("\t\tpublic void DeleteAllRecord() {");
                sw.WriteLine("\t\tDeleteAllRecord<" + className + ">(this.AllRecord());   ");
                sw.WriteLine("\t\t}");

                /*利用物件自已的AllRecord的資料來刪除資料行*/
                sw.WriteLine("\t\t/*利用物件自已的AllRecord的資料來刪除資料行*/");
                sw.WriteLine("\t\tpublic void DeleteAllRecord(DB db) {");
                sw.WriteLine("\t\tDeleteAllRecord<" + className + ">(this.AllRecord(),db);   ");
                sw.WriteLine("\t\t}");
            }
            sw.WriteLine("/*依照資料表與資料表的關係，產生出來的方法*/");
            /*產生Link_XXXXX*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {
                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        //List<string> byName = new List<string>();
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = tmp2.ChildColumnName[i].columnName.ToUpper() + "_";
                        }

                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        sw.WriteLine("\t\tpublic List<" + toRecordClassName + "> " + getLinkMethodName(tmp2.Child, byName) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\tList<" + toRecordClassName + "> ret= new List<" + toRecordClassName + ">();");
                        sw.WriteLine("\t\t\tvar dbc = LK.Config.DataBase.Factory.getInfo();");
                        int relCount = tmp2.ChildColumnName.Count;

                        sw.WriteLine("\t\t\t" + getTableFileName(tmp2.Child).Split('.')[0] + " ___table = new " + getTableFileName(tmp2.Child).Split('.')[0] + "(dbc);");
                        sw.WriteLine("\t\t\tSQLCondition condition = new SQLCondition(___table) ;");

                        sw.WriteLine("\t\t\tforeach(var item in AllRecord()){");
                        sw.WriteLine("\t\t\t\tcondition");
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            sw.Write(".L().Equal(___table." + tmp2.ChildColumnName[i].columnName.ToUpper() + ",this." + tmp2.ParentColumnName[i].columnName.ToUpper() + ").R().Or()");
                            if ((i + 1) < tmp2.ParentColumnName.Count)
                            {
                                sw.Write(".Or()");
                            }
                        }
                        sw.Write("  ;  ");
                        sw.WriteLine("\t\t\t}");
                        sw.WriteLine("\t\t\tcondition.CheckSQL();");
                        sw.WriteLine("\t\t\tret=(List<" + toRecordClassName + ">)");
                        sw.WriteLine("\t\t\t___table.Where(condition)");
                        sw.WriteLine("\t\t\t.FetchAll<" + toRecordClassName + ">() ; ");
                        sw.WriteLine("\t\t\treturn ret;");
                        sw.WriteLine("\t\t}");
                    }
                }
            }
            /*產生LinkFill_XXXXX*/
            foreach (var tmp in XDBs)
            {
                foreach (var tmp2 in tmp.rels)
                {

                    /*由我去別人家的*/
                    if (tmp2.Parent.ToUpper() == source.objectName.ToUpper().Split('.')[1])
                    {
                        string byName = "";
                        for (int i = 0; i < tmp2.ParentColumnName.Count; i++)
                        {
                            //byName.Add(tmp2.ChildColumnName[i].columnName.ToUpper());
                            byName = tmp2.ChildColumnName[i].columnName.ToUpper() + "_";
                        }
                        //tmp2.Child      去他家
                        //算出他家的record的classname
                        string toRecordClassName = getRecordFileName(tmp2.Child).Split('.')[0];
                        string gotoClassName = getTableFileName(tmp2.Child).Split('.')[0];
                        sw.WriteLine("\t\tpublic " + gotoClassName + " " + getLinkFillMethodName(tmp2.Child, byName) + "()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\tvar data = " + getLinkMethodName(tmp2.Child, byName) + "();");

                        sw.WriteLine("\t\t\t" + gotoClassName + " ret=new " + gotoClassName + "(data);");
                        sw.WriteLine("\t\t\treturn ret;");
                        sw.WriteLine("\t\t}");
                    }
                }
            }
            sw.WriteLine("\t}");
            sw.WriteLine("}");
            sw.Flush();
            sw = null;

        }


        private string getLinkMethodName(string name)
        {
            string ret = "Link_";
            ret += getTableFileName(name).Split('.')[0];
            return ret;
        }

        private string getLinkMethodName(string name,string byName)
        {
            string ret = "Link_";
            ret += getTableFileName(name).Split('.')[0]+"_By_"+byName;
            return ret;
        }

        private string getLinkFillMethodName(string name)
        {
            string ret = "LinkFill_";
            ret += getTableFileName(name).Split('.')[0];
            return ret;
        }

        private string getLinkFillMethodName(string name,string byName)
        {
            string ret = "LinkFill_";
            ret += getTableFileName(name).Split('.')[0] + "_By_" + byName;
            return ret;
        }
        private string getTableMethodName(string name)
        {
            string ret = "get";
            ret += getTableFileName(name).Split('.')[0] ;

            return formatMethodString(ret);
        }
        private string getTableMethodName(string name, string byName)
        {
            string ret = "get";
            ret += getTableFileName(name).Split('.')[0] + "_By_" + byName;
            return ret;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadRegistry();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            saveRegistry();
            rtbResult.Text = "";
            builder();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click_2(object sender, EventArgs e)
        {
            if (this.txtModelPath.Text.Trim().Length > 0)
            {
                try
                {
                    openFileDialog1.FileName = this.txtModelPath.Text;
                }
                catch { }
            }

            openFileDialog1.Filter = "(*.xsd)|*.xsd";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                this.txtModelPath.Text = openFileDialog1.FileName;
                saveRegistry();
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.txtProjectRootPath.Text.Trim().Length > 0) {
                try
                {
                    folderBrowserDialog1.SelectedPath = this.txtProjectRootPath.Text;
                }
                catch { }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtProjectRootPath.Text = folderBrowserDialog1.SelectedPath;
                saveRegistry();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.txtOutputPath.Text.Trim().Length > 0)
            {
                try
                {
                    folderBrowserDialog1.SelectedPath = this.txtOutputPath.Text;
                }
                catch { }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtOutputPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void saveRegistry()
        {
            RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\LdBuilderCode");
            key.SetValue("Namespace", txtNamespace.Text);
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\LdBuilderCode");
            key.SetValue("ProjectRootPath", this.txtProjectRootPath.Text);
            key.Close();
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\LdBuilderCode");
            key.SetValue("ModelPath", this.txtModelPath.Text);
            key.Close();
        }

        private void loadRegistry()
        {
            try
            {
                RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\LdBuilderCode");
                if (key.GetValue("Namespace") != null)
                {
                    this.txtNamespace.Text = key.GetValue("Namespace").ToString();
                }
                key.Close();

                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\LdBuilderCode");
                if (key.GetValue("ProjectRootPath") != null)
                {
                    this.txtProjectRootPath.Text = key.GetValue("ProjectRootPath").ToString();
                }

                key.Close(); key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\LdBuilderCode");
                if (key.GetValue("ModelPath") != null)
                {
                    this.txtModelPath.Text = key.GetValue("ModelPath").ToString();
                }
                key.Close();
            }
            catch { }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }
        
    }

    public class XDBSource
    {
        public string objectName;
        public string objectView;
        public List<PKcolumn> ConstraintCol = new List<PKcolumn>();
        public List<Xcolumn> cols = new List<Xcolumn>();
        public List<Relation> rels = new List<Relation>();

        public class Xcolumn
        {
            public string columnName = null;
            public string columnType = null;
            public int? columnLimit = null;
            public Xcolumn()
            {

            }
        }

        public class PKcolumn
        {
            public string columnName = null;
            public string constraintName = null;
            public PKcolumn()
            {

            }
        }

        public class RelationColumn
        {
            public string columnName = null;
            public RelationColumn()
            {

            }
        }

        public class Relation
        {
            public string Parent = null;
            public string Child = null;
            public string RelationName = null;
            public List<RelationColumn> ParentColumnName = new List<RelationColumn>();
            public List<RelationColumn> ChildColumnName = new List<RelationColumn>();
            public Relation()
            {

            }
        }

      
        
     

    }

}
