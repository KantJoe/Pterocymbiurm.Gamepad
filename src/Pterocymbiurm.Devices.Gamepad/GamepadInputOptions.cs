using Pterocymbiurm.Devices.Gamepad.winmm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices.Gamepad
{
    public class GamepadInputOptions : DeviceOptions
    {
        public int GamepadID { get; set; } = 0;

        public Joystickapi.JOYCAPS JOYCAPS { get; set; }

        public const string GAMEPAD_DRIVER = "winmm.dll";
        /// <summary>
        /// freequency : millsecond
        /// </summary>
        public int Period { get; set; } = 100;
    }
}
