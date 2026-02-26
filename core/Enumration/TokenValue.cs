namespace Core.Enumration
{
    public class TokenValue(int Id, string Name): Enumeration(Id, Name)
    {
        public static TokenValue Email = new(1, "EML");
        public static TokenValue PhoneNumber = new(2, "PNB");
    }
}
