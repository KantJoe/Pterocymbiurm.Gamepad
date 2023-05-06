using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Pterocymbiurm.Devices;
using Pterocymbiurm.Devices.ENUM;
using Pterocymbiurm.Devices.Gamepad;
using Pterocymbiurm.Devices.Gamepad.winmm;
using Pterocymbiurm.Devices.Transfer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Pterocymbiurm.Gamepad.WpfUI.ViewModels
{
    internal class GamepadInputViewModel : ObservableObject, IDisposable
    {
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                SetProperty(ref _seriesCollection, value, nameof(SeriesCollection));
            }
        }


        private double _maxYValue;
        public double MaxYValue
        {
            get => _maxYValue;
            set
            {
                SetProperty(ref _maxYValue, value, nameof(MaxYValue));
            }
        }

        private string[] _labels;
        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                SetProperty(ref _labels, value, nameof(Labels));
            }
        }

        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get => _formatter;
            set
            {
                SetProperty(ref _formatter, value, nameof(Formatter));
            }
        }

        private IGamepadInput<GamepadInputOptions> _gamepad;
        //public DeviceBase<GamepadOptions> Gamepad
        //{
        //    get => _gamepad;
        //    set
        //    {
        //        SetProperty(ref _gamepad, value);
        //    }
        //}

        /// <summary>
        /// 定时器
        /// </summary>
        private Timer _captureTimer;

        public GamepadInputViewModel()
        {
            _gamepad = new GamepadInputImpl();

            MaxYValue = 65535;
            SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        //Title = "2015",
                        MaxColumnWidth=35,
                        ColumnPadding=1,
                        Values = new ChartValues<ObservableValue> {
                        }
                    }
                };
            Labels = new[] {
                    "dwSize", "dwFlags", "dwXpos", "dwYpos",
                    "dwZpos", "dwRpos", "dwUpos", "dwVpos",
                    "dwButton","dwButtonNumber",};
            Formatter = value => value.ToString("N");
            _gamepad.Initializing(new GamepadInputOptions { GamepadID = 0, Period = 50 }, TimmingType.Parallel_Gamepad);
        }

        public void StartCapture()
        {
            if (_gamepad.TimmingType == TimmingType.Parallel_Gamepad)
            {
                if (_captureTimer != null)
                {
                    _captureTimer.Dispose();
                }

                var name = $"GamepadViewModel:Gamepad:{_gamepad.DeviceOptions.GamepadID}";
                DeviceBus.Register(_gamepad.TimmingType, name, DataSync);
                _gamepad.StartCapture();
                _captureTimer = new Timer(this.OnTimerCallback, null, 0, _gamepad.DeviceOptions.Period);
            }
        }

        public void StopCapture()
        {
            _gamepad.StopCapture();
            _captureTimer.DisposeAsync().GetAwaiter();
            _captureTimer = null;
        }

        private int counting = 0;
        private void OnTimerCallback(object? state)
        {
            counting++;
            _gamepad.OnTimerCallback(state);
        }

        InputDeviceData _previosData = new InputDeviceData { Data = new JOYINFOEX() };
        public InputDeviceData DataSync(object state)
        {
            counting -= 20;
            var resultEx = _gamepad.JoyInfoex;
            if (resultEx == (JOYINFOEX)_previosData.Data)
            {
                if (_gamepad.DeviceStatus != _previosData.DeviceStatus.State)
                {
                    _previosData.DeviceStatus = _gamepad.DeviceStatus;
                }

                return _previosData;
            }

            //Application.Current.Dispatcher.Invoke(() =>
            //{
            SeriesCollection[0].Values = new ChartValues<ObservableValue>
                {
                    new ObservableValue(resultEx.dwSize),
                    new ObservableValue(resultEx.dwFlags),
                    new ObservableValue(resultEx.dwXpos),
                    new ObservableValue(resultEx.dwYpos),
                    new ObservableValue(resultEx.dwZpos),
                    new ObservableValue(resultEx.dwRpos),
                    new ObservableValue(resultEx.dwUpos),
                    new ObservableValue(resultEx.dwVpos),
                    new ObservableValue(resultEx.dwButtons),
                    new ObservableValue(resultEx.dwButtonNumber),
                };
            //});

            _previosData.Data = resultEx;

            if (_gamepad.DeviceStatus != _gamepad.DeviceStatus.State)
            {
                _previosData.DeviceStatus = _gamepad.DeviceStatus;
            }

            return _previosData;
        }

        public void Dispose()
        {
            if (_gamepad.DeviceStatus == DeviceStatus.DeviceState.Running)
            {
                _gamepad.StopCapture();
                _captureTimer.Dispose();
            }

            _captureTimer = null;
        }
    }
}
