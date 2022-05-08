using ConnectDeviceBLE.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectDeviceBLE
{
    public interface MokoOrderTaskCallback
    {
        void onOrderResult(OrderTaskResponse response);

        void onOrderTimeout(OrderTaskResponse response);

        void onOrderFinish();
    }
}
