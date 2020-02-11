using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GLvNext.Core;
using GLvNext.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GLvNext.Web
{
    public class DetailModel : PageModel
    {
        private readonly IOfferData offerData;
        [TempData]
        public string Message { get; set; }
        public DetailModel(IOfferData offerData)
        {
            this.offerData = offerData;
        }
        public Offer Offer { get; set; }
        public async Task<IActionResult> OnGet(Guid offerId)
        {
            Offer = await offerData.GetByIdAsync(offerId);
            if(Offer == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}