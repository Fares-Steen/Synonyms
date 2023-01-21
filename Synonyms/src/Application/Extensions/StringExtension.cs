namespace Application.Extensions
{
    public static class StringExtension
    {
        public static string ClearWord(this string text)
        {
            return text.Trim().ToLower();
        }
    }
}
