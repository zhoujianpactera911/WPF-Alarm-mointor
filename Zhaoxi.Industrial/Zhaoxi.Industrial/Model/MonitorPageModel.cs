using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Base;

namespace Zhaoxi.Industrial.Model
{
    public class MonitorPageModel : NotifyPropertyBase
    {
        private bool _isShowDetail;

        public bool IsShowDetail
        {
            get { return _isShowDetail; }
            set { _isShowDetail = value; this.RaisePropertyChanged(); }
        }

        private double _monitorValue1;

        public double GlobalMonitorValue1
        {
            get { return _monitorValue1; }
            set { _monitorValue1 = value; this.RaisePropertyChanged(); }
        }

        private double _monitorValue2;

        public double GlobalMonitorValue2
        {
            get { return _monitorValue2; }
            set { _monitorValue2 = value; this.RaisePropertyChanged(); }
        }

        private double _monitorValue3;

        public double GlobalMonitorValue3
        {
            get { return _monitorValue3; }
            set { _monitorValue3 = value; this.RaisePropertyChanged(); }
        }

        private DeviceModel _deviceModel;

        public DeviceModel DeviceModel
        {
            get { return _deviceModel; }
            set
            {
                _deviceModel = value;

                //if(value.error)
                this.RaisePropertyChanged();

                if (value != null)
                {
                    var error = value.LogList.FirstOrDefault(l => l.DeviceId == value.DeviceID && l.LogType == (LogType)1);
                    if (error != null)
                        NewErrorMsg = error.Message;
                }
            }
        }

        private string _newErrorMsg;

        public string NewErrorMsg
        {
            get { return _newErrorMsg; }
            set { _newErrorMsg = value; this.RaisePropertyChanged(); }
        }


        public ObservableCollection<LogModel> Logs { get; set; } = new ObservableCollection<LogModel>();
    }
}
