using Microsoft.VisualBasic;
using TaskyRevamp.Dto.Enums;
using System.ComponentModel.DataAnnotations;

namespace TaskyRevamp.Dto.Files
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileBase64 { get; set; }
        public string Extention { get; set; }

    }
}
