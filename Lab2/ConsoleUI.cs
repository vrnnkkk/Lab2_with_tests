using Lab2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab2
{
    public class ConsoleUI
    {
        Word same_word;
        Dictionary dict = new Dictionary();

        public void Begin()
        {
            do
            {
                ReadWord();
            } while (true);
        }

        public void ReadWord()
        {
            Console.Write("Введите слово: ");
            String new_word = Console.ReadLine().ToLower();
            if (new_word.Equals("q"))
            {
                Environment.Exit(0);
            }

            if (string.IsNullOrEmpty(new_word))
            {
                Console.WriteLine("Вы не ввели слово :(");
                Console.WriteLine("");
                ReadWord();
            }

            var word = dict.GetWordByText(new_word);

            if (word != null)
            {
                Console.WriteLine("Это слово есть в словаре. Список слов с таким корнем:");
                PrintCognate(word);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Неизвестное слово. Хотите добавить его в словарь? Введите 'y', если да, и 'n', если нет :) ");
                String answer = Console.ReadLine().ToLower();
                if (answer.Equals("y"))
                {
                    ReadNewWord();
                    Check(new_word);
                }
                else if (answer.Equals("n"))
                {
                    ReadWord();
                }
                else
                {
                    Console.WriteLine("Неккоректный ответ :(");
                    ReadWord();
                }
            }
        }

        public void ReadNewWord()
        {
            Console.Write("Введите префикс: ");
            string prefix = Console.ReadLine().ToLower();

            Console.Write("Введите корень: ");
            string root = Console.ReadLine().ToLower();

            Console.Write("Введите суффиксы: ");
            string suffix = Console.ReadLine().ToLower();

            Console.Write("Введите окончание: ");
            string ending = Console.ReadLine().ToLower();

            same_word = new Word(prefix, root, suffix, ending);
        }

        public void Check(String new_word)
        {
            if (same_word.CompareWord(new_word))
            {
                dict.AddWord(same_word);

                Console.WriteLine($"Слово '{same_word.FullWord}' успешно добавлено в словарь.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Слова не совпали. Попробуйте снова.");
                ReadNewWord();
                Check(new_word);
            }
        }

        public void PrintCognate(Word enteredWord)
        {
            foreach (Word existingWord in dict.Words)
            {
                if (existingWord.HasTheSameRoot(enteredWord))
                {
                    PrintWord(existingWord);
                }
            }
        }

        public void PrintWord(Word w)
        {
            Console.WriteLine(w.SplittedWord);
        }
    }
}