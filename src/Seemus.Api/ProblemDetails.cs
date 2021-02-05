using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using Seemus.Domain.Extensions;

namespace Seemus.Api
{
	public class ProblemDetails
	{
		public string Message { get; set; } = "Um ou mais erros de validação ocorreram.";

		public IDictionary<string, string[]> Erros { get; set; }

		public ProblemDetails(ModelStateDictionary modelState)
		{
			Erros = GetErrors(modelState);
		}

		public ProblemDetails(string message)
		{
			Message = message;
		}

		public ProblemDetails(string message, ModelStateDictionary modelState)
		{
			Erros = GetErrors(modelState);
			Message = message;
		}

		private IDictionary<string, string[]> GetErrors(ModelStateDictionary modelState)
		{
			return modelState
				.ToDictionary(
					kvp => kvp.Key.Split(".").ToCamelCase(),
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
		}

	}
}
