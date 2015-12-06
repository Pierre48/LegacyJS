using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Web;

namespace LegacyJS
{
    /// <summary>
    /// A rule allows to specify which kind of request has to be modified, and which changes has to be applied
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// The file path end with
        /// </summary>
        public List<string> FilePathEndWith ;

        /// <summary>
        /// The file path end with
        /// </summary>
        public List<string> UserAgents;

        /// <summary>
        /// The changes/
        /// </summary>
        public List<Change> Changes ;

        /// <summary>
        /// Checks the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public bool Check(HttpRequest request, HttpResponse response)
        {
            var result = true;
            if (FilePathEndWith != null)
            {
                if (FilePathEndWith!=null)
                foreach (var end in FilePathEndWith)
                {
                    result &= end != null && request.FilePath.EndsWith(end, StringComparison.InvariantCultureIgnoreCase);
                }

                if (UserAgents != null)
                    foreach (var userAgent in UserAgents)
                {
                    result &= userAgent != null && userAgent == request.UserAgent;
                }
            }
            return result;
        }
    }
}