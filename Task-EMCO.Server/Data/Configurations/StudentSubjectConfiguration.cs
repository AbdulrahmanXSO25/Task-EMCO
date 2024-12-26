namespace Task_EMCO.Server.Data.Configurations;

public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
{
    public void Configure(EntityTypeBuilder<StudentSubject> builder)
    {
        builder.ToTable("StudentSubjects");

        builder.HasKey(ss => new { ss.StudentId, ss.SubjectId });

        builder.HasIndex(ss => new { ss.StudentId, ss.SubjectId }).IsUnique();
        builder.HasIndex(ss => ss.StudentId);
        builder.HasIndex(ss => ss.SubjectId);

        builder.HasOne(ss => ss.Student)
               .WithMany(s => s.StudentSubjects)
               .HasForeignKey(ss => ss.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ss => ss.Subject)
               .WithMany(s => s.StudentSubjects)
               .HasForeignKey(ss => ss.SubjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
