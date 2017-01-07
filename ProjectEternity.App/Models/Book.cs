using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Windows.Storage;

namespace ProjectEternity.App.Models
{
    public enum BookType
    {
        PlainText,
        FictionBook,
        ElectronicPublication
    }

    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }

        public string BookPath { get; set; }
        public string CoverPath { get; set; }
        public string FolderPath { get; set; }

        public BookType Type { get; set; }

        [NotMapped]
        public bool IsImported { get; set; }

        [NotMapped]
        public string CoverImage => Path.Combine(ApplicationData.Current.LocalFolder.Path, CoverPath);

        public DateTime LastOpenedTime { get; set; }
    }
}