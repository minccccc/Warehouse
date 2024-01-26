using Application.Common.Constants;
using Domain.Models;
using System.Text.RegularExpressions;

namespace Application.Extensions;

internal static class ProductExtension
{
    internal static Product Highlight(this Product product, List<string> highlights)
    {
        if (highlights?.Count > 0)
        {
            var highlightedMatches = product.Description.SplitToWords()
                .Where(m => highlights.Contains(m.Value, StringComparer.OrdinalIgnoreCase))
                .OrderByDescending(m => m.Index);

            var tag = AppConstants.Highlight.HighlightTag;

            foreach (Match match in highlightedMatches)
            {
                product.Description = product.Description
                    .Remove(match.Index, match.Length)
                    .Insert(match.Index, $"<{tag}>{match.Value}</{tag}>");
            }
        }

        return product;
    }
}