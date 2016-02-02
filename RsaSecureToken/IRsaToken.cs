namespace RsaSecureToken
{
    public interface IRsaToken
    {
        string GetRandom(string account);
    }
}