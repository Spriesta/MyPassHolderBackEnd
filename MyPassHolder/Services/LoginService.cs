using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.RequestResponse;

namespace MyPassHolder.Services
{
    public class LoginService
    {
        private readonly LoginRepository _loginRepository;

        public LoginService(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public ResponseHandle login(LoginRequest userReq)
        {
            ResponseHandle response = new ResponseHandle();
            User? user = new User();

            try
            {
                if (string.IsNullOrEmpty(userReq.userInput))
                {
                    response.success = false;
                    response.errorMesssage = "The userInput field is required.";
                }
                else if (string.IsNullOrEmpty(userReq.password))
                {
                    response.success = false;
                    response.errorMesssage = "The password field is required.";
                }

                if (!response.success)
                    return response;             

                bool isEmail = Helper.IsEmailRegex(userReq.userInput);
                if (isEmail)
                    user.Email = userReq.userInput;
                else
                    user.PhoneNumber = userReq.userInput;

                user.Password = userReq.password;

                user = _loginRepository.checkAccountForLogin(user);
                if (user == null)
                {
                    response.success = false;
                    response.errorMesssage = "Kullanıcı Girişi veya Şifre Hatalı..!";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.errorMesssage = ex.Message;
            }

            return response;
        }
    }
}
