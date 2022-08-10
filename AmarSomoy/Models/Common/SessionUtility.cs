using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class SessionUtility
    {
        private const string SESSION_KEY_PREFIX = "__AMAR__";
        public static SessionContainer SessionContainer
        {
            set
            {
                HttpContext.Current.Session[SESSION_KEY_PREFIX + "SessionContainer"] = value;
            }
            get
            {
                if (HttpContext.Current.Session[SESSION_KEY_PREFIX + "SessionContainer"] != null)
                {
                    return (SessionContainer)HttpContext.Current.Session[SESSION_KEY_PREFIX + "SessionContainer"];
                }
                else
                {
                    return new SessionContainer();
                }
            }
        }
    }
}