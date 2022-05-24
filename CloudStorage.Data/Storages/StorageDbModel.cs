namespace CloudStorage.Data.Storages;

public class StorageDbModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Secret { get; set; }
}
