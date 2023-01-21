using Application.IRepositories;
using Entities.Entities;
using Entities.Exceptions;
using Models;

namespace FakePersistence
{
    public class SynonymsRepository : ISynonymsRepository
    {
        // I made this public for test purposes 
        public List<Word> _synonyms { get; set; }

        public SynonymsRepository()
        {
            _synonyms = new List<Word>();
        }


        private Word AddWord(string word)
        {
            Word item = new Word
            {
                Text = word,
                Synonyms = new HashSet<Word>(),
                ParentSynonyms = new HashSet<Word>(),

            };
            _synonyms.Add(item);
            return item;
        }

        public void AddSynonym(string word, string synonym)
        {
            var existWord = FindSynonym(word);
            if (existWord == null)
            {
                existWord = new WordNodeIndex
                {
                    Word = AddWord(word),
                    NodeIndex = _synonyms.Count - 1
                };
            }

            if (AreWordsAlreadySynonyms(synonym, existWord.Word))
            {
                throw new DomainDuplicationException($"{synonym} is already a synonym to {word}");
            }

            var existSynonym = FindSynonym(synonym);
            if (existSynonym == null)
            {
                var newWord = new Word
                {
                    Text = synonym,
                    ParentSynonyms = new HashSet<Word>(),
                    Synonyms = new HashSet<Word>()
                };
                newWord.ParentSynonyms.Add(existWord.Word);
                existWord.Word.Synonyms.Add(newWord);
                return;
            }
            
            if (existSynonym.NodeIndex != existWord.NodeIndex)
            {
                _synonyms.RemoveAt(existSynonym.NodeIndex);
            }
            existSynonym.Word.ParentSynonyms.Add(existWord.Word);
            existWord.Word.Synonyms.Add(existSynonym.Word);

        }

        public IEnumerable<SynonymsDto> GetSynonyms(string word)
        {
            var existWord = FindSynonym(word);
            if (existWord == null)
            {
                return new List<SynonymsDto>();
            }

            var synonymsLevels = new HashSet<SynonymsDto>();
            FillSynonymsWords(word, synonymsLevels, existWord.Word, 0);
            IEnumerable<SynonymsDto> synonyms = synonymsLevels.OrderBy(s => s.Level);
            return synonyms;
        }

        public void RemoveAll()
        {
            _synonyms = new List<Word>();
        }

        public void FillSynonymsWords(string firstWord, HashSet<SynonymsDto> synonyms, Word word, int level)
        {
            List<Word> childrenToSearch = new List<Word>();
            List<Word> parentsToSearch = new List<Word>();
            foreach (var child in word.Synonyms)
            {
                if (synonyms.Any(s => s.Text == child.Text) || child.Text == firstWord)
                {
                    continue;
                }
                synonyms.Add(new SynonymsDto
                {
                    Level = level,
                    Text = child.Text
                });
                childrenToSearch.Add(child);
            }

            foreach (var parentSynonym in word.ParentSynonyms)
            {
                if (synonyms.Any(s => s.Text == parentSynonym.Text) || parentSynonym.Text == firstWord)
                {
                    continue;
                }

                synonyms.Add(new SynonymsDto
                {
                    Level = level,
                    Text = parentSynonym.Text
                });
                parentsToSearch.Add(parentSynonym);
            }

            foreach (var child in childrenToSearch)
            {
                FillSynonymsWords(firstWord, synonyms, child, level + 1);
            }
            foreach (var parent in parentsToSearch)
            {
                FillSynonymsWords(firstWord, synonyms, parent, level + 1);
            }
        }

        private static bool AreWordsAlreadySynonyms(string synonym, Word existWord)
        {
            return existWord.Synonyms.Any(s => s.Text == synonym) || existWord.ParentSynonyms.Any(s => s.Text == synonym);
        }


        private WordNodeIndex? FindSynonym(string word)
        {
            for (int i = 0; i < _synonyms.Count; i++)
            {
                var existSynonym = GetSynonym(word, _synonyms[i],new List<string>());
                if (existSynonym != null)
                {
                    return new WordNodeIndex
                    {
                        NodeIndex = i,
                        Word = existSynonym
                    };
                }
            }
            return null;
        }

        private Word? GetSynonym(string text, Word word,List<string> alreadyChecked)
        {
            alreadyChecked.Add(word.Text);
            if (word.Text == text)
            {
                return word;
            }

            foreach (var synonym in word.ParentSynonyms)
            {
                if (!alreadyChecked.Contains(synonym.Text) )
                {
                   
                    var existSynonym =  GetSynonym(text, synonym,alreadyChecked);
                    if (existSynonym != null)
                    {
                        return existSynonym;
                    }
                }
            }


            foreach (var synonym in word.Synonyms)
            {
                if (!alreadyChecked.Contains(synonym.Text))
                {
                    var existSynonym =  GetSynonym(text, synonym,alreadyChecked);
                    if (existSynonym != null)
                    {
                        return existSynonym;
                    }
                }
            }

            return null;
        }
    }
    

    public class WordNodeIndex
    {
        public int NodeIndex { get; set; }

        public Word Word { get; set; }
    }
}
