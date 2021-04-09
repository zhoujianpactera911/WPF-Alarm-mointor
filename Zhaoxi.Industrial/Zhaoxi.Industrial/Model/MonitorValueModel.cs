using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Base;

namespace Zhaoxi.Industrial.Model
{
    public class MonitorValueModel : NotifyPropertyBase
    {
        public Action<MonitorValueState, string, string> ValueStateChanged { get; set; }

        public string ValueId { get; set; }
        public string ValueName { get; set; }
        public string StorageAreaID { get; set; }
        public int StartAddress { get; set; }
        public string DataType { get; set; }
        public bool IsAlarm { get; set; }
        public double LoLoAlarm { get; set; }
        public double LowAlarm { get; set; }
        public double HighAlarm { get; set; }
        public double HiHiAlarm { get; set; }
        public MonitorValueState ValueState { get; set; }

        private double _currentValue;

        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                this.RaisePropertyChanged();

                if (IsAlarm)
                {
                    string msg = ValueDesc;
                    this.ValueState = MonitorValueState.OK;
                    if (value < LoLoAlarm)
                    {
                        ValueState = MonitorValueState.LoLo;
                        msg += "极低";
                    }
                    else if (value < LowAlarm)
                    {
                        ValueState = MonitorValueState.Low;
                        msg += "过低";
                    } 
                    else if (value > HiHiAlarm)
                    {
                        ValueState = MonitorValueState.HiHi;
                        msg += "极高";
                    }
                    else if (value > HighAlarm)
                    {
                        ValueState = MonitorValueState.High;
                        msg += "过高";
                    }
                    ValueStateChanged?.Invoke(ValueState, msg + "。当前值：" + value, ValueId);
                }

                Values.Add(new ObservableValue(value));
                if (Values.Count > 50) Values.RemoveAt(0);
            }
        }
        public string ValueDesc { get; set; }
        public string Unit { get; set; }
        public ChartValues<ObservableValue> Values { get; set; } = new ChartValues<ObservableValue>();
    }
}
