using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Models
{
    public class Reply
    {
        [Key]
        [Column("reply_id")]  // Tên cột là "reply_id"
        public int ReplyId { get; set; }

        [Column("post_id")]  // Tên cột là "post_id"
        public int PostId { get; set; }

        [Column("reply_by")]  // Tên cột là "reply_by"
        public int ReplyBy { get; set; }

        [Column("reply_content")]  // Tên cột là "reply_content"
        public string ReplyContent { get; set; }

        [Column("reply_date")]  // Tên cột là "reply_date"
        public DateTime ReplyDate { get; set; } = DateTime.Now;

        [Column("upvote_amount")]  // Tên cột là "upvote_amount"
        public int UpvoteAmount { get; set; } = 0;

        [Column("downvote_amount")]  // Tên cột là "downvote_amount"
        public int DownvoteAmount { get; set; } = 0;

        [Column("reply_to_reply")]  // Tên cột là "reply_to_reply"
        public int? ReplyToReply { get; set; }
    }
}
