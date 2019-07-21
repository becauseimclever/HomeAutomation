using System.Collections.Generic;

namespace AutomationMQTTServer
{
    public class Config
    {
        /// <summary>
        ///     Gets or sets the port.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        ///     Gets or sets the list of valid users.
        /// </summary>
        public IEnumerable<User> Users { get; } = new List<User>();

    }
}
