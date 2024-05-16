
namespace Talabat.APIS.Error
{
	public class APIResponse
	{
        public string? Messege { get; set; }
        public int StatusCode { get; set; }

        public APIResponse(int statusCode , string? messege = null)
        {
            StatusCode = statusCode;
            Messege = messege ?? GetMessegeByStatusCode(statusCode);
        }

		private string? GetMessegeByStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "Bad request" ,
				401=>"Authorized , you are not",
				404=>"Not found" ,
				500=>"Server Error",
				_=> null

			};
		}
	}

}
