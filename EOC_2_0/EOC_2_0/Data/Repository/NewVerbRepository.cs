using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;

namespace EOC_2_0.Data.Repository
{
    public class NewVerbRepository : IBaseRepository<NewVerb>
    {
        private readonly AppDBContent appDBContent;

        public NewVerbRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public async Task Create(NewVerb entity)
        {
            await appDBContent.NewVerbs.AddAsync(entity);
            await appDBContent.SaveChangesAsync();
        }

        public async Task Delete(NewVerb entity)
        {
            appDBContent.NewVerbs.Remove(entity);
            await appDBContent.SaveChangesAsync();
        }

        public IQueryable<NewVerb> GetAll()
        {
            return appDBContent.NewVerbs;
        }

        public async Task<NewVerb> Update(NewVerb entity)
        {
            appDBContent.NewVerbs.Update(entity);
            await appDBContent.SaveChangesAsync();
            return entity;
        }
    }
}
