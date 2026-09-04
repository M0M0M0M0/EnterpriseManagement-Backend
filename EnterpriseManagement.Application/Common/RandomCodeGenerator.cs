namespace EnterpriseManagement.Application.Common;

public static class RandomCodeGenerator
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string Generate(int length) =>
        string.Create(length, Random.Shared, (span, random) =>
        {
            for (var i = 0; i < span.Length; i++)
            {
                span[i] = Alphabet[random.Next(Alphabet.Length)];
            }
        });
}
