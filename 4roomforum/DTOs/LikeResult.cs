namespace _4roomforum.DTOs
{
    public class LikeResult
    {
        public bool IsSuccessful { get; set; }
        public bool? IsLiked { get; set; }
        public string ErrorMessage { get; set; }

        public LikeResult() { }
        public LikeResult(bool isSuccessful, string errorMessage) 
        { 
            IsSuccessful = isSuccessful; 
            IsLiked = IsLiked;
            ErrorMessage = errorMessage; 
        } 
    }
}
