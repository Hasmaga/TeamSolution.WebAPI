namespace TeamSolution.Enum
{
    public class ApplicationConstants
    {
        //public const string ID_EXISTED = "KeyId {0} đã tồn tại.";
        public const string DUPLICATE = "Symtem_id is duplicated";
    }

    public class ResponseCodeConstants
    {
        public const string NOT_FOUND = "NOT_FOUND";
        public const string SUCCESS = "SUCCESS";
        public const string FAILED = "FAILED";
        public const string EXISTED = "EXISTED";
        public const string DUPLICATE = "DUPLICATE";
        public const string EMPTY = "EMPTY";
        public const string IS_DELETED = "IS_DELETED";
    }
    public class ResponseCodeConstantsStore
    {
        public const string STORE = "STORE";
        public const string CREATE_STORE_SUCCESSFULLY = "CREATE_STORE_SUCCESSFULLY";
        public const string UPDATE_STORE_SUCCESSFULLY = "UPDATE_STORE_SUCCESSFULLY";
        public const string DELETE_STORE_SUCCESSFULLY = "DELETE_STORE_SUCCESSFULLY";

        public const string CREATE_STORE_FAILED = "CREATE_STORE_FAILED";
        public const string UPDATE_STORE_FAILED = "UPDATE_STORE_FAILED";
        public const string DELETE_STORE_FAILED = "DELETE_STORE_FAILED";
    }
}
