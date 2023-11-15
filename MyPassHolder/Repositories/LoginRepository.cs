using Org.BouncyCastle.Ocsp;

namespace MyPassHolder.Repositories
{
    public class LoginRepository
    {
        private readonly PassHolderContext _context;

        public LoginRepository(PassHolderContext context)
        {
            this._context = context;
        }

        public User? checkAccountForLogin(User userReq)
        {
            User? user;

            try
            {
                if(userReq.Email != null)               
                    user = _context.Users.FirstOrDefault(x => x.Email == userReq.Email && x.Password == userReq.Password);               
                else
                    user = _context.Users.FirstOrDefault(x => x.PhoneNumber == userReq.PhoneNumber && x.Password == userReq.Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public User? getAccountWithEmail(string email)
        {
            User? user = new User();

            try
            {
                if (email != null)
                    user = _context.Users.FirstOrDefault(x => x.Email == email);             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public void changePassword(User user, string newPassword)
        {
            User? u = new User();

            try
            {
                u = _context.Users.FirstOrDefault(user => user.Email == user.Email);

                if(u != null)
                {
                    u.Password = newPassword;
                    u.UpdateDate = DateTime.Now;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
