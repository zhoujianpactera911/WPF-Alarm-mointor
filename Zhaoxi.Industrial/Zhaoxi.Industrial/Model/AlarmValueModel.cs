using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Base;

namespace Zhaoxi.Industrial.Model
{
    public class AlarmValueModel : NotifyPropertyBase
    {
        public Action<MonitorValueState, string, double> ValueStateChanged { get; set; }

        public double LoLoAlarm { get; set; }
        public double LowAlarm { get; set; }
        public double HighAlarm { get; set; }
        public double HiHiAlarm { get; set; }

        private double _currentValue;

        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value; this.RaisePropertyChanged();

                MonitorValueState state = MonitorValueState.OK;
                string msg = ValueDesc;

                if (value < LoLoAlarm)
                {
                    state = MonitorValueState.LoLo;
                    msg += "极低";
                }
                else if (value < LowAlarm)
                {
                    state = MonitorValueState.Low;
                    msg += "过低";
                }
                else if (value > HiHiAlarm)
                {
                    state = MonitorValueState.HiHi;
                    msg += "极高";
                }
                else if (value > HighAlarm)
                {
                    state = MonitorValueState.High;
                    msg += "过高";
                }
                ValueStateChanged?.Invoke(state, msg, value);
            }
        }
        public string ValueDesc { get; set; }

    }
}
