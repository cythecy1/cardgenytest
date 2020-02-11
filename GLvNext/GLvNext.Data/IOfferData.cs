using GLvNext.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLvNext.Data
{
    public interface IOfferData
    {
        IEnumerable<Offer> GetOffersByTitle(string title);

        //TODO: Move to IOfferDataAsync
        Task<Offer> GetByIdAsync(Guid id); 

        Offer Update(Offer updatedOffer);

        Offer Add(Offer newOffer);

        Offer Delete(Guid offerId);

        int Commit();
    }
    /// <summary>
    /// Interface segregation principle
    /// </summary>
    public interface IOfferDataAsync
    {
        Task<int> CommitAsync();
    }

}
