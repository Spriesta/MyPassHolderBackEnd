using MyPassHolder.RequestResponse;

namespace MyPassHolder.Repositories
{
    public class UserOperationsRepository
    {
        private readonly PassHolderContext _context;

        public UserOperationsRepository(PassHolderContext context)
        {
            this._context = context;
        }

        public void createCategory(Category req)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                req.UpdateDate = currentDateTime;
                req.CreateDate = currentDateTime;

                _context.Categories.Add(req); 
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
