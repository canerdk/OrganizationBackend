namespace Business.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, object message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }


        public bool Success { get; }

        public object Message { get; }
    }
}
