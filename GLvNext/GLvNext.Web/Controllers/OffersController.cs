using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GLvNext.Core;
using GLvNext.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GLvNext.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOfferData offerData;
        private readonly IExternalOfferData extOfferData;

        public OffersController(IOfferData offerData, IExternalOfferData extOfferData)
        {
            this.offerData = offerData;
            this.extOfferData = extOfferData;
        }
        // GET: api/Offers
        [HttpGet]
        public async Task<IEnumerable<Offer>> Get()
        {
            var portalOffers = offerData.GetOffersByTitle(String.Empty);
            var ret = await extOfferData.GetAllOffers(String.Empty);

            return portalOffers.Concat(ret);
        }

    }
}
