using System;
using System.Text.RegularExpressions;

namespace Seemus.Domain.Core
{
	public class Validations
	{
		public static void Equals(object object1, object object2, string message)
		{
			if (object1.Equals(object2))
				throw new DomainException(message);

		}

		public static void IsNotEquals(object object1, object object2, string message)
		{
			if (!object1.Equals(object2))
				throw new DomainException(message);
		}

		public static void IsNotEquals(string pattern, string value, string message)
		{
			var regex = new Regex(pattern);

			if (!regex.IsMatch(value))
				throw new DomainException(message);
		}

		public static void ValidateLength(string value, int max, string propertyName, string message = null)
		{
			var length = value.Length;
			if (length > max)
			{
				if (message == null)
					message = $"The property {propertyName} cannot be larget than {max}.";

				throw new DomainException(message);
			}
		}

		public static void ValidateLength(string value, int min, int max, string message)
		{
			var length = value.Length;
			if (length < min || length > max)
				throw new DomainException(message);
		}

		public static void IsEmpty(string value, string message)
		{
			if (value == null || value.Trim().Length == 0)
				throw new DomainException(message);
		}

		public static void IsNull(object object1, string propertyName, string message = null)
		{
			if (object1 == null)
			{
				if (message == null)
					message = $"The property {propertyName} cannot be null";

				throw new DomainException(message);
			}
		}

		public static void IsNullOrEmpty(string value, string propertyName, string message = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				if (message == null)
					message = $"The property {propertyName} cannot be null or empty";

				throw new DomainException(message);
			}
		}

		public static void ValidadeMinMax(double value, double min, double max, string message)
		{
			if (value < min || value > max)
				throw new DomainException(message);

		}

		public static void ValidateMinMax(float value, float min, float max, string message)
		{
			if (value < min || value > max)
				throw new DomainException(message);
		}

		public static void Validarminmax(int value, int min, int max, string message)
		{
			if (value < min || value > max)
				throw new DomainException(message);
		}

		public static void ValidadeMinMax(long value, long min, long max, string message)
		{
			if (value < min || value > max)
				throw new DomainException(message);
		}

		public static void ValidadeMinMax(decimal value, decimal min, decimal max, string message)
		{
			if (value < min || value > max)
				throw new DomainException(message);
		}

		public static void IsLowerThan(long value, long min, string message)
		{
			if (value < min)
				throw new DomainException(message);
		}

		public static void IsLowerThan(double value, double min, string message)
		{
			if (value < min)
				throw new DomainException(message);
		}

		public static void IsLowerThan(decimal value, decimal min, string message)
		{
			if (value < min)
				throw new DomainException(message);
		}

		public static void IsLowerThan(DateTime value, DateTime min, string message)
		{
			if (value < min)
				throw new DomainException(message);
		}

		public static void IsLowerThan(int value, int min, string message)
		{
			if (value < min)
				throw new DomainException(message);
		}

		public static void IsFalse(bool boolValue, string message)
		{
			if (!boolValue)
				throw new DomainException(message);
		}

		public static void IsTrue(bool boolValue, string message)
		{
			if (boolValue)
				throw new DomainException(message);
		}
	}
}
