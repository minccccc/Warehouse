using Application.Common.Constants;
using Domain.Models;

namespace Application.Extensions;

internal static class ListProductExtension
{
    internal static List<string> CollectCommonWords(this List<Product> products)
    {
        var commonWords = products
            .SelectMany(p => p.Description.ToLower().SplitToWords())
            .GroupBy(w => w.Value)
            .OrderByDescending(descs => descs.Count())
            .Select(c => c.Key)
            .ToList()
            .GetRange(AppConstants.Highlight.Skip, AppConstants.Highlight.Top);

        return commonWords;
    }

}
