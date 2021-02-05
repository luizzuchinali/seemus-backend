using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seemus.Domain.Extensions
{
	public static class StringExtensions
	{
		public static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
			{
				return s;
			}

			var chars = s.ToCharArray();

			for (var i = 0; i < chars.Length; i++)
			{
				if (i == 1 && !char.IsUpper(chars[i]))
				{
					break;
				}

				var hasNext = (i + 1 < chars.Length);
				if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
				{
					break;
				}

				chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
			}

			return new string(chars);
		}

		public static string ToCamelCase(this string[] stringArray)
		{
			var sb = new StringBuilder();
			var count = stringArray.Count();
			for (int i = 0; i < count; i++)
				sb.Append($"{stringArray[i].ToCamelCase()}{(i != count - 1 ? "." : "")}");

			return sb.ToString();
		}

	}
}
