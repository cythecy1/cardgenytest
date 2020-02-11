using System;
using System.ComponentModel.DataAnnotations;

namespace GLvNext.Core
{

    public class Offer 
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public SourceType Source { get; set; }
    }
}
