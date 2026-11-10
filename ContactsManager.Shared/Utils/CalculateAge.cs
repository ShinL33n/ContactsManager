using System.Globalization;

namespace ContactsManager.Shared.Utils;

public static class CalculateAge
{
    public static int FromBirthday(DateOnly birthday)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - birthday.Year;
        if (today < birthday.AddYears(age)) age--;   // birthday hasn't happened yet this year
        return age;
    }
}
