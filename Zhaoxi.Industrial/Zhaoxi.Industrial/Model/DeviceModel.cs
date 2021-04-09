using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Base;

namespace Zhaoxi.Industrial.Model
{
    public class DeviceModel : NotifyPropertyBase
    {
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; this.RaisePropertyChanged(); }
        }

        private bool _isWarning;

        public bool IsWarning
        {
            get { return _isWarning; }
            set { _isWarning = value; this.RaisePropertyChanged(); }
        }

        public ObservableCollection<WarningMessageModel> WarningMessage { get; set; } = new ObservableCollection<WarningMessageModel>();

        public ObservableCollection<MonitorValueModel> MonitorValueList { get; set; } = new ObservableCollection<MonitorValueModel>();


        public List<LogModel> LogList { get; set; } = new List<LogModel>();
    }
}
