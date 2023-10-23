using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.RequestResponse;
using System.Xml;

namespace MyPassHolder.Services
{
    public class RegisterService
    {
        private readonly RegisterRepository _registerRepository;
        private readonly IMapper _mapper;

        public RegisterService(RegisterRepository registerRepository, IMapper mapper) 
        {
            this._registerRepository = registerRepository;
            this._mapper = mapper;
        }

        public ResponseHandle createAccount(RegisterRequest userReq)         
        {
            ResponseHandle response = new ResponseHandle();

            try
            {
                if(string.IsNullOrEmpty(userReq.fullName))
                {
                    response.success = false;
                    response.errorMesssage = "The fullName field is required.";
                }
                else if(string.IsNullOrEmpty(userReq.password))
                {
                    response.success = false;
                    response.errorMesssage = "The password field is required.";
                }
                else if (string.IsNullOrEmpty(userReq.email))
                {
                    response.success = false;
                    response.errorMesssage = "The email field is required.";
                }
                else if (string.IsNullOrEmpty(userReq.phoneNumber))
                {
                    response.success = false;
                    response.errorMesssage = "The phoneNumber field is required.";
                }

                if (!response.success)
                    return response;

                User userObj = _mapper.Map<User>(userReq);

                User? user = _registerRepository.checkAccount(userObj);
                if(user == null) 
                {
                    _registerRepository.createAccount(userObj);
                    response.data = "Kullanıcı Oluşturma Başarılı.";
                }
                else 
                {
                    response.success = false;
                    response.errorMesssage = "Zaten Sisteme Kayıtlısınız..!";
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
