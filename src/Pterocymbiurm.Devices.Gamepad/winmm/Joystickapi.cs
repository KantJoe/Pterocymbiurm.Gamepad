using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices.Gamepad.winmm
{
    public class Joystickapi
    {
        public struct JOYCAPS
        {
            public ushort wMid;
            public ushort wPid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public int wXmin;
            public int wXmax;
            public int wYmin;
            public int wYmax;
            public int wZmin;
            public int wZmax;
            public int wNumButtons;
            public int wPeriodMin;
            public int wPeriodMax;
            public int wRmin;
            public int wRmax;
            public int wUmin;
            public int wUmax;
            public int wVmin;
            public int wVmax;
            public int wCaps;
            public int wMaxAxes;
            public int wNumAxes;
            public int wMaxButtons;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szRegKey;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szOEMVxD;
        }


        #region JOYINFOEX.dwFlags值的定义
        public const long JOY_RETURNX = 0x1;
        public const long JOY_RETURNY = 0x2;
        public const long JOY_RETURNZ = 0x4;
        public const long JOY_RETURNR = 0x8;
        public const long JOY_RETURNU = 0x10;
        public const long JOY_RETURNV = 0x20;
        public const long JOY_RETURNPOV = 0x40;
        public const long JOY_RETURNBUTTONS = 0x80;
        public const long JOY_RETURNRAWDATA = 0x100;
        public const long JOY_RETURNPOVCTS = 0x200;
        public const long JOY_RETURNCENTERED = 0x400;
        public const long JOY_USEDEADZONE = 0x800;
        public const long JOY_RETURNALL = (JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ | JOY_RETURNR | JOY_RETURNU | JOY_RETURNV | JOY_RETURNPOV | JOY_RETURNBUTTONS);
        public const long JOY_CAL_READALWAYS = 0x10000;
        public const long JOY_CAL_READRONLY = 0x2000000;
        public const long JOY_CAL_READ3 = 0x40000;
        public const long JOY_CAL_READ4 = 0x80000;
        public const long JOY_CAL_READXONLY = 0x100000;
        public const long JOY_CAL_READYONLY = 0x200000;
        public const long JOY_CAL_READ5 = 0x400000;
        public const long JOY_CAL_READ6 = 0x800000;
        public const long JOY_CAL_READZONLY = 0x1000000;
        public const long JOY_CAL_READUONLY = 0x4000000;
        public const long JOY_CAL_READVONLY = 0x8000000;
        #endregion

        /// <summary>
        /// 检查系统是否配置了游戏端口和驱动程序。如果返回值为零，表明不支持操纵杆功能。如果返回值不为零，则说明系统支持游戏操纵杆功能。
        /// </summary>
        /// <returns></returns>
        [DllImport(GamepadInputOptions.GAMEPAD_DRIVER)]
        public static extern int joyGetNumDevs();

        /// <summary>
        /// 获取某个游戏手柄的参数信息
        /// </summary>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <param name="pjc"></param>
        /// <param name="cbjc">JOYCAPS结构的大小</param>
        /// <returns></returns>
        [DllImport(GamepadInputOptions.GAMEPAD_DRIVER)]
        public static extern ErrorCode joyGetDevCaps(int uJoyID, ref JOYCAPS pjc, int cbjc);

        /// <summary>
        /// 获取操纵杆位置和按钮状态
        /// </summary>
        /// <param name="uJoyID"></param>
        /// <param name="pji"></param>
        /// <returns></returns>
        [DllImport(GamepadInputOptions.GAMEPAD_DRIVER)]
        public static extern ErrorCode joyGetPosEx(int uJoyID, ref JOYINFOEX pji);
    }
}
