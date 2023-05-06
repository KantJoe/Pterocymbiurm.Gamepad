using Pterocymbiurm.Devices.ENUM;
using Pterocymbiurm.Devices.Transfer;
using Pterocymbiurm.Transfer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices
{
    public class DeviceBus : IDisposable
    {
        private Timer _timer;
        /// <summary>
        /// developer should avoiding duplicate device when app running
        /// <param name="TimmingType">input that support parallel render</param>
        /// <param name="List">multiple supporting</param>
        /// <param name="string">input device name, be aware of grouping design</param>
        /// <param name="func">render optimization : supporting different timer sync</param>
        /// </summary>
        private static readonly List<KeyValuePair<TimmingType, List<KeyValuePair<string, Func<object, InputDeviceData>>>>> _inputDeviceController;

        private static readonly Dictionary<TimmingType, List<KeyValuePair<string, Func<object, InputDeviceData>>>> _inputDeviceController_Pending;

        /// <summary>
        /// Output actuator proxy for fetch input data
        /// </summary>
        public static readonly List<KeyValuePair<string, InputDeviceData>> IOHubProxy;

        static DeviceBus()
        {
            IOHubProxy = new List<KeyValuePair<string, InputDeviceData>>();
            _inputDeviceController_Pending = new Dictionary<TimmingType, List<KeyValuePair<string, Func<object, InputDeviceData>>>>();
            foreach (TimmingType val in Enum.GetValues(typeof(TimmingType)))
            {
                _inputDeviceController_Pending.Add(val, new List<KeyValuePair<string, Func<object, InputDeviceData>>>());
            }

            _inputDeviceController = _inputDeviceController_Pending.ToList();
        }

        public void GoRunning()
        {
            //_timer = new Timer(this.OnTimerCallback, null, 0, 100);
        }

        public static bool Register(TimmingType timmingType, string name, Func<object, InputDeviceData> func)
        {
            var succeed = false;
            lock (_inputDeviceController_Pending[timmingType])
            {
                if (!_inputDeviceController.Exists(w => w.Key == timmingType && w.Value.Exists(e => e.Key == name)))
                {
                    _inputDeviceController_Pending[timmingType].Add(
                        new KeyValuePair<string, Func<object, InputDeviceData>>(name, func ?? InputDeviceData.Empty));

                    succeed = true;
                }
                else
                {
                    // TODO:should throw or log
                }
            }

            return succeed;
        }

        public void OnTimerCallback(object? state)
        {
            Parallel.ForEach(_inputDeviceController, deviceTransfer =>
            {
                deviceTransfer.Value.ForEach(
                    input =>
                    {
                        input.Value.Invoke(state);

                    });

                if (_inputDeviceController_Pending[deviceTransfer.Key].Count == 0)
                {
                    return;
                }

                lock (_inputDeviceController_Pending[deviceTransfer.Key])
                {
                    var list = _inputDeviceController_Pending[deviceTransfer.Key];
                    deviceTransfer.Value.AddRange(list);
                    _inputDeviceController_Pending[deviceTransfer.Key] = new List<KeyValuePair<string, Func<object, InputDeviceData>>>();
                }
            });
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _timer = null;
        }
    }
}
