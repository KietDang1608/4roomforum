﻿using PostService.DTOs;
using PostService.Models;

namespace PostService.Data
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<PagedResult<Post>> getPostsByThreadIdAsync(int threadId, int page, int userId, int pageSize);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task<bool> IncreaseLikeCountAsync(int postId);
        Task<bool> DecreaseLikeCountAsync(int postId);
    }
}
