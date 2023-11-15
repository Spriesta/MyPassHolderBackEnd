using Microsoft.EntityFrameworkCore;
using MyPassHolder.Models;
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

        public void createMyPassword(MyPassword req)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                req.UpdateDate = currentDateTime;
                req.CreateDate = currentDateTime;

                _context.MyPasswords.Add(req);                
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void updateMyPassword(MyPassword req)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                req.UpdateDate = currentDateTime;

                MyPassword? obj = _context.MyPasswords.FirstOrDefault(x => x.Id == req.Id);
                if (obj != null) 
                {
                    obj.UpdateDate = currentDateTime;
                    obj.Description = req.Description;
                    obj.Password = req.Password;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteMyPassword(long id)
        {
            try
            {
                MyPassword? obj = _context.MyPasswords.FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    _context.MyPasswords.Remove(obj);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> listCategory(long userId)
        {
            try
            {
                IQueryable<Category> obj = _context.Categories.Where(x => x.UserId == userId);
                return obj.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
