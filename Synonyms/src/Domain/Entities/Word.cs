namespace Entities.Entities
{
    public class Word
    {
        public HashSet<Word> ParentSynonyms { get; set; }= new HashSet<Word>();

        public string Text { get; set; } = "";

        public HashSet<Word> Synonyms { get; set; } = new HashSet<Word>();
    }
}
