using Pterocymbiurm.Devices.ENUM;

namespace Pterocymbiurm.Devices
{
    public interface IDevice<TOption> where TOption : DeviceOptions
    {
        TimmingType TimmingType { get; }

        DeviceStatus DeviceStatus { get; }

        TOption DeviceOptions { get; }

        DeviceStatus Initializing(TOption options, TimmingType timmingType);

        DeviceStatus StartCapture();

        DeviceStatus PauseCapture();

        DeviceStatus StopCapture();

        DeviceStatus Recoverying();
    }
}
