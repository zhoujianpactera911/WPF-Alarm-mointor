using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.Industrial.Base;

namespace Zhaoxi.Industrial
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ///启动全局监控
            GlobalMonitor.Start(
                 // 串口打开成功时回调，打开主窗口
                 () =>
                 {
                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         new MainWindow().Show();
                     });
                 },
                 // 串口打开失败时回调，错误消息提醒，并退出程序
                 (msg) =>
                 {
                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         MessageBox.Show(msg, "异常提示");
                         Application.Current.Shutdown();
                     });
                 });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            GlobalMonitor.Dispose();

            base.OnExit(e);
        }
    }
}
