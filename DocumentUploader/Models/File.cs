using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentUploader.Models
{
    public class File
    {

        public int FileId { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; }

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }

    }
}
