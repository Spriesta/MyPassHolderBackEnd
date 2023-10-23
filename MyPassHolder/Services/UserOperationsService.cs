using AutoMapper;
using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.RequestResponse;

namespace MyPassHolder.Services
{
    public class UserOperationsService
    {
        private readonly UserOperationsRepository _repository;
        private readonly IMapper _mapper;

        public UserOperationsService(UserOperationsRepository userOperationsRepository, IMapper mapper)
        {
            this._repository = userOperationsRepository;
            this._mapper = mapper;
        }

        public ResponseHandle createCategory(CategoryRequest req)
        {
            ResponseHandle response = new ResponseHandle();

            try
            {
                if (string.IsNullOrEmpty(req.name))
                {
                    response.success = false;
                    response.errorMesssage = "The category name field is required.";
                }
                else if (req.userId == null || req.userId == 0)
                {
                    response.success = false;
                    response.errorMesssage = "The userId field is required.";
                }

                if (!response.success)
                    return response;



                //User userObj = _mapper.Map<User>(userReq);

                //User? user = _registerRepository.checkAccount(userObj);
                //if (user == null)
                //{
                //    _registerRepository.createAccount(userObj);
                //    response.data = "Kullanıcı Oluşturma Başarılı.";
                //}
                //else
                //{
                //    response.success = false;
                //    response.errorMesssage = "Zaten Sisteme Kayıtlısınız..!";
                //}
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
