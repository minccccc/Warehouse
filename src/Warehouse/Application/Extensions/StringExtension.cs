using Application.Common.Constants;
using System.Text.RegularExpressions;

namespace Application.Extensions;

internal static class StringExtension
{
    public static MatchCollection SplitToWords(this string str)
    {
        return Regex.Matches(str, AppConstants.Highlight.TextWordsSplitter);
    }
}
