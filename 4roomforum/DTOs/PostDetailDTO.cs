using System;

namespace _4roomforum.DTOs;

public class PostDetailDTO
{
    public int ThreadId { get; set; }
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public UserDTO Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ReplyDTO> Comments { get; set; }

    public PostDetailDTO()
    {
        Comments = new List<ReplyDTO>();
    }
}
