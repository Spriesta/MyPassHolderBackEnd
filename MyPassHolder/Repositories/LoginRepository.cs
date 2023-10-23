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
    }
}
