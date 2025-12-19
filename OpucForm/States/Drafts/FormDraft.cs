namespace OpucForm.States.Drafts
{
    public sealed class FormDraft
    {
        public int Id { get; set; }
        public string? TextValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

