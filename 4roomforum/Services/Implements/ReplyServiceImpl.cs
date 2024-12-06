﻿using _4roomforum.DTOs;
using _4roomforum.Services.Interfaces;
using System.Net.Http;

namespace _4roomforum.Services.Implements
{
    public class ReplyServiceImpl : IReplyService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ReplyServiceImpl> _logger;

        public ReplyServiceImpl(HttpClient httpClient, ILogger<ReplyServiceImpl> logger)
        {
            _client = httpClient;         
            _logger = logger;
            _client.BaseAddress = new Uri("http://localhost:5003/");
        }

        public async Task<PagedResult<ReplyDTO>> GetAllReplies(int PostId, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var response = await _client.GetAsync($"api/reply/get-all-by-post/{PostId}?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var replies = await response.Content.ReadFromJsonAsync<PagedResult<ReplyDTO>>();
                    return replies;
                }
                else
                {
                    _logger.LogError($"Failed to get replies. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAllReplies: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAllReplies: {ex.Message}");
                return null;
            }
        }

        public async Task<ReplyDTO> GetAReply(int ReplyId)
        {
            try
            {
                var response = await _client.GetAsync($"api/reply/{ReplyId}");
                if (response.IsSuccessStatusCode)
                {
                    var reply = await response.Content.ReadFromJsonAsync<ReplyDTO>();
                    return reply;
                }
                else
                {
                    _logger.LogError($"Failed to get reply ID{ReplyId}. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAReply ID{ReplyId}: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAReply ID{ReplyId}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateReply(CreateReplyDTO createReplyDTO)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("api/reply", createReplyDTO);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to create reply. Status Code: {response.StatusCode}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in CreateReply: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in CreateReply: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteReply(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/reply/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to delete reply ID:{id}. Status Code: {response.StatusCode}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in DeleteReply ID{id}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in DeleteReply ID{id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateReply(int id, UpdateReplyDTO updateReplyDTO)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"api/reply/{id}", updateReplyDTO);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to update reply ID: {id}. Status Code: {response.StatusCode}");
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unexpected error in UpdateReply for ID{id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ReactToReply(int replyId, int userId, int vote)
        {
            try
            {
                if(vote != -1 || vote != 1 || vote != 0)
                {
                    _logger.LogError($"Failed to react to reply ID: {replyId}. Invalid value");
                    return false;
                }

                var response = await _client.PutAsync($"api/reply/likereply/{replyId}/{userId}/{vote}", null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    _logger.LogError($"Failed to react to reply ID: {replyId}. Status Code: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Unexpected error in ReactToReply for ID{replyId}: {ex.Message}");
                return false;
            } 
        }

        public async Task<IEnumerable<LikeOfReplyDTO>> GetAllReaction(int ReplyId)
        {
            try
            {
                var response = await _client.GetAsync($"/api/reply/get-all-react/{ReplyId}");
                if (response.IsSuccessStatusCode)
                {
                    var reaction = await response.Content.ReadFromJsonAsync<IEnumerable<LikeOfReplyDTO>>();
                    return reaction;
                }
                else
                {
                    _logger.LogError($"Failed to get reaction. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error in GetAllReaction: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetAllReaction: {ex.Message}");
                return null;
            }
        }
    }
}
