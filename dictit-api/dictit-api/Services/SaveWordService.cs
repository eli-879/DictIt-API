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

        public async Task<Result<SavedWordResponseDto>> GetSavedWordsByUser(string userId, string filter, string sort, string order, int page, int numPerPage)
        {
            var user = await _dictItDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Result<SavedWordResponseDto>.Failure(HttpStatusCode.BadRequest, $"No user found with user id of: {userId}.");
            }

            var savedWords = _dictItDbContext.SavedWords.Where(savedWord => savedWord.User == user);
            var resultsLength = savedWords.Count();

            savedWords = HandleQueryParameters(savedWords, filter, sort, order, page, numPerPage);

            var savedWordDto = savedWords.Select(s => new SavedWordDto
            {
                Word = s.Word,
                DateAdded = s.DateAdded
            }).ToList();

            var responseDto = new SavedWordResponseDto
            {
                SavedWords = savedWordDto,
                ResultsLength = resultsLength
            };

            return Result<SavedWordResponseDto>.Success(responseDto);
        }

        public async Task<Result<SavedWordResponseDto>> SaveWordAsync(string word, string userId)
        {

            var user = await _dictItDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Result<SavedWordResponseDto>.Failure(HttpStatusCode.BadRequest, $"No user found with user id of: {userId}.");
            }

            var newSavedWord = new SavedWord { Word = word, UserId = user.Id, User = user };

            if (_dictItDbContext.SavedWords.Any(savedWord => savedWord.Word == newSavedWord.Word && savedWord.UserId == newSavedWord.UserId))
            {
                return Result<SavedWordResponseDto>.Failure(HttpStatusCode.BadRequest, $"You have already added the word: {word}.");
            }

            _dictItDbContext.SavedWords.Add(newSavedWord);

            // Handle duplicate
            _dictItDbContext.SaveChanges();

            var savedWordDto = new SavedWordDto { Word = newSavedWord.Word, DateAdded = newSavedWord.DateAdded };

            return Result<SavedWordResponseDto>.Success(new SavedWordResponseDto { SavedWords = [savedWordDto], ResultsLength = 1 });
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

        private static IQueryable<SavedWord> HandleQueryParameters(IQueryable<SavedWord> savedWords, string filter, string sort, string order, int page, int numPerPage)
        {
            bool ascending = order == "asc";
            var result = savedWords;

            // filter
            result = filter != null ? result.Where(r => r.Word.ToUpper().Contains(filter.ToUpper())) : result;

            // sort and order
            switch (sort)
            {
                case "word":
                    result = ascending ? result.OrderBy(sw => sw.Word) : result.OrderByDescending(sw => sw.Word);
                    break;
                case "dateAdded":
                    result = ascending ? result.OrderBy(sw => sw.DateAdded) : result.OrderByDescending(sw => sw.DateAdded);
                    break;
            }

            // pagination
            return result.Skip(page * numPerPage).Take(numPerPage);
        }
    }
}
