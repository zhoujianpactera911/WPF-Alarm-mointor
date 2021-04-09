using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Base;

namespace Zhaoxi.Industrial.Model
{
    public class LogModel
    {
        public string DeviceId { get; set; }
        public int Num { get; set; }
        public string DeviceName { get; set; }
        public string LogInfo { get; set; }
        public string Message { get; set; }
        public double Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LogType LogType { get; set; } = LogType.Info;
    }
}
