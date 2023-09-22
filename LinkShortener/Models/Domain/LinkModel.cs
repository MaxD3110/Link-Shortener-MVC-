using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Models.Domain
{
    public class LinkModel
    {
        public int Id { get; set; }
        [Display(Name = "Full URL"), Required]
        public string? RawUrl { get; set; }
        [Display(Name = "Short URL")]
        public string? ShortUrl { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;
        [Display(Name = "Number of views")]
        public int Viewed { get; set; } = 0;
    }
}
