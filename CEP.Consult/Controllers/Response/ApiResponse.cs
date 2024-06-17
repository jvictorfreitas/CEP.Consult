namespace CEP.Consult.Controllers.Response
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public ApiResponse() 
        {
            Status = "NotFound";
        }
    }
}
