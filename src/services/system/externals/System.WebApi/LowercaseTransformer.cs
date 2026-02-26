namespace System.WebApi
{
    public class LowercaseTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value) => value?.ToString()?.ToLower();
    }
}
