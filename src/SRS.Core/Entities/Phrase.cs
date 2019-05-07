using SRS.Core.Exceptions;

namespace SRS.Core.Entities
{
    public class Phrase
    {
        public string Term { get; set; }

        public string Translation { get; set; }

        public string Synonym { get; set; }

        public string Meaning { get; set; }

        private Phrase()
        {
        }

        public Phrase(string term, string translation = null, string synonym = null, string meaning = null)
        {
            // todo: use FluentValidation or any other external library if possible
            if(string.IsNullOrEmpty(term))
            {
                throw new InvalidPhraseException($"{nameof(Phrase.Term)} must be provided.");
            }

            if (string.IsNullOrEmpty(translation) && string.IsNullOrEmpty(synonym))
            {
                throw new InvalidPhraseException($"Either {nameof(Phrase.Translation)} or {nameof(Phrase.Synonym)} must be provided.");
            }

            this.Term = term;
            this.Translation = translation;
            this.Synonym = synonym;
            this.Meaning = meaning;
        }
    }
}
