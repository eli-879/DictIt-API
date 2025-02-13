using DictItApi.Database;
using DictItApi.Dtos;
using DictItApi.Entities;
using DictItApi.Result;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DictItApi.Services
{
    public class SaveWordService : ISaveWordService
    {
        private readonly DictItDbContext _dictItDbContext;
        public SaveWordService(DictItDbContext dictItDbContext) 
        {
            _dictItDbContext = dictItDbContext;
        }

        public async Task<Result<List<SavedWordResponseDto>>> GetSavedWordsByUser(string userId)
        {
            var user = await _dictItDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Result<List<SavedWordResponseDto>>.Failure(HttpStatusCode.BadRequest, $"No user found with user id of: {userId}.");
            }

            var savedWords = _dictItDbContext.SavedWords.Where(savedWord => savedWord.User == user);

            var responseDto = savedWords.Select(s => new SavedWordResponseDto
            {
                Word = s.Word
            }).ToList();

            return Result<List<SavedWordResponseDto>>.Success(responseDto);
        }

        public async Task<Result<bool>> SaveWordAsync(string word, string userId)
        {

            var user = await _dictItDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Result<bool>.Failure(HttpStatusCode.BadRequest, $"No user found with user id of: {userId}.");
            }

            var newSavedWord = new SavedWord { Word = word, UserId = user.Id, User = user};

            if (_dictItDbContext.SavedWords.Any(savedWord => savedWord.Word == newSavedWord.Word && savedWord.UserId == newSavedWord.UserId))
            {
                return Result<bool>.Failure(HttpStatusCode.BadRequest, $"You have already added the word: {word}.");
            }

            _dictItDbContext.SavedWords.Add(newSavedWord);

            // Handle duplicate
            _dictItDbContext.SaveChanges();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> RemoveWordAsync(string word, string userId)
        {
            var user = await _dictItDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Result<bool>.Failure(HttpStatusCode.BadRequest, $"No user found with user id of: {userId}.");
            }

            var savedWord = await _dictItDbContext.SavedWords.FirstOrDefaultAsync(savedWord => savedWord.Word == word && savedWord.UserId == user.Id);

            if (savedWord == null)
            {
                return Result<bool>.Failure(HttpStatusCode.BadRequest, $"You have not added this word: {word}.");
            }

            _dictItDbContext.SavedWords.Remove(savedWord);

            _dictItDbContext.SaveChanges();

            return Result<bool>.Success(true);
        }
    }
}
