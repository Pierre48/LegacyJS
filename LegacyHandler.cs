﻿using System;
using System.Collections.Generic;
using System.Web;

namespace LegacyJS
{
    /// <summary>
    /// The handler that will allow to change http response
    /// </summary>
    public class LegacyHandler : IHttpModule
    {
        /// <summary>
        /// The _rules
        /// </summary>
        private static List<Rule> _rules = new List<Rule>
        {
            new Rule
            {
                FilePathEndWith = new List<string> {"WebResource.axd" },
                Changes = new List<Change>
                {
                    new Change
                    {
                        OldString = "if(ig_shared.IsIE&&ig_shared.IsWin)",
                        NewString = "if(false)",
                    }
                }
            },
            new Rule
            {
                FilePathEndWith = new List<string> {"WebResource.axd" },
                Changes = new List<Change>
                {
                    new Change
                    {
                        OldString = "if (typeof (obj[item]) != \"undefined\" && obj[item] != null && !obj[item].tagName && !obj[item].disposing && typeof (obj[item]) != \"string\")",
                        NewString = "if (typeof (obj[item]) != \"undefined\" && obj[item] != null && !obj[item].tagName && !obj[item].disposing && typeof (obj[item]) != \"string\" && obj.hasOwnProperty(item))",
                    }
                }
            },
            new Rule
            {
                FilePathEndWith = new List<string> {"ig_webgrid.js" },
                Changes = new List<Change>
                {
                    new Change
                    {
                        OldString = "if (typeof (obj[item]) != \"undefined\" && obj[item] != null && !obj[item].tagName && !obj[item].disposing && typeof (obj[item]) != \"string\")",
                        NewString = "if (typeof (obj[item]) != \"undefined\" && obj[item] != null && !obj[item].tagName && !obj[item].disposing && typeof (obj[item]) != \"string\" && obj.hasOwnProperty(item))",
                    }
                }
            }
        };

        private HttpApplication _context;

        private HttpRequest Request
        {
            get { return _context?.Request; }
        }

        private HttpResponse Response
        {
            get { return _context?.Response; }
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            _context = context;

            _context.PreRequestHandlerExecute += _context_PreRequestHandlerExecute;
        }

        private void _context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var application = (HttpApplication) sender;

            foreach (var rule in _rules)
            {
                if (rule.Check(Request, Response))
                {
                    application.Response.Filter = new FilterStream(Response.Filter, Response.ContentEncoding, rule.Changes);
                }
            }
            
        }
        
    }
}
