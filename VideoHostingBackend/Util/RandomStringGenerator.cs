namespace VideoHostingBackend.Util;

internal static class RandomStringGenerator
{
    private static readonly Random Random = Random.Shared;
    private static readonly char[] Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

    public static string GenerateName()
    {
        const int length = 10;

        return new(
            Enumerable.Repeat(Chars, length)
                .Select(s => s[Random.Next(Chars.Length)])
                .ToArray());
    }
}