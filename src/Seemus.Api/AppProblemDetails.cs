using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using Seemus.Domain.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Seemus.Api
{
	public class AppProblemDetails
	{
		public string Message { get; set; } = "Um ou mais erros de validação ocorreram.";

		public IDictionary<string, string[]> Erros { get; set; }

		public AppProblemDetails()
		{

		}
		public AppProblemDetails(ModelStateDictionary modelState)
		{
			Erros = GetErrors(modelState);
		}

		public AppProblemDetails(IEnumerable<IdentityError> errors)
		{
			Erros = GetErrors(errors);
		}


		public AppProblemDetails(string message)
		{
			Message = message;
		}

		public AppProblemDetails(string message, ModelStateDictionary modelState)
		{
			Erros = GetErrors(modelState);
			Message = message;
		}

		public AppProblemDetails(string message, IEnumerable<IdentityError> errors)
		{
			Erros = GetErrors(errors);
			Message = message;

		}

		private IDictionary<string, string[]> GetErrors(ModelStateDictionary modelState)
		{
			return modelState
				.ToDictionary(
					kvp => kvp.Key.Split(".").ToCamelCase(),
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
		}

		private IDictionary<string, string[]> GetErrors(IEnumerable<IdentityError> errors)
		{
			return errors
				.ToDictionary(
					kvp => kvp.Code.Split(".").ToCamelCase(),
					kvp => new[] { kvp.Description });
		}

	}
}
