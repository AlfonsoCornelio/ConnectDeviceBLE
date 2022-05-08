using ConnectDeviceBLE.entity;
using System;
using System.Threading;

namespace ConnectDeviceBLE
{
    public abstract class OrderTask 
    {
        public static long DEFAULT_DELAY_TIME = 3000;
        public static int RESPONSE_TYPE_NOTIFY = 2;
        public static int RESPONSE_TYPE_WRITE_NO_RESPONSE = 3;
        public static int ORDER_STATUS_SUCCESS = 1;
        public OrderType orderType;
        public OrderEnum order;
        public MokoOrderTaskCallback callback;
        public OrderTaskResponse response;
        public long delayTime = DEFAULT_DELAY_TIME;
        public int orderStatus;

        public OrderTaskResponse getResponse()
        {
            return response;
        }

        public void setResponse(OrderTaskResponse response)
        {
            this.response = response;
        }

        public OrderTask(OrderType orderType, OrderEnum order, MokoOrderTaskCallback callback, int responseType)
        {
            response = new OrderTaskResponse();
            this.orderType = orderType;
            this.order = order;
            this.callback = callback;
            this.response.order = order;
            this.response.responseType = responseType;
        }

        public abstract byte[] assemble();

        public MokoOrderTaskCallback getCallback()
        {
            return callback;
        }

        public void setCallback(MokoOrderTaskCallback callback)
        {
            this.callback = callback;
        }

        public OrderEnum getOrder()
        {
            return order;
        }

        public void setOrder(OrderEnum order)
        {
            this.order = order;
        }

        public void parseValue(byte[] value)
        {
        }

        void RunInterface()
        {
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }

        //Thread timeoutRunner = new Thread(new ThreadStart(Run));
        //newThread.Start();

        public void Run()
        {
            Console.WriteLine("Remover mensaje: Running in a different thread.");
            if (orderStatus != OrderTask.ORDER_STATUS_SUCCESS)
            {
                if (timeoutPreTask())
                {
                    MokoSupport.getInstance().pollTask();
                    callback.onOrderTimeout(response);
                    MokoSupport.getInstance().executeTask(callback);
                }
            }
        }

        public Boolean timeoutPreTask()
        {
            Console.WriteLine(order.getOrderName() + " se acabó el tiempo");
            return true;
        }
    }
}