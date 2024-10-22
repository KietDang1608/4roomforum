using System.ComponentModel.DataAnnotations;

namespace PostService.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public required int ThreadId { get; set; }
        public required int PostedBy { get; set; }
        public int Like { get; set; }
        public required string PostContent { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsEdited { get; set; }
        public Post() { }

    }
}
