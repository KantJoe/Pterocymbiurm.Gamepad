namespace Pterocymbiurm.Devices
{
    public interface IGamepadInput<T> : IInputDevice<T> where T : DeviceOptions
    {
        JOYINFOEX JoyInfoex { get; }

        void OnTimerCallback(object? state);
    }

    /// <summary>
    /// 游戏手柄的位置与按钮状态
    /// <see cref="https://learn.microsoft.com/zh-cn/windows/win32/api/joystickapi/ns-joystickapi-joyinfoex"/>
    /// 左按键            dwPOV(互斥)
    //  Up	            0
    //  Up & Right	    4500
    //  Right	        9000
    //  Right & Down	13500
    //  Down	        18000
    //  Down & Left 	22500
    //  Left	        27000
    //  Left & Up	    31500
    //
    //  左摇杆             X       Y
    //  None	        32767	32767
    //  Up	            32767	0
    //  Up & Right	    32767	32767
    //  Right	        65535	32767
    //  Right & Down	65535	65535
    //  Down	        32767	65535
    //  Down & Left	    0	65535
    //  Left	        0	32767
    //  Left & Up	    0	0
    //
    //  右摇杆             U       R
    //  None	        32767	32767
    //  Up	            32767	0
    //  Up & Right	    32767	32767
    //  Right	        65535	32767
    //  Right & Down	65535	65535
    //  Down	        32767	65535
    //  Down & Left	    0	65535
    //  Left	        0	32767
    //  Left & Up	    0	0
    //
    //  LT                Z(互斥）
    //  None            32767
    //  Actived         65535
    //
    //  RT                Z(互斥）
    //  None            32767
    //  Actived         0
    //
    //  右按键             Button(或运算)
    //  Y               8
    //  B               2
    //  A               1
    //  X               4
    /// </summary>
    public struct JOYINFOEX
    {
        /// <summary>
        /// Size, in bytes, of this structure.
        /// </summary>
        public int dwSize;
        /// <summary>
        /// Flags indicating the valid information returned in this structure. Members that do not contain valid information are set to zero.
        /// </summary>
        public int dwFlags;
        /// <summary>
        /// Current X-coordinate.
        /// </summary>
        public int dwXpos;
        /// <summary>
        /// Current Y-coordinate.
        /// </summary>
        public int dwYpos;
        /// <summary>
        /// Current Z-coordinate.
        /// </summary>
        public int dwZpos;
        /// <summary>
        /// Current position of the rudder or fourth joystick axis.
        /// </summary>
        public int dwRpos;
        /// <summary>
        /// Current fifth axis position.
        /// </summary>
        public int dwUpos;
        /// <summary>
        /// Current sixth axis position.
        /// </summary>
        public int dwVpos;
        /// <summary>
        /// Current state of the 32 joystick buttons. The value of this member can be set to any combination of JOY_BUTTONn flags, where n is a value in the range of 1 through 32 corresponding to the button that is pressed.
        /// </summary>
        public int dwButtons;
        /// <summary>
        /// Current button number that is pressed.
        /// </summary>
        public int dwButtonNumber;
        /// <summary>
        /// Current position of the point-of-view control. Values for this member are in the range 0 through 35,900. These values represent the angle, in degrees, of each view multiplied by 100. 
        /// </summary>
        public int dwPOV;
        /// <summary>
        /// Reserved; do not use.
        /// </summary>
        public int dwReserved1;
        /// <summary>
        /// Reserved; do not use.
        /// </summary>
        public int dwReserved2;

        public static bool operator !=(JOYINFOEX left, JOYINFOEX right)
        {
            return left.dwSize == right.dwSize && left.dwFlags == right.dwFlags
                && left.dwXpos == right.dwXpos && left.dwYpos == right.dwYpos
                && left.dwZpos == right.dwZpos && left.dwRpos == right.dwRpos
                && left.dwVpos == right.dwVpos && left.dwButtons == right.dwButtons
                && left.dwButtonNumber == right.dwButtonNumber
                && left.dwPOV == right.dwPOV;
        }

        public static bool operator ==(JOYINFOEX left, JOYINFOEX right)
        {
            return left.dwSize == right.dwSize && left.dwFlags == right.dwFlags
                && left.dwXpos == right.dwXpos && left.dwYpos == right.dwYpos
                && left.dwZpos == right.dwZpos && left.dwRpos == right.dwRpos
                && left.dwVpos == right.dwVpos && left.dwButtons == right.dwButtons
                && left.dwButtonNumber == right.dwButtonNumber
                && left.dwPOV == right.dwPOV;
        }
    }

    /// <summary>
    /// error code
    /// class maybe more comvenience
    /// </summary>
    public enum ErrorCode
    {
        JOYERR_NOERROR = 0,
        JOYERR_PARMS = 165,
        JOYERR_NOCANDO = 166,
        JOYERR_UNPLUGGED = 167
    }
}