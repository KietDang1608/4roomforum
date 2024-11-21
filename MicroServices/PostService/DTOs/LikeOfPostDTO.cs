using System.Diagnostics.CodeAnalysis;

namespace PostService.DTOs
{
    public class LikeOfPostDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }

    public class CreateLikeOfPostDTO
    {
        [SetsRequiredMembers]
        public CreateLikeOfPostDTO(int postId, int userId)
        {
            PostId = postId;
            UserId = userId;
        }
        public required int PostId { get; set; }
        public required int UserId { get; set; }
    }

    public class UpdateLikeOfPostDTO { 
        
    }
}
