using Pterocymbiurm.Devices.ENUM;
using Pterocymbiurm.Devices.Gamepad.winmm;
using Pterocymbiurm.Devices.Transfer;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Pterocymbiurm.Devices.Gamepad
{
    /// <summary>
    /// 输入设备
    /// </summary>
    public class GamepadInputImpl : DeviceBase, IGamepadInput<GamepadInputOptions>, IDisposable
    {
        #region properties
        public TimmingType TimmingType
        {
            get => _timmingType;
            protected set
            {
                _timmingType = value;
            }
        }

        public DeviceStatus DeviceStatus
        {
            get => _deviceStatus;
            protected set
            {
                _deviceStatus = value;
            }
        }

        public GamepadInputOptions DeviceOptions
        {
            get => (GamepadInputOptions)_deviceOptions;
            protected set
            {
                _deviceOptions = value;
            }
        }

        private JOYINFOEX _joyInfoex;
        public JOYINFOEX JoyInfoex
        {
            get => _joyInfoex;
        }
        #endregion

        public GamepadInputImpl() : base()
        {
            var stickCount = Joystickapi.joyGetNumDevs();
            if (stickCount <= 0)
            {
                DeviceStatus = DeviceStatus.Define(DeviceStatus.DeviceState.None);
            }
            else
            {
                DeviceStatus = DeviceStatus.Define(DeviceStatus.DeviceState.Defined);
            }
        }

        public DeviceStatus Initializing(
            GamepadInputOptions options, TimmingType timmingType)
        {
            if (DeviceStatus != DeviceStatus.DeviceState.Defined)
            {
                return DeviceStatus;
            }

            base.Initializing(options, timmingType);

            Joystickapi.JOYCAPS pjc = new Joystickapi.JOYCAPS();
            var code = Joystickapi.joyGetDevCaps(
                options.GamepadID, ref pjc, Marshal.SizeOf(typeof(Joystickapi.JOYCAPS)));
            if (code == ErrorCode.JOYERR_NOERROR)
            {
                options.JOYCAPS = pjc;
                DeviceStatus = ToStatus(DeviceStatus.DeviceState.Initialized);
            }
            else
            {
                DeviceStatus = ToStatus(DeviceStatus.DeviceState.Error, code.ToString());
            }

            return DeviceStatus;
        }

        private object _lock = new object();
        public DeviceStatus StartCapture()
        {
            lock (_lock)
            {
                //if (DeviceStatus != DeviceStatus.DeviceState.Initialized)
                //{
                //    return DeviceStatus;
                //}

                DeviceStatus = ToStatus(DeviceStatus.DeviceState.Running);

                return DeviceStatus;
            }
        }

        public void OnTimerCallback(object? state)
        {
            lock (_lock)
            {
                if (DeviceStatus != DeviceStatus.DeviceState.Running)
                {
                    return;
                }

                JOYINFOEX _infoEx = new JOYINFOEX()
                {
                    dwSize = Marshal.SizeOf(typeof(JOYINFOEX)),
                    dwFlags = (int)Joystickapi.JOY_RETURNBUTTONS
                };
                var errorCode = Joystickapi.joyGetPosEx(this.DeviceOptions.GamepadID, ref _infoEx);
                if (errorCode == ErrorCode.JOYERR_NOERROR)
                {
                    _joyInfoex = _infoEx;
                    //_infoEx.dwFlags = (int)Joystickapi.JOY_RETURNBUTTONS;
                }
                else
                {
                    DeviceStatus = ToStatus(DeviceStatus.DeviceState.Interrupt);
                }
            }
        }

        public DeviceStatus PauseCapture()
        {
            lock (_lock)
            {
                DeviceStatus = ToStatus(DeviceStatus.DeviceState.Paused);

                return DeviceStatus;
            }
        }

        public DeviceStatus StopCapture()
        {
            lock (_lock)
            {
                DeviceStatus = ToStatus(DeviceStatus.DeviceState.Down);
                //_joyInfoex = new JOYINFOEX();
                return DeviceStatus;
            }
        }

        public DeviceStatus Recoverying()
        {
            Initializing(DeviceOptions, TimmingType);
            lock (_lock)
            {
                if (DeviceStatus == DeviceStatus.DeviceState.Initialized)
                {
                    StartCapture();
                }

                return DeviceStatus;
            }
        }

        public void Dispose()
        {
            if (DeviceStatus == DeviceStatus.DeviceState.Running)
            {
                StopCapture();
            }
        }
    }
}