namespace Infra.Helpers
{
    public static class ObjectExtensions
    {
        public static string ConvertToCurrencyString(this int value)
        {
            if (value < 0) value = 0;

            return value switch
            {
                >= 1_000_000_000 => $"{value / 1_000_000_000.0:F3}B$",
                >= 1_000_000 => $"{value / 1_000_000.0:F3}M$",
                _ => $"{value}$",
            };
        }
    }
}