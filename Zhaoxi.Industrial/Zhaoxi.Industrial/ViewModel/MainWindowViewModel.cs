using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Industrial.Model;

namespace Zhaoxi.Industrial.ViewModel
{
     public class MainWindowViewModel
    {
        private TabType tabType = TabType.C;
        public TabType TabType
        {
            get
            {
                return tabType;
            }
            set
            {
                tabType = value;
            }
        }

    }
}
