using Microsoft.EntityFrameworkCore;
using Minio;
using Seiun.Entities;

namespace Seiun.Repositories;

public class WordRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    :BaseRepository<WordEntity>(dbContext,minioClient), IWordRepository
{
    public async void BulkInsert(IEnumerable<WordEntity> words)
    {
        await DbContext.Set<WordEntity>().AddRangeAsync(words);
    }
    public async Task<List<string>> GetExistingWordsAsync(IEnumerable<string> wordTexts)
    {
        return await DbContext.Set<WordEntity>()
            .Where(w => wordTexts.Contains(w.WordText))
            .Select(w => w.WordText)
            .ToListAsync();
    }
}

public class TagRepository(SeiunDbContext dbContext, IMinioClient minioClient)
    :BaseRepository<TagEntity>(dbContext, minioClient), ITagRepository
{
    public async Task<TagEntity?> GetByNameAsync(string name)
    {
        return await DbContext.Set<TagEntity>().FirstOrDefaultAsync(t => t.Name == name);
    }
    
}
