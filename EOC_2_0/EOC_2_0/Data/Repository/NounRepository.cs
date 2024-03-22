using Microsoft.EntityFrameworkCore;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;

namespace EOC_2_0.Data.Repository
{
    public class NounRepository : IBaseRepository<Noun>
    {
        private readonly AppDBContent appDBContent;

        public NounRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public async Task Create(Noun entity)
        {
            await appDBContent.Nouns.AddAsync(entity);
            await appDBContent.SaveChangesAsync();
        }

        public async Task Delete(Noun entity)
        {
            appDBContent.Nouns.Remove(entity);
            await appDBContent.SaveChangesAsync();
        }

        public IQueryable<Noun> GetAll()
        {
            return appDBContent.Nouns;
        }

        public async Task<Noun> Update(Noun entity)
        {
            appDBContent.Nouns.Update(entity);
            await appDBContent.SaveChangesAsync();
            return entity;
        }
    }
}
