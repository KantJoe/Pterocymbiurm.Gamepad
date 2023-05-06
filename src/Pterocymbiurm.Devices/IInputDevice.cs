using Pterocymbiurm.Devices.ENUM;
using Pterocymbiurm.Devices.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices
{
    public interface IInputDevice<TOption> : IDevice<TOption> where TOption : DeviceOptions
    {

    }
}
