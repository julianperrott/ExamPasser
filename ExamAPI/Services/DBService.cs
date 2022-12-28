using ExamAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExamAPI.Services;

public interface IDataAccessObject
{
    string Id { get; set; }
}

public class DBService<T> where T : class, IDataAccessObject
{
    private readonly IMongoCollection<T> _collection;

    public DBService()
    {
        var databaseSettings = new DatabaseSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "ExamStore",
        };

        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.DatabaseName);

        _collection = mongoDatabase.GetCollection<T>(typeof(T).Name + "s");
    }

    public async Task<List<T>> GetAsync() => await _collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T newItem) => await _collection.InsertOneAsync(newItem);

    public async Task UpdateAsync(string id, T updatedItem) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedItem);

    public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}