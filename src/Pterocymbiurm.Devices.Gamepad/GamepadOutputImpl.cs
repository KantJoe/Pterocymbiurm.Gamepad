using Pterocymbiurm.Devices.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices.Gamepad
{
    public class GamepadOutputImpl : DeviceBase, IGamepadOutput<GamepadOutputOptions>
    {
        public TimmingType TimmingType => throw new NotImplementedException();

        public DeviceStatus DeviceStatus => throw new NotImplementedException();

        public GamepadOutputOptions DeviceOptions => throw new NotImplementedException();

        public DeviceStatus Initializing(GamepadOutputOptions options, TimmingType timmingType)
        {
            throw new NotImplementedException();
        }

        public DeviceStatus PauseCapture()
        {
            throw new NotImplementedException();
        }

        public DeviceStatus Recoverying()
        {
            throw new NotImplementedException();
        }

        public DeviceStatus StartCapture()
        {
            throw new NotImplementedException();
        }

        public DeviceStatus StopCapture()
        {
            throw new NotImplementedException();
        }
    }
}
