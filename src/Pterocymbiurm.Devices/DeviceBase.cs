using Pterocymbiurm.Devices.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pterocymbiurm.Devices.ENUM.DeviceStatus;

namespace Pterocymbiurm.Devices
{
    public abstract class DeviceBase
    {
        protected virtual DeviceStatus _deviceStatus { get; set; }

        protected virtual TimmingType _timmingType { get; set; }

        protected virtual DeviceOptions _deviceOptions { get; set; }

        protected DeviceBase() { }

        protected virtual DeviceStatus Initializing(
            DeviceOptions options, TimmingType timmingType)
        {
            _deviceOptions = options;
            _timmingType = timmingType;

            return _deviceStatus;
        }

        protected static DeviceStatus ToStatus(
            DeviceState state, string code = null, string msg = null)
        {
            return new DeviceStatus { State = state, Code = code, Msg = msg };
        }
    }

    //public abstract class DeviceBase<TConfig> :  IDisposable
    //{

    //    public virtual TConfig Options { get; protected set; }

    //    public virtual DeviceStatus DeviceStatus { get; protected set; }
    //    public abstract TimmingType TimmingType { get;protected set; }  

    //    public abstract DeviceStatus Initializing(TConfig options);

    //    public abstract DeviceStatus StartCapture();

    //    public DeviceStatus TimerCallback(object state)
    //    {
    //        if (TimmingType == TimmingType.Synchronous)
    //        {
    //            OnTimerCallback(ref state);
    //        }

    //        return DeviceStatus;
    //    }

    //    public abstract void OnTimerCallback(ref object state);

    //    public abstract DeviceStatus PauseCapture();

    //    public abstract DeviceStatus StopCapture();

    //    public abstract DeviceStatus Recoverying();

    //    public abstract void Dispose();

    //    DeviceStatus IDevice.StartCapture()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    DeviceStatus IDevice.TimerCallback(object state)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    DeviceStatus IDevice.PauseCapture()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    DeviceStatus IDevice.StopCapture()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    DeviceStatus IDevice.Recoverying()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
