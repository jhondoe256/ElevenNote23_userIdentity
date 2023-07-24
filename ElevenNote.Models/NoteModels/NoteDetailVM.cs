namespace ElevenNote.Models.NoteModels
{
    public class NoteDetailVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string CatetoryName { get; set; } = string.Empty;

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}