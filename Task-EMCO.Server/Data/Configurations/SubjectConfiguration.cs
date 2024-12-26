namespace Task_EMCO.Server.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
       builder.ToTable("Subjects");

       builder.HasKey(s => s.Id);

       builder.HasIndex(s => s.Id).IsUnique();
       builder.HasIndex(s => s.Code).IsUnique();

       builder.Property(s => s.Code)
              .IsRequired()
              .HasMaxLength(7);

       builder.Property(s => s.Name)
              .IsRequired()
              .HasMaxLength(100);

       builder.Property(s => s.Description)
              .HasMaxLength(500);

       builder.HasMany(s => s.StudentSubjects)
              .WithOne(ss => ss.Subject)
              .HasForeignKey(ss => ss.SubjectId);
    }
}
