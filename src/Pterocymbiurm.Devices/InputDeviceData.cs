using Pterocymbiurm.Devices.ENUM;
using Pterocymbiurm.Transfer;

namespace Pterocymbiurm.Devices.Transfer
{
    public class InputDeviceData : IOData
    {
        public DeviceStatus DeviceStatus { get; set; }

        public static InputDeviceData EmptyData => new();
        public static Func<object, InputDeviceData> Empty { get; set; }

        public static InputDeviceData EmptyFunc()
        {
            return EmptyData;
        }
    }
}
