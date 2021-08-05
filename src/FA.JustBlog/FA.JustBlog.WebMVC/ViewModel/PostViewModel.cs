using FA.JustBlog.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.WebMVC.ViewModel
{
    public class PostViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [MaxLength(1000, ErrorMessage = "The {0} must less than {1} characters")]
        public string ShortDescription { get; set; }

        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 4)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string PostContent { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string UrlSlug { get; set; }

        public bool Published { get; set; }
        public int ViewCount { get; set; }

        public DateTime PublishedDate { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}