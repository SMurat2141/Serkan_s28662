using System;

namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        public bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return false;

            // Check email contains both "@" and "."
            if (!(email.Contains("@") && email.Contains(".")))
                return false;

            if (CalculateAge(dateOfBirth) < 21)
                return false;

            return true;
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now < dateOfBirth.AddYears(age))
                age--;
            return age;
        }
    }
}