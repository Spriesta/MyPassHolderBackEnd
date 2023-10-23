using MyPassHolder.Common;
using MyPassHolder.RequestResponse;

namespace MyPassHolder.Repositories
{
    public class RegisterRepository
    {
        private readonly PassHolderContext _context;

        public RegisterRepository(PassHolderContext context)
        {
            this._context = context;
        }

        public void createAccount(User userReq)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                userReq.UpdateDate = currentDateTime;
                userReq.CreateDate = currentDateTime;

                _context.Users.Add(userReq);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User? checkAccount(User userReq)
        {
            User? user;

            try
            {
                user = _context.Users.FirstOrDefault(x => x.Email == userReq.Email || x.PhoneNumber == userReq.PhoneNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }
    }
}
