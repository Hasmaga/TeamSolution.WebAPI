namespace TeamSolution.Helper
{
    public static class CoreHelper
    {

        public static DateTime SystemTimeNow => DateTime.UtcNow;
        public static DateTime DefaultTime => new DateTime();
        public static string DefaultEmptyString => "";
        public static Guid DefaultGuid => Guid.Empty;
    }
}
