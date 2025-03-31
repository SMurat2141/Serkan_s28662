using System;

namespace LegacyApp
{
    public interface IUserValidator
    {
        bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth);
        int CalculateAge(DateTime dateOfBirth);
    }
}