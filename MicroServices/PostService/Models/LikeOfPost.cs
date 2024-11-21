using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PostService.Models
{
    public class LikeOfPost
    {
        [Key]
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("post_id"), ForeignKey("Post")]
        public required int PostId { get; set; }

        [Column("user_id")]
        public required int UserId { get; set; }

        [InverseProperty("Likes")]
        public Post? Post { get; set; } = null!;
    }

}
