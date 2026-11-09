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

    //public static int? FromBirthdayString(string birthdayText)
    //{
    //    if (DateOnly.TryParseExact(birthdayText, "dd.MM.yyyy", CultureInfo.InvariantCulture,
    //        DateTimeStyles.None, out DateOnly birthday))
    //    {
    //        return CalculateAge.FromToday(birthday);
    //    }

    //    return null;
    //}
}
