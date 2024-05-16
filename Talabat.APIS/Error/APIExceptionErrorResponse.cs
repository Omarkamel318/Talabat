namespace Talabat.APIS.Error
{
	public class APIExceptionErrorResponse :APIResponse
	{
        public string? Details { get; set; }
        public APIExceptionErrorResponse(int statusCode,string? details = null , string? messege = null) :base(statusCode, messege)
        {
            Details= details;
        }
    }
}
