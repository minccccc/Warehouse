using Domain.Models;
using System.Text.RegularExpressions;

namespace Application.Extensions
{
    internal static class ProductExtensions
    {
        internal static Product Highlight(this Product product, List<string> highlights)
        {
            if (highlights?.Count > 0)
            {
                var highlightedMatches = SplitToWords(product.Description)
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

        internal static List<string> CollectCommonWords(this List<Product> products)
        {
            var commonWords = products
                .SelectMany(p => SplitToWords(p.Description.ToLower()))
                .GroupBy(w => w.Value)
                .OrderByDescending(descs => descs.Count())
                .Select(c => c.Key)
                .ToList()
                .GetRange(AppConstants.Highlight.Skip, AppConstants.Highlight.Top);

            return commonWords;
        }

        private static MatchCollection SplitToWords(string str)
        {
            return Regex.Matches(str, AppConstants.Highlight.TextWordsSplitter);
        }
    }
}
