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
    public class EditModel : PageModel
    {
        private readonly IOfferData offerData;
        [BindProperty]
        public Offer Offer { get; set; }

        public EditModel(IOfferData offerData)
        {
            this.offerData = offerData;
        }

        public async Task<IActionResult> OnGet(Guid? offerId)
        {
            if(offerId.HasValue)
            {
                Offer = await offerData.GetByIdAsync(offerId.Value);
            }
            else
            {
                Offer = new Offer();

            }

            if (Offer == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if(Offer.Id == Guid.Empty)
            {
                Offer = offerData.Add(Offer);
            }
            else
            {
                Offer = offerData.Update(Offer);

            }
            offerData.Commit();
            TempData["Message"] = "Offer Saved!";
            return RedirectToPage("./Detail", new { offerId = Offer.Id }); //POST-REDIRECT-GET Pattern


        }

    }
}