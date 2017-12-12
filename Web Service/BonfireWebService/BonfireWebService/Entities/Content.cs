using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonfireWebService.Entities
{
    public class Content
    {
        public string id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}