using System.Globalization;

namespace Tasks.Domain.Extensions;
public static class StringExtension
{
    public static bool CompareWithoutConsideringSpecialCharactersAndUpperCase(this string origen, string searchFor)
    {
        var index = CultureInfo.CurrentCulture.CompareInfo.IndexOf(origen, searchFor, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);

        return index >= 0;
    }

    public static string RemoveAccent(this string text)
    {
        return new string(text.Normalize(System.Text.NormalizationForm.FormD).Where(x => char.GetUnicodeCategory(x) != UnicodeCategory.NonSpacingMark).ToArray());
    }
}
