using System;
using System.Collections.Generic;
using System.Text;

namespace newProfileBook.Model
{
    public enum Validator
    {
        Success,
        LoginIsTooShort,
        LoginIsTooLong,
        LoginIsTaken,
        LoginStartsWithNumber,
        PasswordIsTooShort,
        PasswordIsTooLong,
        PasswordIsWeak,
        PasswordsAreNotEqual
    }
}
