using System;

namespace ConnectDeviceBLE
{
    [Serializable]
    public class OrderType
    {
        private string Uuid;
        private string Name;

        OrderType(string name, string uuid) {
            Name = name;
            Uuid = uuid;
            }

        public static readonly OrderType NOTIFY = new OrderType("NOTIFY", "0000ffc2-0000-1000-8000-00805f9b34fb");
        public static readonly OrderType WRITE = new OrderType("WRITE", "0000ffc1-0000-1000-8000-00805f9b34fb");
        public static readonly OrderType READ_CHARACTER = new OrderType("READ_CHARACTER", "0000ffb0-0000-1000-8000-00805f9b34fb");
        public static readonly OrderType WRITE_CHARACTER = new OrderType("WRITE_CHARACTER", "0000ffb1-0000-1000-8000-00805f9b34fb");
        public static readonly OrderType STEP_CHARACTER = new OrderType("STEP_CHARACTER", "0000ffb2-0000-1000-8000-00805f9b34fb");
        public static readonly OrderType HEART_RATE_CHARACTER = new OrderType("HEART_RATE_CHARACTER", "0000ffb3-0000-1000-8000-00805f9b34fb");
    };
}
