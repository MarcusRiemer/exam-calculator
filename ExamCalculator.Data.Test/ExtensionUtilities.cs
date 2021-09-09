namespace ExamCalculator.Data.Test
{
    public static class ExtensionUtilities
    {
        public static string Next(this char c, int step) => ((char) (c + step)).ToString();
    }
}