using AutoMapper;
using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.RequestResponse;
using Org.BouncyCastle.Ocsp;

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

        public ResponseHandle createOrUpdateMyPassword(createOrUpdateMyPasswordRequest req)
        {
            ResponseHandle response = new ResponseHandle();

            try
            {
                if (string.IsNullOrEmpty(req.description))
                {
                    response.success = false;
                    response.errorMesssage = "The description name field is required.";
                }
                else if (string.IsNullOrEmpty(req.password))
                {
                    response.success = false;
                    response.errorMesssage = "The password name field is required.";
                }
                else if (req.userId == 0 || req.userId == null)
                {
                    response.success = false;
                    response.errorMesssage = "The userId field is required.";
                }
                else if (req.categoryId == 0 || req.categoryId == null)
                {
                    response.success = false;
                    response.errorMesssage = "The categoryId field is required.";
                }

                if (!response.success)
                    return response;

                MyPassword obj = _mapper.Map<MyPassword>(req);

                if (obj.Id == 0 || obj.Id == null)
                    _userOperationsRepository.createMyPassword(obj);
                else
                    _userOperationsRepository.updateMyPassword(obj);

                response.data = "İşlem Başarılı..!";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.errorMesssage = ex.Message;
            }

            return response;
        }

        public ResponseHandle deleteMyPassword(long id)
        {
            ResponseHandle response = new ResponseHandle();

            try
            {
                if (id == 0 || id == null)
                {
                    response.success = false;
                    response.errorMesssage = "The id field is required.";
                }             

                if (!response.success)
                    return response;

                _userOperationsRepository.deleteMyPassword(id);
                response.data = "İşlem Başarılı..!";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.errorMesssage = ex.Message;
            }

            return response;
        }

        public ResponseHandle listCategory(long userId)
        {
            ResponseHandle response = new ResponseHandle();

            try
            {
                if (userId == 0 || userId == null)
                {
                    response.success = false;
                    response.errorMesssage = "The id field is required.";
                }

                if (!response.success)
                    return response;

                List<Category> records = _userOperationsRepository.listCategory(userId);

                List<CategoryResponse> categoryResponse = _mapper.Map<List<CategoryResponse>>(records);
                response.data = categoryResponse;
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
