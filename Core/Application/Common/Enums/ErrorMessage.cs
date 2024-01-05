using System;
namespace Application.Common.Enums;

public static class ErrorMessage
{
    public static class Authentication
    {

        public static readonly string UserDoesNotExist = "User does not exist.";
        public static readonly string PasswordIncorrect = "Password incorrect.";
        public static readonly string AccessDenied = "Access denied.";

        public static readonly string EmailIsRequired = "Email is required.";
        public static readonly string PasswordIsRequired = "Password is required.";
        public static readonly string RePasswordIsRequired = "RePassword is required.";
        public static readonly string PasswordNotMatch = "Password not match.";
        public static readonly string EmailIsDuplicate = "Email is duplicate.";

        public static readonly string EmailIncorrectFormat = "Email incorrect format.";
        public static readonly string PasswordIncorrectFormat = "Password incorrect format.";


    }
}

