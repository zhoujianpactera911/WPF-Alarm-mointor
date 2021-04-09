using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Base;
using Zhaoxi.Industrial.Model;

namespace Zhaoxi.Industrial.ViewModel
{
    public class SystemMonitorViewModel
    {
        public MonitorPageModel PageModel { get; set; }
        //public DeviceModel TowerModel1 { get; set; }
        //public DeviceModel TowerModel2 { get; set; }
        //public DeviceModel TowerModel3 { get; set; }
        public CommandBase ComponentCommand { get; set; }

        public SystemMonitorViewModel()
        {
            PageModel = new MonitorPageModel()
            {
                IsShowDetail = false,
                GlobalMonitorValue1 = 100,
                GlobalMonitorValue2 = 63,
                GlobalMonitorValue3 = 38,
            };
            PageModel.Logs.Add(new LogModel { Num = 1, DeviceId = "CT001", DeviceName = "冷却塔1#", LogInfo = "已启动", LogType = 0 });
            PageModel.Logs.Add(new LogModel { Num = 2, DeviceId = "CT002", DeviceName = "冷却塔2#", LogInfo = "已启动", LogType = 0 });
            PageModel.Logs.Add(new LogModel { Num = 3, DeviceId = "CT003", DeviceName = "冷却塔3#", LogInfo = "液位极低", LogType = (LogType)1 });
            PageModel.Logs.Add(new LogModel { Num = 4, DeviceId = "CP001", DeviceName = "循环水泵1#", LogInfo = "频率过大", LogType = (LogType)1 });
            PageModel.Logs.Add(new LogModel { Num = 5, DeviceId = "CP002", DeviceName = "循环水泵2#", LogInfo = "已停机", LogType = 0 });
            PageModel.Logs.Add(new LogModel { Num = 6, DeviceId = "CP003", DeviceName = "循环水泵3#", LogInfo = "已停机", LogType = 0 });

            //this.TowerModel1 = new DeviceModel() { DeviceID = "CT001", DeviceName = "冷却塔 1#", IsRunning = true };
            //this.TowerModel2 = new DeviceModel() { DeviceID = "CT002", DeviceName = "冷却塔 2#", IsRunning = true };
            //this.TowerModel3 = new DeviceModel() { DeviceID = "CT003", DeviceName = "冷却塔 3#", IsWarning = true };

            this.ComponentCommand = new CommandBase(new Action<object>(doTowerCommand));
        }

        private void doTowerCommand(object obj)
        {
            PageModel.IsShowDetail = true;

            PageModel.DeviceModel = obj as DeviceModel;
        }
    }
}
