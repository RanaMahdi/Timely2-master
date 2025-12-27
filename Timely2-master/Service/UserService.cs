using Timely.Dtos;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class UserService : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(User user)
        {
            _unitOfWork._repositoryUsers.Add(user);
            _unitOfWork.Save();
        }
        public void Update(User user)
        {
            _unitOfWork._repositoryUsers.Update(user);
            _unitOfWork.Save();
        }
        public void Delete(int id)
        {
            _unitOfWork._repositoryUsers.Delete(id);
            _unitOfWork.Save();
        }
        public IEnumerable<User> GetAll()
        {
            return _unitOfWork._repositoryUsers.GetAll();
        }
        public User GetByEmailAndpass(LoginRequestDto loginRequest)
        {
            return _unitOfWork._repositoryUsers.GetAll().FirstOrDefault(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);
        }
        public User GetById(int id)
        {
            return _unitOfWork._repositoryUsers.GetById(id);
        }
        public bool IsEmailExist(string email)
        {
            return _unitOfWork._repositoryUsers.GetAll().Any(u => u.Email == email);
        }


    }
}