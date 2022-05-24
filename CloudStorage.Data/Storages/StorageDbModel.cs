using CloudStorage.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudStorage.Data.Storages;

public class StorageDbModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Secret { get; set; }

    /* navigation properties */
    public UserDbModel User { get; set; }

    internal class Map : IEntityTypeConfiguration<StorageDbModel>
    {
        public void Configure(EntityTypeBuilder<StorageDbModel> builder)
        {
            builder.ToTable("storages");

            builder.HasKey(it => it.Id).HasName("pk_storage_id");
        }
    }
}
