using Pterocymbiurm.Devices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Pterocymbiurm.Gamepad.WpfUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Task _task;
        private CancellationTokenSource _tokenSource;
        public static DeviceBus DeviceBus { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DeviceBus = new DeviceBus();
            _tokenSource = new CancellationTokenSource();
            _task = Task.Factory.StartNew(DataRender, _tokenSource.Token);

            var r=AllowSetForegroundWindow(Process.GetCurrentProcess().Id);
        }

        private void DataRender()
        {
            while (!_tokenSource.IsCancellationRequested && !_tokenSource.Token.IsCancellationRequested)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {

                    DeviceBus.OnTimerCallback(null);

                    //var _hnwd = new WindowInteropHelper(this.MainWindow).Handle;
                    //var ret = LockSetForegroundWindow(2);
                    //ret &= SetForegroundWindow(_hnwd);
                }, System.Windows.Threading.DispatcherPriority.SystemIdle);

                Task.Delay(1000).Wait();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _tokenSource.Cancel();
            _task.Wait();
        }

        #region 强制获取焦点 防止手柄停止获取设备数据 https://learn.microsoft.com/zh-cn/gaming/gdk/_content/gc/input/overviews/input-fundamentals

        /// <summary>
        /// https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-allowsetforegroundwindow
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(IntPtr pID);

        /// <summary>
        /// https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-locksetforegroundwindow
        /// </summary>
        /// <param name="uLockCode">1 禁用对 SetForegroundWindow 的调用。2 启用对 SetForegroundWindow 的调用。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool LockSetForegroundWindow(uint uLockCode);

        /// <summary>
        /// https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-setforegroundwindow?redirectedfrom=MSDN
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion
    }
}
