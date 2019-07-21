using System.Collections.Generic;

namespace AutomationMQTTServer
{
    /// <summary>
    /// The <see cref="User"/> read from the config.json file.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the allowed topics.
        /// </summary>
        public IEnumerable<string> AllowedTopics { get; } = new List<string>();
    }
}