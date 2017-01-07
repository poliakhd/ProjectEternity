using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectEternity.App.Core;
using ProjectEternity.App.Models;

namespace ProjectEternity.App.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170103134816_BookType")]
    partial class BookType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("ProjectEternity.App.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("BookPath");

                    b.Property<string>("CoverPath");

                    b.Property<string>("FolderPath");

                    b.Property<bool>("IsImported");

                    b.Property<DateTime>("LastOpenedTime");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });
        }
    }
}
