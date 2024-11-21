namespace Business.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(object message):base(false, message)
        {

        }

        public ErrorResult():base(false)
        {

        }
    }
}
