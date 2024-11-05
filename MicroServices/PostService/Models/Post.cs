﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Models
{
    public class Post
    {
        [Key]
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("thread_id")]
        public required int ThreadId { get; set; }
        [Column("posted_by")]
        public required int PostedBy { get; set; }
        [Column("like")]
        public int Like { get; set; }
        [Column("post_content")]
        public required string PostContent { get; set; }
        [Column("post_date")]
        public DateTime PostDate { get; set; }
        [Column("is_edited")]
        public bool IsEdited { get; set; }

        [InverseProperty("Post")]
        public ICollection<Reply>? Replies { get; set; }


    }
}