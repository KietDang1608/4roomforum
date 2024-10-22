namespace PostService.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public required int ThreadId { get; set; }
        public required int PostedBy { get; set; }
        public required string PostContent { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsEdited { get; set; }
    }
}
