namespace Application.Common.Constants;

public static class AppConstants
{
    public const string ProductsCacheKey = "Products";
    public const string ProductsSummaryCacheKey = "ProductsSumary";

    public static class Highlight
    {
        public const string TextWordsSplitter = @"\w+[^\s]*\w+|\w";
        public const string HighlightTag = "em";
        public const int Skip = 5;
        public const int Top = 10;
    }

    public static class Validations
    {
        public const string MinPrice_Negative = "MinPrice can not be negative";
        public const string MaxPrice_Negative = "MaxPrice can not be negative";
        public const string MinPrice_LessThanMaxPrice = "MinPrice must be less than MaxPrice";
    }

    public static class Configuration
    {
        public const string MockyClientSection = "MockyClient";
        public const string SchedulerSection = "Scheduler";
    }
}