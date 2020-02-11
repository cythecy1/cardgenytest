using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GLvNext.Core;
using GLvNext.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace GLvNext.Web
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IOfferData offerData;
        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; }
        public string Message { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public ListModel(IConfiguration config, 
            IOfferData offerData)
        {
            this.config = config;
            this.offerData = offerData;
        }
        public void OnGet()
        {
            Message = config["Message"];
            Offers = offerData.GetOffersByTitle(SearchTitle);
        }
    }
}