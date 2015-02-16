#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ExtDirect;
using ExtDirect.Direct;
using LK.DB.SQLCreater;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using LKWebTemplate.Model.Basic;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
using LKWebTemplate;
using System.Text;
using LK.Util;
using System.Data;
using System.Diagnostics;
using System.Management;


#endregion

[DirectService("ServerAction")]
public class ServerAction : BaseAction
{
    [DirectMethod("loadCpuInfo", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadCpuInfo(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        #endregion
        try
        {   /*Cloud身份檢查**/

            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }

            /*權限檢查**/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };

            /*是Store操作一下就可能含有分頁資訊。*/
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("SystemName");
            dt.Columns.Add("Name");
            dt.Columns.Add("NumberOfLogicalPRocessors");
            dt.Columns.Add("Caption");
            var cpuInfo = getCpuInfo();
            var newRow = dt.NewRow();
            newRow["SystemName"] = cpuInfo.SystemName;
            newRow["Name"] = cpuInfo.Name;
            newRow["NumberOfLogicalPRocessors"] = cpuInfo.NumberOfLogicalPRocessors;
            newRow["Caption"] = cpuInfo.Caption;
            dt.Rows.Add(newRow);
            return ExtDirect.Direct.Helper.Store.OutputJObject(dt, 0, dt.Rows.Count);


        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadOSInfo", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadOSInfo(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        #endregion
        try
        {   /*Cloud身份檢查**/

            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }

            /*權限檢查* */
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };

            /*是Store操作一下就可能含有分頁資訊。*/
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("OSArchitecture");
            dt.Columns.Add("Caption");
            var ofInfo = getOsInfo();
            var newRow = dt.NewRow();

            newRow["Name"] = ofInfo.Name;
            newRow["OSArchitecture"] = ofInfo.OSArchitecture;
            newRow["Caption"] = ofInfo.Caption;
            dt.Rows.Add(newRow);
            return ExtDirect.Direct.Helper.Store.OutputJObject(dt, 0, dt.Rows.Count);


        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadMemoryInfo", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadMemoryInfo(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        #endregion
        try
        {   /*Cloud身份檢查**/

            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }

            /*權限檢查* */
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };

            /*是Store操作一下就可能含有分頁資訊。*/
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("SystemName");
            dt.Columns.Add("OSArchitecture");
            dt.Columns.Add("Name");

            var osInfo = getOsInfo();
            var newRow = dt.NewRow();
            newRow["Name"] = osInfo.Name;
            newRow["OSArchitecture"] = osInfo.OSArchitecture;
            newRow["Caption"] = osInfo.Caption;
            dt.Rows.Add(newRow);
            var data = JsonHelper.DataTable2JObject(dt, 0, dt.Rows.Count);
            return data;
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadRuntimeCpuMem", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadRuntimeCpuMem(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        //OrderLimit orderLimit = null;
        float cpu, men;
        cpu = 0;
        men = 0;

        #endregion
        try
        {   /*Cloud身份檢查**/

            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }

            /*權限檢查 * */
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };

            /*是Store操作一下就可能含有分頁資訊。*/
            System.Data.DataTable dt = new DataTable();
            string columnName = System.DateTime.Now.ToString("mm:ss");
            if (HttpContext.Current.Application["RunTimeCPU"] != null)
            {
                dt = (DataTable)HttpContext.Current.Application["RunTimeCPU"];
                if (dt.Columns[columnName] == null)
                {
                    dt.Columns.Add(columnName);
                    dt.AcceptChanges();
                }
                getRunTimeCUP(ref cpu, ref men);
                dt.Rows[0][dt.Columns.Count - 1] = cpu;
                dt.Rows[1][dt.Columns.Count - 1] = men;
                dt.AcceptChanges();
                HttpContext.Current.Application["RunTimeCPU"] = dt;
            }
            else
            {
                dt.Columns.Add("TYPE");
                if (dt.Columns[columnName] == null)
                {
                    dt.Columns.Add(System.DateTime.Now.ToString("mm:ss"));
                }
                var drNew = dt.NewRow();
                drNew["TYPE"] = "CPU";
                getRunTimeCUP(ref cpu, ref men);
                drNew[dt.Columns.Count - 1] = cpu;
                dt.Rows.Add(drNew);

                var drNew2 = dt.NewRow();
                drNew2["TYPE"] = "MEN";
                drNew2[dt.Columns.Count - 1] = men;
                dt.Rows.Add(drNew2);

                HttpContext.Current.Application["RunTimeCPU"] = dt;
            }

            while (dt.Columns.Count > 50)
            {
                dt.Columns.RemoveAt(1);
            }
            dt.AcceptChanges();
            //getMemoryTotal();
            return ExtDirect.Direct.Helper.Store.OutputJObject(dt, 0, dt.Rows.Count);
            //var  data  = JsonHelper.DataTable2JObject(dt, 0, dt.Rows.Count);

            //
            //return data;
            //return ExtDirect.Direct.Helper.Store.OutputJObject(data, dt.Rows.Count);            
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadDisk", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadDisk(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel modBasic = new BasicModel();
        ApplicationHead tblApplication = new ApplicationHead();
        //OrderLimit orderLimit = null;
        //float cpu, men;
        //cpu = 0;
        // men = 0;

        #endregion
        try
        {   /*Cloud身份檢查**/

            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }

            /*權限檢查* */
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };

            /*是Store操作一下就可能含有分頁資訊。*/
            System.Data.DataTable dt = new DataTable();

            string output = runWMI("logicaldisk get DeviceId,DriveType,FreeSpace,Size");


            dt.Columns.Add("DeviceID");
            dt.Columns.Add("DriveType");
            dt.Columns.Add("FreeSpace");
            dt.Columns.Add("Size");
            dt.AcceptChanges();

            for (var i = 0; i < output.Split('\n').Length; i++)
            {
                if (i == 0)
                    continue;

                var tmpRow = output.Split('\n')[i];

                while (tmpRow.IndexOf("  ") > 0)
                {
                    tmpRow = tmpRow.Replace("  ", " ");
                }
                var newRow = dt.NewRow();
                try
                {
                    newRow["DeviceID"] = tmpRow.Split(' ')[0];
                }
                catch { }
                try
                {
                    newRow["DriveType"] = tmpRow.Split(' ')[1];
                }
                catch { }
                try
                {
                    newRow["FreeSpace"] = tmpRow.Split(' ')[2];
                }
                catch { }
                try
                {
                    newRow["Size"] = tmpRow.Split(' ')[3];
                }
                catch { }
                dt.Rows.Add(newRow);
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FreeSpace"].ToString().Trim().ToString() == "")
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
            return ExtDirect.Direct.Helper.Store.OutputJObject(dt, 0, dt.Rows.Count);
        }
        catch (Exception ex)
        {
            log.Error(ex);
            LK.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    private void getRunTimeCUP(ref float cpu, ref float men)
    {
        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        cpuCounter = new PerformanceCounter();
        cpuCounter.CategoryName = "Processor";
        cpuCounter.CounterName = "% Processor Time";
        cpuCounter.InstanceName = "_Total";
        ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        var firstValue = cpuCounter.NextValue();
        System.Threading.Thread.Sleep(1000);
        cpu = cpuCounter.NextValue();
        men = ramCounter.NextValue();
    }

    private Int64 getMemoryTotal()
    {
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = "wmic";
        p.StartInfo.Arguments = "memorychip get capacity";
        p.Start();
        string output = p.StandardOutput.ReadToEnd();
        var total = System.Convert.ToInt64(output.Split(' ')[4].Trim()) / 1024 / 1024;
        p.WaitForExit();

        return total;
    }

    private CPU getCpuInfo()
    {
        CPU cpu = new CPU();
        if (HttpContext.Current.Application["CPU"] == null)
        {
            cpu.Caption = runWMI("cpu get caption").Split('\n')[1];
            cpu.Name = runWMI("cpu get name").Split('\n')[1];
            cpu.SystemName = runWMI("cpu get SystemName").Split('\n')[1];
            cpu.NumberOfLogicalPRocessors = runWMI("cpu get NumberOfLogicalPRocessors").Split('\n')[1];
            cpu.Caption = cpu.Caption.Trim();
            cpu.Name = cpu.Name.Trim();
            cpu.NumberOfLogicalPRocessors = cpu.NumberOfLogicalPRocessors.Trim();
            cpu.SystemName = cpu.SystemName.Trim();

            HttpContext.Current.Application["CPU"] = cpu;

        }
        return (CPU)HttpContext.Current.Application["CPU"];
    }

    private OS getOsInfo()
    {
        OS os = new OS();
        if (HttpContext.Current.Application["OS"] == null)
        {
            os.Caption = runWMI("os get caption").Split('\n')[1];
            os.Name = runWMI("os get name").Split('\n')[1];
            os.OSArchitecture = runWMI("os get OSArchitecture").Split('\n')[1];

            os.Caption = os.Caption.Trim();
            os.Name = os.Name.Trim().Split('|')[0];
            os.OSArchitecture = os.OSArchitecture.Trim();
            HttpContext.Current.Application["OS"] = os;
        }
        return (OS)HttpContext.Current.Application["OS"];
    }

    private string runWMI(string property)
    {
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = "wmic";
        p.StartInfo.Arguments = property;
        p.Start();
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        return output;

    }

    private class CPU
    {
        public string Caption;
        public string Name;
        public string NumberOfLogicalPRocessors;
        public string SystemName;
    }

    private class OS
    {
        public string Caption;
        public string Name;
        public string OSArchitecture;
    }




}



