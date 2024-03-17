using Microsoft.EntityFrameworkCore;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;

namespace EOC_2_0.Data.Repository
{
    public class VerbRepository : IBaseRepository<Verb>
    {
        private readonly AppDBContent appDBContent;

        public VerbRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public async Task Create(Verb entity)
        {
            await appDBContent.Verbs.AddAsync(entity);
            await appDBContent.SaveChangesAsync();
        }

        public async Task Delete(Verb entity)
        {
            appDBContent.Verbs.Remove(entity);
            await appDBContent.SaveChangesAsync();
        }

        public IQueryable<Verb> GetAll()
        {
            return appDBContent.Verbs;
        }

        public async Task<Verb> Update(Verb entity)
        {
            appDBContent.Verbs.Update(entity);
            await appDBContent.SaveChangesAsync();
            return entity;
        }
    }
}
