//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WordPuzzle.Classes
{
    class WordEntity
    {
        public string Category { get; set; }
        public string Word { get; set; }
    }

    class Words
    {
        private string PRESET_WORDS = "COUNTRIES|BANGLADESH|GAMBIA|AUSTRALIA|ENGLAND|NEPAL|INDIA|PAKISTAN|TANZANIA|SRILANKA|CHINA|CANADA|JAPAN|BRAZIL|ARGENTINA|" +
                                      "MUSIC|PINKFLOYD|DYLAN|METALLICA|IRONMAIDEN|NOVA|ARTCELL|FEEDBACK|ORTHOHIN|DEFLEPPARD|BEATLES|ADAMS|JACKSON|PARTON|SHAKIRA|" +
                                      "MUSICIANS|BEETHOVEN|VIVALDI|MOZART|BACH|ALBENIZ|STRAUSS|WAGNER|SCHUMANN|HANDEL|BRUCKNER|PALESTRINA|PUCCINI|DEBUSSY|VERDI|" +
                                      "MOVIES|ZOOTOPIA|TERMINATOR|RIO|CHICKENRUN|MADAGASCAR|DUMBO|FROZEN|REVENANT|PATRIOT|BRAVE|DRDOLITTLE|JUMANJI|HOMEALONE|SPIDERMAN|" +
                                      "AUTHORS|TOLSTOY|BUCK|CHEKHOV|PUSHKIN|TURGENEV|GOGOL|KIPLING|CAMUS|DEFOE|SHELLEY|AHMED|DOYLE|VERNE|BRADBURY|" +
                                      "CITIES|DHAKA|KHULNA|RAJSHAHI|BANJUL|LONDON|MANCHESTER|ONTARIO|PARIS|RIO|SYDNEY|GLASGOW|KIAMA|PERTH|ADELAIDE|" +
                                      "ANIMALS|COW|GOAT|GAZELLE|SLOTH|DINOSAUR|BUFFALO|SHEEP|CAMEL|HORSE|CHEETAH|JAGUAR|TIGER|ELEPHANT|BUNNY|" +
                                      "GREEK_MYTH|ZEUS|HERA|POSEIDON|HADES|APHRODITE|APOLLO|ARES|ARTEMIS|DIONYSUS|HERCULES|HERMES|NIKE|IRIS|NEMESIS|" +
                                      "ROMAN_MYTH|JUPITER|JUNO|NEPTUNE|SATURN|VENUS|PLUTO|VULCAN|DIANA|MERCURY|CUPID|CERES|MARS|GAEA|BACCHUS|" + 
                                      "SCIENTISTS|NEWTON|EINSTEIN|ARCHIMEDES|IBNSINA|KHWARIZMI|RAMANUJAN|BOSE|PRAFULLA|KALAM|MAHAVIRA|SALAM|PASTEUR|FLEMING|SALK|" +
                                      "FOOTBALL|MARADONA|MESSI|PELE|ZIDANE|RONALDINHO|ZICO|RONALDO|MULLER|BAGGIO|NEYMAR|CARLOS|PLATINI|MILLA|PUSKAS|" +
                                      "CRICKET|BRADMAN|SHAKIB|GILCHRIST|IMRAN|CROWE|LARA|AKRAM|TENDULKAR|KAPIL|INZAMAM|LLOYD|MASHRAFE|MUSHFIQUR|PONTING";
        private const string FILE_NAME_FOR_STORING_WORDS = "Words.dat";
        private const string ENCRYPTION_PASSWORD = "mambazamba";
        private const int MAX_LENGTH = 10;
        private const int MIN_LENGTH = 3;
        private int MAX_WORDS = Convert.ToInt16(ConfigurationManager.AppSettings["MAX_WORDS"]);

        private List<string> Categories = new List<string>();
        private List<WordEntity> WordsList = new List<WordEntity>();

        public List<string> GetCategories()
        {
            return Categories;
        }

        public List<WordEntity> GetWords()
        {
            return WordsList;
        }

        public Words()
        {
            PopulateCategoriesAndWords();
        }

        public void PopulateCategoriesAndWords()
        {
            try
            {
                if (File.Exists(FILE_NAME_FOR_STORING_WORDS))   // If words file exists, then read it.
                    ReadFromFile();
                else
                {   // Otherwise create the file and populate from there.
                    string EncryptedWords = StringCipher.Encrypt(PRESET_WORDS, ENCRYPTION_PASSWORD);
                    using (StreamWriter OutputFile = new StreamWriter(FILE_NAME_FOR_STORING_WORDS))
                        OutputFile.Write(EncryptedWords);
                    ReadFromFile();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'ReadWordsFromFile' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void ReadFromFile()
        {
            try
            {
                string Str = File.ReadAllText(FILE_NAME_FOR_STORING_WORDS);
                string[] DecryptedWords = StringCipher.Decrypt(Str, ENCRYPTION_PASSWORD).Split('|');
                if (DecryptedWords[0].Equals(""))  // This means the file was tampered.
                {
                    MessageBox.Show("The words file was tampered. Any Categories/Words saved by the player will be lost.");
                    File.Delete(FILE_NAME_FOR_STORING_WORDS);
                    PopulateCategoriesAndWords();   // Circular reference.
                    return;
                }

                string Category = "";

                for (int i = 0; i <= DecryptedWords.GetUpperBound(0); i++)
                {
                    if (i % (MAX_WORDS + 1) == 0)   // Every 15th word is the category name.
                    {
                        Category = DecryptedWords[i];
                        Categories.Add(Category);
                    }
                    else
                    {
                        WordEntity Word = new WordEntity();
                        Word.Category = Category;
                        Word.Word = DecryptedWords[i];
                        WordsList.Add(Word);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'ReadFromFile' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        public bool SavedWordsAndCategoriesSuccessfully(List<string> WordsByThePlayer, out string CategoryNameFromUser)
        {
            CategoryNameFromUser = "";
            try
            {
                string CategoryName = Interaction.InputBox("Please enter a name for the preset: ", "WordPuzzle", "", -1, -1);
                if (!string.IsNullOrEmpty(CategoryName.Trim()))
                    if (Categories.IndexOf(CategoryName.ToUpper()) >= 0)
                    {
                        MessageBox.Show("There is already a preset with the same name. Duplicate names are not allowed.");
                        CategoryNameFromUser = "";
                        return false;
                    }
                    else
                    {
                        if (File.Exists(FILE_NAME_FOR_STORING_WORDS))   // If words file exists, then read it.
                        {
                            File.Delete(FILE_NAME_FOR_STORING_WORDS);   // First delete the file.
                            string PipedWords = PipeThePlayersWords(CategoryName, WordsByThePlayer.ToArray());
                            PRESET_WORDS += PipedWords; // Add the player's list to the existing list, and then store in disk.
                            string EncryptedWords = StringCipher.Encrypt(PRESET_WORDS, ENCRYPTION_PASSWORD);
                            using (StreamWriter OutputFile = new StreamWriter(FILE_NAME_FOR_STORING_WORDS))
                                OutputFile.Write(EncryptedWords);
                            CategoryNameFromUser = CategoryName;
                            return true;
                        }
                    }
                return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'SavePresetSuccess' method of 'LoadWords' form. Error msg: " + Ex.Message);
                return false;
            }
        }

        private string PipeThePlayersWords(string PresetName, string[] WordsByThePlayer)
        {
            try
            {
                StringBuilder SBuilder = new StringBuilder();
                SBuilder.Append("|");
                SBuilder.Append(PresetName.ToUpper());
                SBuilder.Append("|");
                foreach (string Str in WordsByThePlayer)
                {
                    SBuilder.Append(Str);
                    SBuilder.Append("|");
                }
                SBuilder.Remove(SBuilder.Length - 1, 1);    // Remove the last pipe.
                return SBuilder.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'PipeThePlayersWords' method of 'LoadWords' form. Error msg: " + Ex.Message);
                return "";
            }
        }

        public bool CheckUserInputValidity(DataGridView WordsDataGridView, List<string> WordsByThePlayer)
        {
            try
            {
                if (WordsDataGridView.Rows.Count != MAX_WORDS)
                {
                    MessageBox.Show("You need to have " + MAX_WORDS + " words in the list. Please add more.");
                    return false;
                }

                char[] NoLettersList = { ':', ';', '@', '\'', '"', '{', '}', '[', ']', '|', '\\', '<', '>', '?', ',', '.', '/',
                                     '`', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', '~', '!', '#', '$',
                                     '%', '^', '&', '*', '(', ')', '_', '+'};
                foreach (DataGridViewRow Itm in WordsDataGridView.Rows)
                {
                    if (Itm.Cells[0].Value == null) continue;
                    if (Itm.Cells[0].Value.ToString().IndexOfAny(NoLettersList) >= 0)
                    {
                        MessageBox.Show("Should only contain letters. The word that contains something else other than letters is: '" + Itm.Cells[0].Value.ToString() + "'");
                        return false;
                    }
                    if (WordsByThePlayer.IndexOf(Itm.Cells[0].Value.ToString()) != -1)
                    {
                        MessageBox.Show("Can't have duplicate word in the list. The duplicate word is: '" + Itm.Cells[0].Value.ToString() + "'");
                        return false;
                    }
                    WordsByThePlayer.Add(Itm.Cells[0].Value.ToString());
                }

                for (int i = 0; i < WordsByThePlayer.Count - 1; i++)    // For every word in the list check the minimum length; it should be at least 3 characters long.
                    if (WordsByThePlayer[i].Length <3)
                        {
                            MessageBox.Show("Words must be at least 3 characters long. A word '" + WordsByThePlayer[i]  + "' is encountered having less than the acceptable length.'");
                            return false;
                        }

                for (int i = 0; i < WordsByThePlayer.Count - 1; i++)    // For every word in the list.
                {
                    string str = WordsByThePlayer[i];
                    for (int j = i + 1; j < WordsByThePlayer.Count; j++)    // Check existence with every other word starting from the next word
                        if (str.IndexOf(WordsByThePlayer[j]) != -1)
                        {
                            MessageBox.Show("Can't have a word as a sub-part of another word. Such words are: '" + WordsByThePlayer[i] + "' and '" + WordsByThePlayer[j] + "'");
                            return false;
                        }
                }
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'CheckUserInputValidity' method of 'LoadWords' form. Error msg: " + Ex.Message);
                return false;
            }
        }

        public void KeyPressValidity(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                //if (e.KeyChar == (char)Keys.Enter)
                //{
                //    if (tb.Text.Length <= MIN_LENGTH)   // Checking length
                //    {
                //        MessageBox.Show("Words should be at least " + MAX_LENGTH + " characters long.");
                //        e.Handled = true;
                //        return;
                //    }
                //}
                if (tb.Text.Length >= MAX_LENGTH)   // Checking max length
                {
                    MessageBox.Show("Word length cannot be more than " + MAX_LENGTH + ".");
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar.Equals(' '))  // Checking space; no space allowed. Other invalid characters check can be put here instead of the final check on save button click.
                {
                    MessageBox.Show("No space, please.");
                    e.Handled = true;
                    return;
                }
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'Control_KeyPress' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }
    }
}