using Helper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public bool IsAuthenticated { get; set; }

        public string UiLanguage { get; set; }

        public IConfiguration Configuration { get; set; }

        public UserViewModel()
        {
            IsAuthenticated = false;
            Configuration = new Configuration();
        }
    }
}