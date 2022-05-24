namespace CloudStorage.Core.Domains.Storages;

public class Storage
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Secret { get; set; }
}
