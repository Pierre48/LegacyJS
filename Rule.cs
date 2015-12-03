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
            if (FilePathEndWith != null)
            {
                foreach (var end in FilePathEndWith)
                {
                    if (request.FilePath.EndsWith(end, StringComparison.InvariantCultureIgnoreCase))
                    return true;
                }
            }
            return false;
        }
    }
}