using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zhaoxi.Industrial.Components
{
    /// <summary>
    /// CoolingPump.xaml 的交互逻辑
    /// </summary>
    public partial class CoolingPump : ComponentBase
    {
        public CoolingPump()
        {
            InitializeComponent();
        }

        private void ComponentBase_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = !this.IsSelected;

            //this.IsRunning = !this.IsRunning;

            //this.IsFaultState = !this.IsFaultState;

            this.Command.Execute(this.CommandParameter);

            e.Handled = true;
        }
    }
}
