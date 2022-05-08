using System;

namespace ConnectDeviceBLE.entity
{
    [Serializable]
    public class OrderTaskResponse
    {
        public OrderEnum order;
        public int responseType;
        public byte[] responseValue;
    }
}
