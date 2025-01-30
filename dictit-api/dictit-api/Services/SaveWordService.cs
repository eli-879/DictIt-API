using DictItApi.Database;
using DictItApi.Entities;
using DictItApi.Repository;
using DictItApi.Result;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DictItApi.Services
{
    public class SaveWordService : ISaveWordService
    {
        readonly SaveWordDbContext _savedWordContext;
        readonly UsersDbContext _usersContext;
        public SaveWordService(SaveWordDbContext saveWordDbContext, UsersDbContext usersDbContext) 
        {
            _savedWordContext = saveWordDbContext;
            _usersContext = usersDbContext;
        }

        public async Task<Result<bool>> SaveWordAsync(string word, string userId)
        {
            var user = await _usersContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Result<bool>.Failure(HttpStatusCode.BadRequest, $"No user found with user id of: {userId}.");
            }

            var newSavedWord = new SavedWord { Word = word, User = user};
            _savedWordContext.SavedWords.Add(newSavedWord);

            _savedWordContext.SaveChanges();
            return Result<bool>.Success(true);
            
        }
    }
}
