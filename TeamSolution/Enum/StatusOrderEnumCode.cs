namespace TeamSolution.Enum
{
    public class StatusOrderEnumCode
    {        
        public const string WAITING_STORE_ACCEPT = "WAITING_STORE_ACCEPT";
        public const string STORE_ACCEPT = "STORE_ACCEPT";
        public const string READY_TAKE_ORDER = "READY_TAKE_ORDER";
        public const string STORE_DECLINCE = "STORE_DECLINCE";
        public const string WAITING_SHIPPER_ACCEPT = "WAITING_SHIPPER_ACCEPT";
        public const string READY_DELIVERY_ORDER = "READY_DELIVERY_ORDER";
        public const string WASH_DONE = "WASH_DONE";
        public const string ORDER_IN_PROGRESS = "ORDER_IN_PROGRESS";
        public const string SHIPPER_ON_THE_WAY = "SHIPPER_ON_THE_WAY";
        public const string SHIPPER_ARRIVED_CUSTOMER = "SHIPPER_ARRIVED_CUSTOMER";
        public const string SHIPPER_TAKE_ORDER = "SHIPPER_TAKE_ORDER";
        public const string DELIVER_TO_STORE = "DELIVER_TO_STORE";
        public const string SHIPPER_ARRIVED_STORE = "SHIPPER_ARRIVED_STORE";
        public const string READY_TO_WASH_ORDER = "READY_TO_WASH_ORDER";
        public const string ORDER_DONE = "ORDER_DONE";
        public const string ORDER_REJECT = "ORDER_REJECT";
    }
}
