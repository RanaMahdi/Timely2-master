using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Repositories;

namespace Timely.Service
{
    public class ClientService : IClientServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Client client)
        {
            _unitOfWork._clientRepo.Add(client);
            _unitOfWork.Save();
            return true;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByUid(string uid)
        {
            var client = _unitOfWork._clientRepo.GetByUid(uid);
            if (client == null)
            {
                return false;
            }
            _unitOfWork._clientRepo.Delete(client.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Client> GetAll()
        {
            return _unitOfWork._clientRepo.GetAll();
        }

        public Client? GetByUid(string uid)
        {
            return _unitOfWork._clientRepo.GetByUid(uid);
        }

        public IEnumerable<Nationality> GetNationalities()
        {
            return _unitOfWork._nationalityRepo.GetAll();
        }

        public bool Update(string uid, Client updatedClient)
        {
            var existingClient = _unitOfWork._clientRepo.GetByUid(uid);
            if (existingClient == null)
            {
                return false;
            }

            existingClient.Name = updatedClient.Name;
            existingClient.Phone = updatedClient.Phone;
            existingClient.Email = updatedClient.Email;
            existingClient.Password = updatedClient.Password;
            existingClient.Gender = updatedClient.Gender;
            existingClient.NationalityId = updatedClient.NationalityId;

            _unitOfWork._clientRepo.Update(existingClient);
            _unitOfWork.Save();

            return true;
        }
    }
}
