using GLvNext.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace GLvNext.Data
{
    public class ExternalOfferData : IExternalOfferData
    {
        private readonly IConfiguration configuration;
        private ExternalOfferApiConnection connection;
        static HttpClient client = new HttpClient();

        public ExternalOfferData(IConfiguration configuration)
        {
            this.configuration = configuration;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            connection = new ExternalOfferApiConnection();
            connection.Url = configuration["ExternalOffersApi:Url"];
            connection.GlApiKey = configuration["ExternalOffersApi:Gl-ApiKey"];
            connection.GlAppKey = configuration["ExternalOffersApi:Gl-AppKey"];

            
            client.DefaultRequestHeaders.Add("Gl-ApiKey", connection.GlApiKey);
            client.DefaultRequestHeaders.Add("Gl-AppKey", connection.GlAppKey);
        }
        public async Task<IEnumerable<Offer>> GetAllOffers(string title)
        {
            List<Offer> offers = null;
            HttpResponseMessage response = await client.GetAsync(connection.Url);
            if(response.IsSuccessStatusCode)
            {
                var extOfferStr = await response.Content.ReadAsStringAsync();
                var extOffers = JsonConvert.DeserializeObject<ExternalOfferObj>(extOfferStr);

                if(extOffers.Data != null)
                {
                    offers = new List<Offer>();
                    foreach(var data in extOffers.Data)
                    {
                        offers.Add(new Offer() { Id = Guid.Parse(data.Key), Title = data.Name, Source = SourceType.Api, Description = data.Summary?.Text});
                    }
                }

            }
            return offers;
        }
     

         
    }
}
