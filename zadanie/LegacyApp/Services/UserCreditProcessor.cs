using System;

namespace LegacyApp
{
    public class UserCreditProcessor : IUserCreditProcessor
    {
        private readonly IUserCreditServiceFactory _creditServiceFactory;

        public UserCreditProcessor(IUserCreditServiceFactory creditServiceFactory)
        {
            _creditServiceFactory = creditServiceFactory;
        }

        public void ProcessCredit(User user, Client client)
        {
            if (client.Type == "VeryImportantClient")
            {
                // Very important clients do not require credit checks.
                user.HasCreditLimit = false;
                return;
            }

            using (var creditService = _creditServiceFactory.Create())
            {
                int creditLimit = creditService.GetCreditLimit(user.LastName, user.DateOfBirth);

                if (client.Type == "ImportantClient")
                {
                    // For important clients, double the credit limit.
                    user.CreditLimit = creditLimit * 2;
                }
                else
                {
                    // For normal clients, enforce credit limit.
                    user.HasCreditLimit = true;
                    user.CreditLimit = creditLimit;
                }
            }
        }
    }
}