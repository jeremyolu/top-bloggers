using System.ComponentModel.DataAnnotations;

namespace TopBloggers.Models
{
    [MetadataType(typeof(AuthorPartial))]
    public partial class Author
    {
        public string AuthorUrl { get; set; }
    }

    public class AuthorPartial
    {
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "surname is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
        public string ProfileImage { get; set; }
    }
}