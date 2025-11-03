namespace SuperHeroAPI
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }

    public interface ISuperHeroRepository
    {
        Task<int> Add(SuperHero hero);
        Task<int> Update(SuperHero hero);
        Task<IEnumerable<SuperHero>> GetAll();
        Task<SuperHero> Get(int id);
        Task<int> Delete(int id);
    }

    public class SuperHeroRepository:ISuperHeroRepository
    {
        private readonly DataContext dataContext;
        public SuperHeroRepository(DataContext dataContext) { 
            this.dataContext = dataContext;
        }

        public async Task<int> Add(SuperHero hero)
        {
            await dataContext.SuperHeroes.AddAsync(hero);
            await dataContext.SaveChangesAsync();
            return hero.Id;
        }
        public async Task<int> Update(SuperHero hero) { 
            dataContext.Entry(hero).State = EntityState.Modified;
            await dataContext.SaveChangesAsync();
            return hero.Id;
        }
        public async Task<IEnumerable<SuperHero>> GetAll() { 
            return await dataContext.SuperHeroes.ToListAsync();
        }
        public async Task<SuperHero> Get(int id)
        {
            SuperHero? hero = await dataContext.SuperHeroes.FirstOrDefaultAsync(n => n.Id == id);
            
            return hero as SuperHero;
        }

        public async Task<int> Delete(int id)
        {
            SuperHero? hero = await dataContext.SuperHeroes.FindAsync(id);
            if (hero != null) { 
                return hero.Id;
            }
            return 0;
        }
    }
}
