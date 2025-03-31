using System;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserValidator _validator;
        private readonly IUserCreditProcessor _creditProcessor;

        // Parameterless constructor for backward compatibility with LegacyAppConsumer.
        public UserService()
            : this(new ClientRepository(), new UserValidator(), new UserCreditProcessor(new UserCreditServiceFactory()))
        {
        }

        // Constructor for dependency injection (e.g., unit tests)
        public UserService(IClientRepository clientRepository, IUserValidator validator, IUserCreditProcessor creditProcessor)
        {
            _clientRepository = clientRepository;
            _validator = validator;
            _creditProcessor = creditProcessor;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            // Validate user input.
            if (!_validator.Validate(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            // Get client from repository.
            var client = _clientRepository.GetById(clientId);

            // Create new user.
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email,
                DateOfBirth = dateOfBirth,
                Client = client
            };

            // Process credit details.
            _creditProcessor.ProcessCredit(user, client);

            // Enforce credit limit if applicable.
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            // Persist user using legacy data access code (unchanged).
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}