﻿using System.Web;
using System.Web.Mvc;

namespace K_Burns_SpitTree
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
