namespace Gravity.Express.Application.Common.Models;

public sealed record ApiSettings
{
    public const string SectionName = "Api";

    public string GatewayEndpoint { get; init; }= null!;

    public string Secret { get; init; }= null!;

    public string ClientId { get; init; }= null!;

    public string InternalToolEndpoint { get; init; }= null!;

    public string UserEndpoint { get; set; } = null!;
}