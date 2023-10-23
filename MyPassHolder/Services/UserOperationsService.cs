using AutoMapper;
using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.RequestResponse;

namespace MyPassHolder.Services
{
    public class UserOperationsService
    {
        private readonly UserOperationsRepository _userOperationsRepository;
        private readonly IMapper _mapper;

        public UserOperationsService(UserOperationsRepository userOperationsRepository, IMapper mapper)
        {
            this._userOperationsRepository = userOperationsRepository;
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

                Category categoryObj = _mapper.Map<Category>(req);

                _userOperationsRepository.createCategory(categoryObj);
                response.data = "Kategori Oluşturma Başarılı.";
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
