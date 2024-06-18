namespace CEP.Consult.Controllers.Response
{
    public class ApiResponse<T>
    {
        public Status Status { get; set; }
        public T Data { get; set; }
        public ApiResponse() 
        {
            Status = Status.NotFound;
        }
    }

    public enum Status
    {
        Success,
        NotFound,
        Error
    }
}
