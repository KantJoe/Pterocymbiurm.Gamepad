using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pterocymbiurm.Devices.ENUM
{
    public struct DeviceStatus
    {
        public DeviceState State { get; set; }

        public string Code { get; set; }

        public string Msg { get; set; }

        public static bool operator ==(DeviceStatus left, DeviceState right)
        {
            return left.State == right;
        }

        public static bool operator !=(DeviceStatus left, DeviceState right)
        {
            return left.State != right;
        }

        public static DeviceStatus Define(
            DeviceState state, string code = null, string msg = null)
        {
            return new DeviceStatus { State = state, Code = code, Msg = msg };
        }

        public enum DeviceState
        {
            None,
            /// <summary>
            /// 已定义
            /// </summary>
            Defined,
            /// <summary>
            /// 已初始化
            /// </summary>
            Initialized,
            /// <summary>
            /// 运行中
            /// </summary>
            Running,
            /// <summary>
            /// 暂停
            /// </summary>
            Paused,
            /// <summary>
            /// 中断
            /// </summary>
            Interrupt,
            /// <summary>
            /// 恢复中，完成后会在Initialized
            /// </summary>
            Recorying,
            /// <summary>
            /// 终止/失去连接/卸载，可控的
            /// </summary>
            Down,
            /// <summary>
            /// 出现问题，不可控的
            /// </summary>
            Error
        }
    }
}
