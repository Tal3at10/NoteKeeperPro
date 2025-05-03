using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteKeeperPro.Domain.Entities.Collaborators;

namespace NoteKeeperPro.Infrastructure.Presistance.Data.Configurations
{
    internal class CollaboratorConfigurations : IEntityTypeConfiguration<Collaborator>
    {
        public void Configure(EntityTypeBuilder<Collaborator> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PermissionType)
                .HasConversion<int>()  // Store the enum as an integer in the database
                .IsRequired();  // Make sure it's required
        }
    }
}
