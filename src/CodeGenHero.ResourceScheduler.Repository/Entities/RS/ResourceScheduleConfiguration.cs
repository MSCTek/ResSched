// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.6
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace CodeGenHero.ResourceScheduler.Repository.Entities.RS
{

    // ResourceSchedule
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class ResourceScheduleConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ResourceSchedule>
    {
        public ResourceScheduleConfiguration()
            : this("dbo")
        {
        }

        public ResourceScheduleConfiguration(string schema)
        {
            ToTable("ResourceSchedule", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"ID").HasColumnType("uniqueidentifier").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.ResourceId).HasColumnName(@"ResourceID").HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime2").IsRequired();
            Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.UpdatedDate).HasColumnName(@"UpdatedDate").HasColumnType("datetime2").IsRequired();
            Property(x => x.ReservationStartDateTime).HasColumnName(@"ReservationStartDateTime").HasColumnType("datetime2").IsRequired();
            Property(x => x.ReservationEndDateTime).HasColumnName(@"ReservationEndDateTime").HasColumnType("datetime2").IsRequired();
            Property(x => x.ReservationNotes).HasColumnName(@"ReservationNotes").HasColumnType("nvarchar").IsOptional().HasMaxLength(255);
            Property(x => x.ReservedByUserId).HasColumnName(@"ReservedByUserId").HasColumnType("uniqueidentifier").IsOptional();
            Property(x => x.ReservedForUser).HasColumnName(@"ReservedForUser").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.ReservedOnDateTime).HasColumnName(@"ReservedOnDateTime").HasColumnType("datetime2").IsRequired();

            // Foreign keys
            HasOptional(a => a.User).WithMany(b => b.ResourceSchedules).HasForeignKey(c => c.ReservedByUserId).WillCascadeOnDelete(false); // FK_ResourceSchedule_User
            HasRequired(a => a.Resource).WithMany(b => b.ResourceSchedules).HasForeignKey(c => c.ResourceId).WillCascadeOnDelete(false); // FK_ResourceSchedule_Resource
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>