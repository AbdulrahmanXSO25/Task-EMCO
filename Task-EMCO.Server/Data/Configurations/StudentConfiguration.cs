namespace Task_EMCO.Server.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
       builder.ToTable("Students");

       builder.HasKey(s => s.Id);

       builder.HasIndex(s => s.Id).IsUnique();
       builder.HasIndex(s => s.Email).IsUnique();
       builder.HasIndex(s => s.PhoneNumber).IsUnique();

       builder.Property(s => s.FirstName)
              .IsRequired()
              .HasMaxLength(50);

       builder.Property(s => s.SecondName)
              .IsRequired()
              .HasMaxLength(50);

       builder.Property(s => s.LastName)
              .HasMaxLength(50);

       builder.Property(s => s.DateOfBirth)
              .IsRequired();

       builder.Property(s => s.IsMale)
              .IsRequired();

       builder.Property(s => s.Email)
              .IsRequired()
              .HasMaxLength(100);

       builder.Property(s => s.PhoneNumber)
              .IsRequired()
              .HasMaxLength(20);       

       builder.HasMany(s => s.StudentSubjects)
              .WithOne(ss => ss.Student)
              .HasForeignKey(ss => ss.StudentId);

       builder.HasMany(s => s.StudentSubjects)
              .WithOne(ss => ss.Student)
              .HasForeignKey(ss => ss.StudentId);
    }
}