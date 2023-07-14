using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp13
{
    [Serializable]
        public class Journal//Завдання 2-3
        {
            public string Title { get; set; }
            public string Publisher { get; set; }
            public DateTime PublicationDate { get; set; }
            public int PageCount { get; set; }
            public List<Article> Articles { get; set; } // Список статей

            public void InputJournalInfo()//Ввід інформації про журнал
            {
                Console.WriteLine("Enter the name of the Journal:");
                Title = Console.ReadLine();
                Console.WriteLine("Enter the name of the publisher:");
                Publisher = Console.ReadLine();
                Console.WriteLine("Enter the publication date (in dd.mm.yyyy format):");
                string publicationDateStr = Console.ReadLine();
                PublicationDate = DateTime.ParseExact(publicationDateStr, "dd.MM.yyyy", null);
                Console.WriteLine("Enter the number of pages:");
                string pageCountStr = Console.ReadLine();
                PageCount = int.Parse(pageCountStr);
                Console.WriteLine("\nAdding articles to the journal:");
                Articles = new List<Article>();
                int articleCount;
                do
                {
                    Article article = new Article();
                    Console.WriteLine("Enter the title of the article:");
                    article.Title = Console.ReadLine();
                    Console.WriteLine("Enter the number of characters in the article:");
                    string charCountStr = Console.ReadLine();
                    article.CharacterCount = int.Parse(charCountStr);
                    Console.WriteLine("Enter the article summary:");
                    article.Summary = Console.ReadLine();
                    Articles.Add(article);
                    Console.WriteLine("Do you want to add another article? (Enter the number of articles to add or 0 to finish)");
                    string articleCountStr = Console.ReadLine();
                    articleCount = int.Parse(articleCountStr);
                }
                while (articleCount > 0);
            }
            public void OutputJournalInfo()//Виввід
            {
                Console.WriteLine("Name: {0}", Title);
                Console.WriteLine("Publisher: {0}", Publisher);
                Console.WriteLine("Publication date: {0}", PublicationDate.ToShortDateString());
                Console.WriteLine("Pages: {0}", PageCount);

                Console.WriteLine("\nList of articles:");
                foreach (var article in Articles)
                {
                    Console.WriteLine("Title: {0}", article.Title);
                    Console.WriteLine("Character Count: {0}", article.CharacterCount);
                    Console.WriteLine("Summary: {0}", article.Summary);
                    Console.WriteLine();
                }
            }
            public string SerializeJournal()//Серіалізація журналу
        {
                XmlSerializer serializer = new XmlSerializer(typeof(Journal));
                StringWriter stringWriter = new StringWriter();
                serializer.Serialize(stringWriter, this);
                return stringWriter.ToString();
            }
            public static Journal DeserializeJournal(string serializedJournal)//десеріалізація журналу 
        {
                XmlSerializer serializer = new XmlSerializer(typeof(Journal));
                StringReader stringReader = new StringReader(serializedJournal);
                Journal journal = (Journal)serializer.Deserialize(stringReader);
                return journal;
            }
            public static Journal LoadSerializedJournalFromFile(string filePath)
            {
                string serializedJournal = File.ReadAllText(filePath);
                Journal journal = DeserializeJournal(serializedJournal);
                Console.WriteLine("File downloaded.");
                return journal;
            }
            public void SaveSerializedJournalToFile(string filePath)//Створення та збереження у файл назва файлу береться автоматично з назви журналу
            {
            //C:\Users\Admin\Desktop\test
            string serializedJournal = SerializeJournal();
                File.WriteAllText(filePath + ($@"\{Title}"), serializedJournal);
                Console.WriteLine("File saved successfully.");
            }
            public static void AddJournal()//Метод який включає виклик всіх необхідних методів для повного тесну 
            {
                Journal journal = new Journal();
                Console.WriteLine("1. Enter information about the journal.");
                journal.InputJournalInfo();
                Console.WriteLine("\n2. Display information about the journal.");
                journal.OutputJournalInfo();
                Console.WriteLine("\n3. Journal serialization.");
                string serializedJournal = journal.SerializeJournal();
                Console.WriteLine("\n4. Saving the serialized journal to a file.");
                Console.WriteLine("Enter the path to save the file:");
                string filePath = Console.ReadLine();
                journal.SaveSerializedJournalToFile(filePath);
                Console.WriteLine("\n5. Loading a serialized journal from a file and deserializing.");
                Console.WriteLine("Enter the path to the file to download:");
                string loadedFilePath = Console.ReadLine();
                Journal loadedJournal = Journal.LoadSerializedJournalFromFile(loadedFilePath);
                Console.WriteLine("\nDownloaded journal:");
                loadedJournal.OutputJournalInfo();
            }
        }
        public class Article
        {
            public string Title { get; set; }
            public int CharacterCount { get; set; }
            public string Summary { get; set; }
        }
}