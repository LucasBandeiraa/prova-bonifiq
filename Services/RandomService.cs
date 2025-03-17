using ProvaPub.Data.Models;
using ProvaPub.Data.Repository;
using ProvaPub.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class RandomService
    {
        private readonly IRandomNumberRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Random _random = new Random();

        public RandomService(IRandomNumberRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetRandom()
        {
            int number;
            bool isUnique;

            do
            {
                number = _random.Next(100); // Numero aleatorio
                isUnique = !await _repository.Exists(number);
            } while (!isUnique);

            await _repository.Add(new RandomNumber { Number = number });
            await _unitOfWork.SaveChangesAsync();

            return number;
        }
    }
}