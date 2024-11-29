namespace _4roomforum.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public required int ThreadId { get; set; }
        public required int PostedBy { get; set; }
        public required string PostTitle { get; set; }
        public int Like { get; set; }
        public required string PostContent { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsEdited { get; set; }
        public bool IsCreatedByAdmin { get; set; }
    }

    public class CreatePostDTO
    {
        public required int ThreadId { get; set; }
        public required int PostedBy { get; set; }
        public required string PostTitle { get; set; }
        public required string PostContent { get; set; }
        public bool IsCreatedByAdmin { get; set; }
    }

    public class UpdatePostDTO
    {
        public required string PostTitle { get; set; }
        public required string PostContent { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsEdited { get; set; } = true;
    }
}
