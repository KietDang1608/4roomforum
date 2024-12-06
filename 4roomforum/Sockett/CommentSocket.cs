
using Microsoft.AspNetCore.SignalR;

namespace _4roomforum.Sockett
{
    public class CommentSocket : Hub
    {
        public async Task ReceiveComment(int postId, string replyContent, string userName, int replyToReply,int? replyId)
        {
            await Clients.All.SendAsync("ReceiveComment", postId, replyContent, userName,replyToReply, replyId );
        }
    }
}
