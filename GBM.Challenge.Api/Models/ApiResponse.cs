namespace GBM.Challenge.Api.Models
{
    public class ApiResponse<T>
    {
        public int Code { get; set; } = 0;

        public string Message { get; set; }

        public T Result { get; set; }

        public ResponseStatus Status
        {
            get
            {
                if (Code == 0)
                {
                    return ResponseStatus.Success;
                }
                else if (Code > 0)
                {
                    return ResponseStatus.Warning;
                }
                else if (Code <= -200)
                {
                    return ResponseStatus.UnknownError;
                }
                else if (Code <= -100)
                {
                    return ResponseStatus.LogicError;
                }
                else if (Code < 0)
                {
                    return ResponseStatus.PlatformError;
                }
                else
                {
                    return ResponseStatus.Unknown;
                }
            }
        }
    }

    public enum ResponseStatus
    {
        Unknown,
        Success,
        PlatformError,
        LogicError,
        UnknownError,
        Warning
    }
}