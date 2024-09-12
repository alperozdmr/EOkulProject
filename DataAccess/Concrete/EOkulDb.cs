using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EOkulDb :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-AMHHP22\\MSSQLSERVER01;Initial Catalog=EOkulDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Teacher>? Teachers { get; set; }
        public DbSet<OperationClaim>? OperationClaims { get; set; }
        public DbSet<StudentOperationClaim>? StudentOperationClaims { get; set; }
        public DbSet<TeacherOperationClaim>? TeacherOperationClaims { get; set; }
        public DbSet<StudentNote>? StudentNotes { get; set; }
        public DbSet<StudentClass>? StudentsClasses { get; set; }


        // BAĞIMLILIKLAR YANLIŞ
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Student>()
        //        .HasOne(x => x.StudentClass)
        //        .WithMany(y => y.Students)
        //        .HasForeignKey(z => z.Id)
        //        .OnDelete(DeleteBehavior.ClientSetNull);
        //    builder.Entity<Student>()
        //        .HasMany(x => x.StudentNote)
        //        .WithOne(y => y.Student)
        //        .HasForeignKey(y => y.StudentId);

        //    base.OnModelCreating(builder);
        //}
    }
}
