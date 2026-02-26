namespace Core.Commons
{
    public static class Commons
    {
        public static DateTime? ConvertToDateTime(this string date, string format = null)
        {
            if (string.IsNullOrEmpty(format))
                format = DateTimeFormat.DDMMYYYY.Value;
            if (string.IsNullOrEmpty(date))
                return null;
            try
            {
                return DateTime.ParseExact(
                    date,
                    format,
                    System.Globalization.CultureInfo.InvariantCulture
                );
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }

    public struct DateTimeFormat
    {
        private DateTimeFormat(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static readonly DateTimeFormat DDMMYYYY = new DateTimeFormat("dd/MM/yyyy");
        public static readonly DateTimeFormat YYYYMMDD = new DateTimeFormat("yyyy/MM/dd");

        public override string ToString()
        {
            return Value;
        }

        public static IEnumerable<DateTimeFormat> GetAllFormats()
        {
            return typeof(DateTimeFormat)
                .GetFields(
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                )
                .Where(f => f.FieldType == typeof(DateTimeFormat))
                .Select(f => (DateTimeFormat)f.GetValue(null));
        }
    }
}
