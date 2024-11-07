using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplicationBack.Model;

namespace WebApplicationBack.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Model.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
                b.Property<string>("Email")
                    .HasColumnType("text");
                b.Property<string>("Password")
                    .HasColumnType("text");
                b.Property<string>("Name")
                    .HasColumnType("text");
                b.Property<string>("Surname")
                   .HasColumnType("text");
                b.Property<DateTime>("DateOfBirth")
                    .HasColumnType("timestamp without time zone");
                b.Property<int>("UserType")
                    .HasColumnType("integer");
                b.Property<string>("Token")
                    .HasColumnType("text");
                b.Property<byte[]>("Image")
                      .HasColumnType("bytea");
                b.HasKey("Id");
                b.ToTable("Users");
                b.HasData(
                    new
                    {
                        Id = 1,
                        Email = "admin@gmail.com",
                        Password = "admin",
                        Name = "Miki",
                        Surname = "Mikic",
                        DateOfBirth = new DateTime(1998, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        UserType = 1,
                        Image = new byte[0]
                    });
            });
#pragma warning restore 612, 618
        }
    }
}
