using GLvNext.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace GLvNext.Data
{
    /// <summary>
    /// For testing purposes.
    /// </summary>
    public class InMemoryOfferData : IOfferData
    {
        readonly ConcurrentBag<Offer> offers; //ConcurrentBag for thread safety
        public InMemoryOfferData()
        {
            offers = new ConcurrentBag<Offer>()
            {
                new Offer { Id = Guid.NewGuid(), Title="Buy 1 get 1 half price", Description="Lipsum Lipsum Lipsum Lipsum More Lipsum", Source = SourceType.Portal },
                new Offer { Id = Guid.NewGuid(), Title="30% Off Shoes", Description="More Lipsum Lipsum Lipsum Lipsum More Lipsum", Source = SourceType.Portal },
                new Offer { Id = Guid.NewGuid(), Title="10 Flybuy Points for every $300 spend", Description="Lipsum More More Lipsum Lipsum Lipsum More Lipsum", Source = SourceType.Portal },
            };
        }

        public async Task<Offer> GetByIdAsync(Guid id)
        {
            return await Task.Run(() => offers.SingleOrDefault(o => o.Id == id));

        }

        public IEnumerable<Offer> GetOffersByTitle(string title)
        {
            return from o in offers
                   where string.IsNullOrEmpty(title) || o.Title.StartsWith(title)
                   orderby o.Title
                   select o;
        }

        public Offer Update(Offer updatedOffer)
        {
            var offer = offers.SingleOrDefault(b => b.Id == updatedOffer.Id);
            if(offer != null)
            {
                offer.Title = updatedOffer.Title;
                offer.Source = updatedOffer.Source;
            }
            return offer;
        }

        public int Commit()
        {
            return 0;
        }

        public Offer Add(Offer newOffer)
        {
            newOffer.Id = Guid.NewGuid();
            offers.Add(newOffer);
            return newOffer;
        }

        public Offer Delete(Guid offerId)
        {
            var offer = offers.FirstOrDefault(o => o.Id == offerId);
            if(offer != null)
            {

                offers.TryTake(out offer);
            }
            return offer;
        }
    }
}
