using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HolidayCalendar.Tools
{
    public class Sha256StringHasher : StringHasher
    {
        public override string Hash(string input)
        {
            ValidateParameter(input);

            return Encode(input);
        }

        private void ValidateParameter(string parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "Parameter string cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException("Parameter string cannot be empty or whitespace.");
            }
        }

        public bool Compare(string plain, string hashed)
        {
            ValidateParameter(plain);
            ValidateParameter(hashed);

            return Hash(plain).Equals(hashed);
        }

        private string Encode(string input)
        {
            using (var encoder = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var encodedBytes = encoder.ComputeHash(bytes);

                return encodedBytes.Aggregate(string.Empty, (output, b) => output + string.Format((string) "{0:X}", (object) b)).ToLower();
            }
        }
    }
}