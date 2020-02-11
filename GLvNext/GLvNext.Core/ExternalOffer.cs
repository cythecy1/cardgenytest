using System;
using System.Collections.Generic;
using System.Text;

namespace GLvNext.Core
{
    public class ExternalOfferObj
    {
        public IEnumerable<ExternalOffer> Data { get; set; }
    }
    public class ExternalOffer
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public ExternalOfferSummary Summary { get; set; }
    }

    public class ExternalOfferSummary
    {
        public string Text { get; set; }
    }
}
