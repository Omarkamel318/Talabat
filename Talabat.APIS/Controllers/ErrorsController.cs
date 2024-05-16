using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Error;

namespace Talabat.APIS.Controllers
{
	[Route("Errors/{code}")]
	[ApiController]
	public class ErrorsController : ControllerBase
	{
		[ApiExplorerSettings(IgnoreApi =true)]
		public ActionResult Error(int code)
		{
			if (code == 404)
				return NotFound(new APIResponse(code));
			if (code == 401)
				return Unauthorized(new APIResponse(code));
			else 
				return StatusCode(code);
		}
	}
}
