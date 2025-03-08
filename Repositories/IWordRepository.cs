using Seiun.Entities;
namespace Seiun.Repositories;

public interface  IWordRepository : IBaseRepository<WordEntity>
{
    void BulkInsert(IEnumerable<WordEntity> words);
    Task<List<string>> GetExistingWordsAsync(IEnumerable<string> wordTexts);
    Task<List<WordEntity>> GetWordsByTagAsync(string tagName, Guid userId, int num);    
}
public interface  ITagRepository : IBaseRepository<TagEntity>
{
    Task<TagEntity?> GetByNameAsync(string name);
}
