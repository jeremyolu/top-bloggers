using System;
using System.ComponentModel.DataAnnotations;

namespace TopBloggers.Models
{
    [MetadataType(typeof(ArticlePartial))]
    public partial class Article
    {
        public string Url { get; set; }
    }

    public class ArticlePartial
    {
        public int BlogArticleID { get; set; }

        [Required(ErrorMessage = "title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "text is required")]
        public string Text { get; set; }

        public string Image { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
    }
}