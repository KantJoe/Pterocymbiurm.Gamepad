using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices
{
    public interface IOutputDevice<TOption> : IDevice<TOption> where TOption : DeviceOptions
    {
    }
}
