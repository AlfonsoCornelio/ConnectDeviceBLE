using ConnectDeviceBLE.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectDeviceBLE
{
    public class MokoSupport : MokoResponseCallback
    {
        public static int HANDLER_MESSAGE_WHAT_CONNECTED = 1;
        public static int HANDLER_MESSAGE_WHAT_DISCONNECTED = 2;
        public static int HANDLER_MESSAGE_WHAT_SERVICES_DISCOVERED = 3;
        public static int HANDLER_MESSAGE_WHAT_DISCONNECT = 4;
        public static int HANDLER_MESSAGE_WHAT_RECONNECT = 5;
        // Hora de finalización del escaneo
        private static long SCAN_PERIOD = 5000;

        private BlockingQueue<OrderTask> mQueue;

        //private BluetoothAdapter mBluetoothAdapter;
        //private BluetoothGatt mBluetoothGatt;
        //private BlockingQueue<OrderTask> mQueue;

        public void pollTask()
        {
            mQueue.poll();
        }


    }
}
