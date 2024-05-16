namespace Talabat.APIS.Error
{
	public class APiValidationErrorResponse :APIResponse
	{
        public IEnumerable<string> Errors { get; set; } 
        public APiValidationErrorResponse(IEnumerable<string> errors) :base(400)
        {
			Errors=errors;

		}
    }
}
