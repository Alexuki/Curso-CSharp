using System.ComponentModel.DataAnnotations;

public class PdfFile
{
    [Required]
    public string FileName { get; set; }
    [Required]
    public string Text { get; set; }
}