using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Seemus.Api.Controllers
{
	[ApiController]
	public class ApplicationController : ControllerBase
	{
		protected readonly IMapper Mapper;
		protected const int PaginationSize = 10;

		public ApplicationController(IMapper mapper)
		{
			Mapper = mapper;
		}

		[NonAction]
		public override BadRequestObjectResult BadRequest([ActionResultObjectValue] ModelStateDictionary modelState)
		{
			return base.BadRequest(new ProblemDetails(modelState));
		}

		[NonAction]
		public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object error)
		{
			if (error is string)
				return new BadRequestObjectResult(new ProblemDetails((string)error));

			return base.BadRequest(error);
		}

		protected void IncluirHeaderDePaginacao(int total, int current, int count)
		{
			HttpContext.Response.Headers.Add("X-Total-Count", total.ToString());
			HttpContext.Response.Headers.Add("X-Current-Page", current.ToString());
			HttpContext.Response.Headers.Add("X-Total-Pages", count.ToString());
		}

		//protected Guid GetCurrentUserId()
		//{
		//	return Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid));
		//}

		protected void AddMessage(string key, string message)
		{
			ModelState.AddModelError(key, message);
		}
	}
}
