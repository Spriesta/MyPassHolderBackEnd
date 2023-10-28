using HelperLib;
using MailSender;
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


        public ResponseHandle forgetPassword(string email)  //şifremi unuttum maili token ve link ile gidecek 
        {
            ResponseHandle response = new ResponseHandle();
            MailRequest mailRequest = new MailRequest();
            //User? user = new User();

            try
            {
                bool isEmail = Helper.IsEmailRegex(email);

                if (string.IsNullOrEmpty(email) || !isEmail)
                {
                    response.success = false;
                    response.errorMesssage = "The email field is required.";
                }

                if (!response.success)
                    return response;

                // Bu kısımda sistemde email adresi varmı kontrol et

                mailRequest.toMailAdress = email;
                mailRequest.messageSubject = "Şifremi Unuttum";
                mailRequest.messageBody = "Merhabalar, Şifre Tutucu Parolanızı Değiştirmek İçin Lütfen Aşağıdaki Linke Tıklayınız.";

                response = MailSender.MailSender.send(mailRequest);
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
