namespace _4roomforum.DTOs
{
    public class LikeResult
    {
        public bool IsSuccessful { get; set; }
        public bool? IsLiked { get; set; }
        public int TotalLikes { get; set; }
        public string ErrorMessage { get; set; }

        public LikeResult() { }

        public LikeResult(bool isSuccessful, bool? isLiked, int totalLikes, string errorMessage)
        {
            IsSuccessful = isSuccessful;
            IsLiked = isLiked;
            TotalLikes = totalLikes; 
            ErrorMessage = errorMessage;
        }
    }
}
