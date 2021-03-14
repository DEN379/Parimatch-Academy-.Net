namespace DepsWebApp.Options
{
    /// <summary>
    /// Rates options.
    /// </summary>
    public class RatesOptions
    {
        /// <summary>
        /// Base currency.
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Value represent is a valid field.
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
}
