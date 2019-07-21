using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomationMQTTServer
{
    /// <summary>
    /// A class to test the topics accoridng to the regex provided.
    /// </summary>
    public static class TopicChecker
    {  /// <summary>
       /// Does a regex check on the topics.
       /// </summary>
       /// <param name="allowedTopic">The allowed topic.</param>
       /// <param name="topic">The topic.</param>
       /// <returns><c>true</c> if the topic is valid, <c>false</c> if not.</returns>
        public static bool Test(string allowedTopic, string topic)
        {
            if (string.IsNullOrWhiteSpace(allowedTopic)) throw new ArgumentNullException(nameof(allowedTopic));
            if (string.IsNullOrWhiteSpace(topic)) throw new ArgumentNullException(nameof(topic));
            var realTopicRegex = allowedTopic
                .Replace(@"/", @"\/", true, CultureInfo.InvariantCulture)
                .Replace("+", "§", true, CultureInfo.InvariantCulture)
                .Replace("#", @"[a-zA-Z0-9 \/_#+.-]*", true, CultureInfo.InvariantCulture)
                .Replace("§", @"[a-zA-Z0-9 _.-]*", true, CultureInfo.InvariantCulture);
            var regex = new Regex(realTopicRegex);
            var matches = regex.Matches(topic);

            foreach (var match in matches.ToList())
            {
                if (match.Value == topic)
                {
                    return true;
                }
            }

            return false;
        }
    }
}