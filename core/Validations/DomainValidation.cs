namespace Core.Validations
{
    public static class DomainValidator
    {
        public static void Required(string value, string fieldName, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(value))
                errors.Add($"{fieldName} is required");
        }

        public static void GreaterThan(
            decimal value,
            decimal threshold,
            string fieldName,
            List<string> errors
        )
        {
            if (value <= threshold)
                errors.Add($"{fieldName} must be greater than {threshold}");
        }

        public static void MinLength(
            string value,
            int minLength,
            string fieldName,
            List<string> errors
        )
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length < minLength)
                errors.Add($"{fieldName} must be at least {minLength} characters");
        }

        // Add more rules as needed
    }
}
