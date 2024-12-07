using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public interface IReplyRepo
    {
        Task<IEnumerable<ReplyDTO>> GetAllRepliesAsync(int PostId);
        Task<PagedResult<ReplyDTO>> GetPagedAsync(int pageNumber, int pageSize, int PostId);
       
    }
}
