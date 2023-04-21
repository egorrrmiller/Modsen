using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modsen.Domain.Models;

namespace Modsen.Database.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(email => email.Email);
        builder.Property(email => email.Email).IsRequired().HasMaxLength(30);

        builder.Property(pass => pass.Password).IsRequired().HasMaxLength(30);
    }
}