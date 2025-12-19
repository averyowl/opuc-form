namespace OpucForm.Shared;

// Data Transfer Object for file name, content, and data
public class UploadedDocumentDto
{
    public string FileName { get; set; } = "";
    public string ContentType { get; set; } = "";
    public byte[] Data { get; set; } = Array.Empty<byte>();
}