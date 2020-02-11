using GLvNext.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLvNext.Data
{
    public interface IExternalOfferData
    {
        Task<IEnumerable<Offer>> GetAllOffers(string title);
    }
}
