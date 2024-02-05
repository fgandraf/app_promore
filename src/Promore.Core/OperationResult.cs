namespace Promore.Core
{
    public class OperationResult
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public OperationResult(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
        }

        public static OperationResult SuccessResult(string message = "Ok") => new OperationResult ( success : true, message: message );
        public static OperationResult FailureResult(string message) => new OperationResult ( success : false, message : message );
    }


    public class OperationResult<T> : OperationResult
    {
        public T Value { get; private set; }

        protected OperationResult(bool success, T value = default, string message = null) : base(success, message)
            => Value = value;

        public static OperationResult<T> SuccessResult(T model) => new OperationResult<T>( success: true, value: model, message: "OK" );
        public new static OperationResult<T> FailureResult(string message) => new OperationResult<T>( success: false, value: default, message: message);
    }
    
}
