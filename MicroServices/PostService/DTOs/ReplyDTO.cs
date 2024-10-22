namespace PostService.DTOs
{
    public class ReplyDTO
    {
        public int ReplyId { get; set; }
        public int PostId { get; set; }
        public int ReplyBy { get; set; }
        public string ReplyContent { get; set; }
        public DateTime ReplyDate { get; set; }
        public int UpvoteAmount { get; set; }
        public int DownvoteAmount { get; set; }
        public int ReplyToReply { get; set; }
    }

    public class CreateReplyDTO1
    {
        public int PostId { get; set; }
        public int ReplyBy { get; set; }
        public string ReplyContent { get; set; } 
    }

    public class CreateReplyDTO2
    {
        public int PostId { get; set; }
        public int ReplyBy { get; set; }
        public string ReplyContent { get; set; }       
        public int ReplyToReply { get; set; }
    }

    public class UpdateReplyDTO {
        public string ReplyContent { get; set; }
    }

}
