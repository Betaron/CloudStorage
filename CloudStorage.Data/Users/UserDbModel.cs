using CloudStorage.Data.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudStorage.Data.Users;

public class UserDbModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public byte[] Salt { get; set; }

    /* navigation properties */
    public StorageDbModel Storage { get; set; }

    internal class Map : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder.ToTable("users");

            builder.HasKey(it => it.Id).HasName("pk_user_id");

            builder.HasOne(it => it.Storage)
                .WithOne(it => it.User)
                .HasForeignKey<StorageDbModel>(it => it.UserId);
        }
    }
}
