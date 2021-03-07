using System;

namespace DepsWebApp.Options
{
    /// <summary>
    /// Nbu client options.
    /// </summary>
    public class NbuClientOptions
    {
        /// <summary>
        /// Base adress of nbu client.
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Value represent is a valid field.
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
}
