using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Data.Models;

namespace ProvaPub.Data.Configurations
{
    public class RandomNumberConfiguration : IEntityTypeConfiguration<RandomNumber>
    {
        public void Configure(EntityTypeBuilder<RandomNumber> builder)
        {
            builder.HasKey(rn => rn.Id);
            builder.HasIndex(rn => rn.Number).IsUnique();
        }
    }
}