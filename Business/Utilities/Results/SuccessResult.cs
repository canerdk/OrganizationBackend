namespace Business.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(object message) : base(true, message)
        {
        }

        public SuccessResult():base(true)
        {

        }
    }
}
