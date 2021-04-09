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
using Zhaoxi.Controls.Components;
using Zhaoxi.Industrial.ViewModel;

namespace Zhaoxi.Industrial.View
{
    /// <summary>
    /// SystemMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class SystemMonitor : UserControl
    {
        public SystemMonitor()
        {
            InitializeComponent();

            this.DataContext = new SystemMonitorViewModel();

            this.SizeChanged += SystemMonitor_SizeChanged;
        }

        private void SystemMonitor_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.mainView.SetValue(Canvas.LeftProperty, (this.RenderSize.Width - this.mainView.ActualWidth) / 2);
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double width = this.mainView.ActualWidth + e.Delta;
            double height = this.mainView.ActualHeight + e.Delta;
            if (height < 100) height = 100;
            if (width < 500) width = 500;

            this.mainView.Width = width;
            this.mainView.Height = height;

            this.mainView.SetValue(Canvas.LeftProperty, (this.RenderSize.Width - this.mainView.Width) / 2);

            e.Handled = true;
        }

        Point _downPoint = new Point(0, 0);
        bool _isMoving = false;
        double left = 0, top = 0;
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _downPoint = e.GetPosition(sender as Canvas);
            left = double.Parse(this.mainView.GetValue(Canvas.LeftProperty).ToString());
            top = double.Parse(this.mainView.GetValue(Canvas.TopProperty).ToString());

            _isMoving = true;
            (sender as Canvas).CaptureMouse();

            foreach(var item in this.grid.Children)
            {
                if (item is ComponentBase)
                    (item as ComponentBase).IsSelected = false;
            }

            e.Handled = true;
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMoving = false;
            (sender as Canvas).ReleaseMouseCapture();

            e.Handled = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMoving)
            {
                Point currentPoint = e.GetPosition(sender as Canvas);

                this.mainView.SetValue(Canvas.LeftProperty, left + (currentPoint.X - _downPoint.X));
                this.mainView.SetValue(Canvas.TopProperty, top + (currentPoint.Y - _downPoint.Y));

                e.Handled = true;
            }
        }
    }
}
