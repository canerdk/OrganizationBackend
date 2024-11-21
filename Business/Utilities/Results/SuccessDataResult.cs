namespace Business.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, object message) : base(data, true, message)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(object message) : base(default, true, message)
        {
        }

        public SuccessDataResult() : base(default, true)
        {
        }
    }
}
