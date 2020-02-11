using GLvNext.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GLvNext.Data
{
    public class SqlOfferData : IOfferData, IOfferDataAsync
    {
        private readonly GLDbContext db;

        public SqlOfferData(GLDbContext db)
        {
            this.db = db;
        }

        public Offer Add(Offer newOffer)
        {
            db.Add(newOffer);
            return newOffer;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }

        public Offer Delete(Guid offerId)
        {
            var offer = GetByIdAsync(offerId).Result;
            if(offer != null)
            {
                db.Offers.Remove(offer);
            }
            return offer;

        }

        public async Task<Offer> GetByIdAsync(Guid id)
        {
            return await db.Offers.FindAsync(id);
        }

        public IEnumerable<Offer> GetOffersByTitle(string title)
        {
            var query = from o in db.Offers
                        where o.Title.StartsWith(title) || string.IsNullOrEmpty(title)
                        orderby o.Title
                        select o;

            return query;
        }

        public Offer Update(Offer updatedOffer)
        {
            var entity = db.Offers.Attach(updatedOffer);
            entity.State = EntityState.Modified;
            return updatedOffer;
        }
    }
}
