using System.Diagnostics.CodeAnalysis;

namespace PostService.DTOs
{
    public class LikeOfReplyDTO
    {
        public int Id { get; set; }
        public int Vote { get; set; }
        public int ReplyId { get; set; }
        public int UserId { get; set; }
    }

    public class CreateLikeOfReplyDTO
    {
        [SetsRequiredMembers]
        public CreateLikeOfReplyDTO(int replyId, int userId)
        {
            ReplyId = replyId;
            UserId = userId;
        }

        public int Vote { get; set; }
        public required int ReplyId { get; set; }
        public required int UserId { get; set; }
    }

    public class UpdateLikeOfReplyDTO
    {
        [SetsRequiredMembers]
        public UpdateLikeOfReplyDTO(int userId, int vote)
        {
            UserId = userId;
            Vote = vote;
        }
        public int Vote { get; set; }
        public required int UserId { get; set; }
    }
}
