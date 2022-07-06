using BetsAreMade.DataContracts.Dbo.Users;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsAreMade.Repositories.NoSQL
{
    public class UserRepository
    {
        private IMongoCollection<UserDbo> _collection;

        public UserRepository(IMongoCollection<UserDbo> collection) 
        {
            _collection = collection;
        }
        public async Task<UserDbo> CreateAsync(UserDbo user)
        {
            user.SetId(ObjectId.GenerateNewId());

            ProcessBets(user);

            await _collection.InsertOneAsync(user);

            return user;
        }

        public async Task<UserDbo> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<List<UserDbo>> GetAsync()
        {
            return await _collection.Find(Builders<UserDbo>.Filter.Empty).ToListAsync();
        }

        public async Task<UserDbo> UpdateAsync(UserDbo user)
        {
            ProcessBets(user);

            var result = await _collection.ReplaceOneAsync(x => x.Id == user.Id, user);

            return user;
        }
        
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        private void ProcessBets(UserDbo user)
        {
            foreach (var b in user.Bets?.Where(b => string.IsNullOrEmpty(b.Id)))
            {
                b.SetId(ObjectId.GenerateNewId());
            }
        }

    }
}
