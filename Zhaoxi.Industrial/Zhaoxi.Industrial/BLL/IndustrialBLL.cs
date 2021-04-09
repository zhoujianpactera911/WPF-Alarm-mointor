using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Communication.Modbus;
using Zhaoxi.Industrial.DAL;
using Zhaoxi.Industrial.Model;
using System.Data;
using System.Windows;

namespace Zhaoxi.Industrial.BLL
{
    public class IndustrialBLL
    {
        DataAccess dataAccess = DataAccess.GetInstance();
        public DataResult<ModbusSerialInfo> InitSerialInfo()
        {
            DataResult<ModbusSerialInfo> result = new DataResult<ModbusSerialInfo>();
            result.State = false;

            try
            {
                ModbusSerialInfo SerialInfo = new ModbusSerialInfo();
                SerialInfo.PortName = ConfigurationManager.AppSettings["port"];
                SerialInfo.BaudRate = int.Parse(ConfigurationManager.AppSettings["baud"].ToString());
                SerialInfo.DataBit = int.Parse(ConfigurationManager.AppSettings["data_bit"].ToString());
                SerialInfo.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), ConfigurationManager.AppSettings["parity"].ToString(), true);
                SerialInfo.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), ConfigurationManager.AppSettings["stop_bit"].ToString(), true);

                result.State = true;
                result.Data = SerialInfo;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public DataResult<List<StorageModel>> InitStorageArea()
        {
            DataResult<List<StorageModel>> result = new DataResult<List<StorageModel>>();
            result.State = false;

            try
            {
                var sa = dataAccess.GetStorageArea();

                result.Data = (from q in sa.AsEnumerable()
                               select new StorageModel
                               {
                                   StorageID = q.Field<string>("id"),
                                   SlaveAddress = q.Field<Int32>("slave_add"),
                                   FuncCode = q.Field<string>("func_code"),
                                   StorageName = q.Field<string>("s_area_name"),
                                   StartAddress = int.Parse(q.Field<string>("start_reg")),
                                   Length = int.Parse(q.Field<string>("length"))
                               }).ToList();
                result.State = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public DataResult<List<DeviceModel>> InitDevices()
        {
            DataResult<List<DeviceModel>> result = new DataResult<List<DeviceModel>>();
            result.State = false;

            try
            {
                var devices = dataAccess.GetDevices();
                var monitorValues = dataAccess.GetMonitorValues();

                List<DeviceModel> deviceList = new List<DeviceModel>();
                // 设备
                foreach (var d in devices.AsEnumerable())
                {
                    DeviceModel dm = new DeviceModel();
                    deviceList.Add(dm);

                    dm.DeviceID = d.Field<string>("d_id");
                    dm.DeviceName = d.Field<string>("d_name");
                    dm.IsWarning = false;
                    dm.IsRunning = true;

                    // 点位
                    foreach (var m in monitorValues.AsEnumerable().Where(m => m.Field<string>("d_id") == dm.DeviceID))
                    {
                        MonitorValueModel mvm = new MonitorValueModel();
                        dm.MonitorValueList.Add(mvm);

                        mvm.ValueId = m.Field<string>("value_id");
                        mvm.ValueName = m.Field<string>("value_name");
                        mvm.StorageAreaID = m.Field<string>("s_area_id");
                        mvm.StartAddress = m.Field<Int32>("address");
                        mvm.DataType = m.Field<string>("data_type");
                        mvm.IsAlarm = m.Field<Int32>("is_alarm") == 1;
                        mvm.ValueDesc = m.Field<string>("description");
                        mvm.Unit = m.Field<string>("unit");
                        mvm.ValueState = Base.MonitorValueState.OK;

                        // 警戒值
                        var column = m.Field<string>("alarm_lolo");
                        mvm.LoLoAlarm = column == null ? 0.0 : double.Parse(column);
                        column = m.Field<string>("alarm_low");
                        mvm.LowAlarm = column == null ? 0.0 : double.Parse(column);
                        column = m.Field<string>("alarm_high");
                        mvm.HighAlarm = column == null ? 0.0 : double.Parse(column);
                        column = m.Field<string>("alarm_hihi");
                        mvm.HiHiAlarm = column == null ? 0.0 : double.Parse(column);

                        mvm.ValueStateChanged = new Action<Base.MonitorValueState, string, string>((state, msg, value_id) =>
                        {
                            try
                            {
                                Application.Current?.Dispatcher.Invoke(() =>
                                {
                                    var index = dm.WarningMessage.ToList().FindIndex(w => w.ValueID == value_id);
                                    if (index > -1)
                                        dm.WarningMessage.RemoveAt(index);

                                    if (state != Base.MonitorValueState.OK)
                                    {
                                        dm.WarningMessage.Add(new WarningMessageModel { ValueID = value_id, Message = msg });

                                        dm.LogList.Add(new LogModel { });
                                    }
                                });

                                var ss = dm.WarningMessage.ToList().Exists(w => w.ValueID.StartsWith(dm.DeviceID));
                                if (dm.IsWarning != ss)
                                    dm.IsWarning = ss;
                            }
                            catch { }
                        });
                    }
                }

                result.State = true;
                result.Data = deviceList;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
