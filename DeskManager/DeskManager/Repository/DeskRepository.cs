namespace DeskManager.Repository
{
    public class DeskRepository : IDeskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DeskRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
