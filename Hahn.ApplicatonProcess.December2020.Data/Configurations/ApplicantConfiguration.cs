using static Hahn.ApplicatonProcess.December2020.Domain.Constants.Validations;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicatonProcess.December2020.Data.Configuration
{
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.Property(it => it.Name)
                .IsRequired()
                .HasMaxLength(MAX_LENGTH);

            builder.Property(it => it.FamilyName)
                .IsRequired()
                .HasMaxLength(MAX_LENGTH);

            builder.Property(it => it.Address)
                .IsRequired()
                .HasMaxLength(MAX_LENGTH_ADDRESS);

            builder.Property(it => it.CountryOfOrigin)
                .IsRequired()
                .HasMaxLength(MAX_LENGTH);

            builder.Property(it => it.EMailAddress)
                .IsRequired()
                .HasMaxLength(MAX_LENGTH);

            builder.Property(it => it.Age)
                .IsRequired();

            builder.Property(it => it.Hired)
                .IsRequired();
        }
    }
}
