using System;

namespace ConnectDeviceBLE.entity
{
    [Serializable]
    public class OrderEnum
    {
        private string OrderName;
        private int OrderHeader;

        OrderEnum(string orderName, int orderHeader)
        {
            OrderName = orderName;
            OrderHeader = orderHeader;
        }

        public int getOrderHeader()
        {
            return OrderHeader;
        }

        public String getOrderName()
        {
            return OrderName;
        }

        public static readonly OrderEnum GET_CRC_VERIFY_RESULT = new OrderEnum("CRC校验", 0x28);
        public static readonly OrderEnum OPEN_NOTIFY = new OrderEnum("打开设备通知", 0);
        public static readonly OrderEnum READ_NOTIFY = new OrderEnum("打开读取通知", 0);
        public static readonly OrderEnum WRITE_NOTIFY = new OrderEnum("打开设置通知", 0);
        public static readonly OrderEnum STEP_NOTIFY = new OrderEnum("打开记步通知", 0);
        public static readonly OrderEnum HEART_RATE_NOTIFY = new OrderEnum("打开心率通知", 0);
        public static readonly OrderEnum Z_READ_ALARMS = new OrderEnum("读取闹钟", 0x01);
        public static readonly OrderEnum Z_READ_SIT_ALERT = new OrderEnum("读取久坐提醒", 0x04);
        public static readonly OrderEnum Z_READ_STEP_TARGET = new OrderEnum("读取记步目标", 0x06);
        public static readonly OrderEnum Z_READ_UNIT_TYPE = new OrderEnum("读取单位类型", 0x07);
        public static readonly OrderEnum Z_READ_TIME_FORMAT = new OrderEnum("读取时间格式", 0x08);
        public static readonly OrderEnum Z_READ_LAST_SCREEN = new OrderEnum("读取显示上次屏幕", 0x0A);
        public static readonly OrderEnum Z_READ_HEART_RATE_INTERVAL = new OrderEnum("读取心率间隔", 0x0B);
        public static readonly OrderEnum Z_READ_AUTO_LIGHTEN = new OrderEnum("读取翻腕亮屏", 0x0D);
        public static readonly OrderEnum Z_READ_USER_INFO = new OrderEnum("读取个人信息", 0x0E);
        public static readonly OrderEnum Z_READ_PARAMS = new OrderEnum("读取硬件参数", 0x10);
        public static readonly OrderEnum Z_READ_VERSION = new OrderEnum("读取版本", 0x11);
        public static readonly OrderEnum Z_READ_SLEEP_GENERAL = new OrderEnum("读取睡眠概况", 0x12);
        public static readonly OrderEnum Z_READ_SLEEP_DETAIL = new OrderEnum("读取睡眠详情", 0x14);
        public static readonly OrderEnum Z_READ_LAST_CHARGE_TIME = new OrderEnum("读取上次充电时间", 0x18);
        public static readonly OrderEnum Z_READ_BATTERY = new OrderEnum("读取电量", 0x19);
        public static readonly OrderEnum Z_READ_DIAL = new OrderEnum("读取表盘", 0x0F);
        public static readonly OrderEnum Z_READ_SHAKE_STRENGTH = new OrderEnum("读取震动强度", 0x1E);
        public static readonly OrderEnum Z_READ_DATE_FORMAT = new OrderEnum("读取日期制式", 0x1D);
        public static readonly OrderEnum Z_READ_CUSTOM_SORT_SCREEN = new OrderEnum("读取自定义屏幕排序", 0x1F);
        public static readonly OrderEnum Z_READ_NODISTURB = new OrderEnum("读取勿扰模式", 0x0C);
        public static readonly OrderEnum Z_READ_SPORTS = new OrderEnum("读取运动数据", 0x16);
        public static readonly OrderEnum Z_WRITE_FIND_PHONE = new OrderEnum("打开寻找手机", 0x16);
        public static readonly OrderEnum Z_WRITE_LANGUAGE = new OrderEnum("设置手环语言", 0x1B);
        public static readonly OrderEnum Z_WRITE_ALARMS = new OrderEnum("设置闹钟", 0x01);
        public static readonly OrderEnum Z_WRITE_SIT_ALERT = new OrderEnum("设置久坐提醒", 0x04);
        public static readonly OrderEnum Z_WRITE_STEP_TARGET = new OrderEnum("设置记步目标", 0x06);
        public static readonly OrderEnum Z_WRITE_UNIT_TYPE = new OrderEnum("设置单位类型", 0x07);
        public static readonly OrderEnum Z_WRITE_TIME_FORMAT = new OrderEnum("设置时间格式", 0x08);
        public static readonly OrderEnum Z_WRITE_LAST_SCREEN = new OrderEnum("设置显示上次屏幕", 0x0A);
        public static readonly OrderEnum Z_WRITE_HEART_RATE_INTERVAL = new OrderEnum("设置心率间隔", 0x0B);
        public static readonly OrderEnum Z_WRITE_AUTO_LIGHTEN = new OrderEnum("设置翻腕亮屏", 0x0D);
        public static readonly OrderEnum Z_WRITE_USER_INFO = new OrderEnum("设置个人信息", 0x0E);
        public static readonly OrderEnum Z_WRITE_SYSTEM_TIME = new OrderEnum("设置系统时间", 0x0F);
        public static readonly OrderEnum Z_WRITE_SHAKE = new OrderEnum("设置手环震动", 0x13);
        public static readonly OrderEnum Z_WRITE_NOTIFY = new OrderEnum("设置通知", 0x14);
        public static readonly OrderEnum Z_WRITE_COMMON_NOTIFY = new OrderEnum("设置通用通知", 0x1F);
        public static readonly OrderEnum Z_WRITE_DIAL = new OrderEnum("设置表盘", 0x10);
        public static readonly OrderEnum Z_WRITE_SHAKE_STRENGTH = new OrderEnum("设置震动强度", 0x1D);
        public static readonly OrderEnum Z_WRITE_DATE_FORMAT = new OrderEnum("设置日期制式", 0x1C);
        public static readonly OrderEnum Z_WRITE_CUSTOM_SORT_SCREEN = new OrderEnum("设置自定义屏幕排序", 0x1E);
        public static readonly OrderEnum Z_WRITE_NODISTURB = new OrderEnum("设置勿扰模式", 0x0C);
        public static readonly OrderEnum Z_WRITE_STEP_INTERVAL = new OrderEnum("设置记步间隔", 0x21);
        public static readonly OrderEnum Z_WRITE_SCREEN_BG = new OrderEnum("设置屏幕背景", 0x22);
        public static readonly OrderEnum Z_READ_STEPS = new OrderEnum("读取记步", 0x01);
        public static readonly OrderEnum Z_STEPS_CHANGES_LISTENER = new OrderEnum("监听记步", 0x03);
        public static readonly OrderEnum Z_READ_STEP_INTERVAL = new OrderEnum("读取记步间隔", 0x05);
        public static readonly OrderEnum Z_READ_HEART_RATE = new OrderEnum("读取心率", 0x01);
        public static readonly OrderEnum Z_READ_SPORTS_HEART_RATE = new OrderEnum("读取运动心率", 0x04);
        public static readonly OrderEnum RESET_DATA = new OrderEnum("恢复出厂设置", 0x0A);
        public static readonly OrderEnum Z_WRITE_CLOSE = new OrderEnum("关机", 0x12);
        public static readonly OrderEnum Z_WRITE_RESET = new OrderEnum("恢复出厂设置", 0x20);
    }
}
