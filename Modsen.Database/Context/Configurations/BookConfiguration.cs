using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modsen.Domain.Models;

namespace Modsen.Database.Context.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookModel>
{
    public void Configure(EntityTypeBuilder<BookModel> builder)
    {
        builder.HasKey(key => key.Id);

        builder.Property(isbn => isbn.Isbn).IsRequired().HasMaxLength(17);
        builder.HasAlternateKey(isbn => isbn.Isbn);

        builder.Property(title => title.Title).IsRequired().HasMaxLength(30);
        builder.Property(desc => desc.Description).HasMaxLength(200);
        builder.Property(author => author.Author).HasMaxLength(35);

        builder.Property(borrowTime => borrowTime.BorrowTime).HasDefaultValue(DateTime.UtcNow.AddHours(3));
        builder.Property(returnTime => returnTime.ReturnTime).HasDefaultValue(DateTime.UtcNow.AddHours(3).AddDays(7));
    }
}