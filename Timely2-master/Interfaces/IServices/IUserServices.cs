using Timely.Dtos;
using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IUserServices
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool IsEmailExist(string email);
        void Create(User user);
        void Update(User user);
        void Delete(int id);


        User GetByEmailAndpass(LoginRequestDto loginRequest);

    }
}
