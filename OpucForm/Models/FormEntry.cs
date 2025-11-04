using System;

namespace OpucForm.Models
{
    public class FormEntry
    {
        public int Id { get; set; }
        public string? TextValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
