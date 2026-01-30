using System;
using System.Collections.Generic;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Services
{
    public class WalkService : IWalkService
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IWalkValidator _walkValidator;

        public WalkService(IWalkRepository walkRepository, IWalkValidator walkValidator)
        {
            _walkRepository = walkRepository;
            _walkValidator = walkValidator;
        }

        public void Add(Walk walk)
        {
            var validationResult = _walkValidator.Validate(walk);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            _walkRepository.Add(walk);
        }

        public void Update(Walk walk)
        {
            var validationResult = _walkValidator.Validate(walk);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            _walkRepository.Update(walk);
        }

        public void Delete(int id)
        {
            _walkRepository.Delete(id);
        }

        public IEnumerable<Walk> GetByClientIdDogId(int clientId, int dogId)
        {
            return _walkRepository.GetByClientIdDogId(clientId, dogId);
        }
    }
}
