using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Model;
using System.Configuration;
using Zhaoxi.Communication.Modbus;
using System.Windows;
using Zhaoxi.Communication;
using System.Data;
using Zhaoxi.Industrial.DAL;
using Zhaoxi.Industrial.BLL;
using System.Collections.ObjectModel;

namespace Zhaoxi.Industrial.Base
{
    public class GlobalMonitor
    {
        public static List<StorageModel> StorageList { get; set; }
        public static ObservableCollection<DeviceModel> DeviceList { get; set; } = new ObservableCollection<DeviceModel>();
        public static ModbusSerialInfo SerialInfo { get; set; }


        public static List<ICommunicationBase> CommList = new List<ICommunicationBase>();

        static bool isRunning = true;
        static Task mainTask = null;
        public static void Start(Action successAction, Action<string> faultAction)
        {
            mainTask = Task.Run(new Action(async () =>
             {
                 IndustrialBLL bll = new IndustrialBLL();
                 /// 获取串口配置信息
                 var si = bll.InitSerialInfo();
                 if (si.State)
                     SerialInfo = si.Data;
                 else
                 {
                     faultAction.Invoke(si.Message); return;
                 }

                 /// 初始化存储区
                 var sa = bll.InitStorageArea();
                 if (sa.State)
                     StorageList = sa.Data;
                 else
                 {
                     faultAction.Invoke(sa.Message); return;
                 }

                 /// 初始化设备变量集合以及警戒值
                 var dr = bll.InitDevices();
                 if (dr.State)
                     foreach (var item in dr.Data)
                         DeviceList.Add(item);
                 else
                 {
                     faultAction.Invoke(dr.Message); return;
                 }


                 /// 初始化ModbusRTU串口通信
                 var rtu = RTU.GetInstance(SerialInfo);
                 rtu.Responsed = new Action<int, List<byte>>(ParsingData);
                 CommList.Add(rtu);

                 /// 连接串口
                 if (rtu.Connection())
                 {
                     successAction.Invoke();

                     while (isRunning)
                     {
                         int startAddress = 0;
                         //int errorCount = 0;

                         foreach (var item in StorageList)
                         {
                             if (item.Length > 100)
                             {
                                 startAddress = item.StartAddress;
                                 int readCount = item.Length / 100;
                                 for (int i = 0; i < readCount; i++)
                                 {
                                     int readLength = i == readCount ? item.Length - 100 * i : 100;

                                     await rtu.Send(item.SlaveAddress, (byte)int.Parse(item.FuncCode), startAddress + 100 * i, readLength);
                                 }
                             }
                             if (item.Length % 100 > 0)
                             {
                                 await rtu.Send(item.SlaveAddress, (byte)int.Parse(item.FuncCode), startAddress + 100 * (item.Length / 100), item.Length % 100);
                             }
                             //if (item.StorageName == "01 Coil Status(0x)")
                             //{
                             //}
                             //else if (item.StorageName == "03 Holding Register(4x)")
                             //{
                             //    List<byte> byteList = new List<byte>();
                             //    //如果长度过大，分批次去读，最大不要超过125
                             //    if (item.Length > 100)
                             //    {
                             //        startAddress = item.StartAddress;
                             //        int readCount = item.Length / 100;
                             //        for (int i = 0; i < readCount; i++)
                             //        {
                             //            int readLength = i == readCount ? item.Length - 100 * i : 100;
                             //            List<byte> resp = await rtu.RequestKeepReg(item.SlaveAddress, startAddress + 100 * i, readLength);
                             //            if (resp != null)
                             //            {
                             //                //errorCount = 0;
                             //                byteList.AddRange(resp);
                             //            }
                             //            //else
                             //            //{
                             //            //    errorCount += 1;
                             //            //}

                             //        }
                             //    }

                             //    if (item.Length % 100 > 0)
                             //    {
                             //        List<byte> resp = await rtu.RequestKeepReg(SerialInfo.Address, startAddress, item.Length % 100);
                             //        if (resp != null)
                             //        {
                             //            //errorCount = 0;
                             //            byteList.AddRange(resp);
                             //        }
                             //        //else
                             //        //{
                             //        //    errorCount += 1;
                             //        //}
                             //    }

                             //    //解析
                             //    if (byteList.Count == item.Length * 2)
                             //    {
                             //        ParsingData4x(byteList);
                             //    }
                             //}
                         }
                     }
                 }
                 else
                 {
                     faultAction.Invoke("程序无法启动，串口连接初始化失败！请检查设备是否连接正常。");
                 }
             }));
        }

        private static void ParsingData(int start_addr, List<byte> byteList)
        {
            if (byteList != null && byteList.Count > 0)
            {
                // 查找所有4x区域的变量
                // 03 Holding Register(4x)
                //var sl = StorageList.FirstOrDefault(s => s.StorageName == "03 Holding Register(4x)");
                //if (sl == null) return;

                // 查找所有相关区域的变量
                var mvl = (from q in DeviceList
                           from m in q.MonitorValueList
                           where m.StorageAreaID == (byteList[0].ToString() + byteList[1].ToString("00") + start_addr.ToString()) && q.IsRunning
                           select m).ToList();

                //DeviceList.Select(d => d.MonitorValueList.Where(m => m.StorageAreaID == sl.StorageID));

                int startByte;
                byte[] res = null;
                foreach (var item in mvl)
                {
                    switch (item.DataType)
                    {
                        case "Float":
                            startByte = item.StartAddress * 2 + 3;
                            if (startByte < start_addr + byteList.Count)
                            {
                                res = new byte[4] { byteList[startByte], byteList[startByte + 1], byteList[startByte + 2], byteList[startByte + 3] };
                                item.CurrentValue = Convert.ToDouble(res.ByteArrayToFloat());
                            }
                            break;
                        case "Unsigned":
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void Dispose()
        {
            isRunning = false;
            foreach (var item in CommList)
                item.Close();

            if (mainTask != null)
                mainTask.Wait();
        }
    }
}
