using Xunit;
using System.Reflection;
using System.Collections.Generic;
using System;
using Lab2;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void AddNewWordToDictionary()
        {
            // Arrange
            ConsoleUI consoleUI = new ConsoleUI();
            string newWord = "testword";

            // Act
            consoleUI.ReadNewWord();
            consoleUI.Check(newWord);

            // Assert
            Word addedWord = GetPrivateField<Word>(consoleUI, "same_word");
            Assert.NotNull(addedWord);
            Assert.Equal("testword", addedWord.FullWord.ToLower());
        }

        [Fact]
        public void PrintCognateWords()
        {
            // Arrange
            ConsoleUI consoleUI = new ConsoleUI();
            Word existingWord = new Word("pre", "root", "suf", "end");
            GetPrivateField<Dictionary>(consoleUI, "dict").AddWord(existingWord);

            // Act
            consoleUI.PrintCognate(existingWord);

            // Assert
            Assert.Contains(existingWord, GetPrivateField<Dictionary>(consoleUI, "dict").Words);
        }

        [Fact]
        public void CheckUnknownWord()
        {
            // Arrange
            ConsoleUI consoleUI = new ConsoleUI();
            string unknownWord = "unknownword";

            // Act
            consoleUI.ReadWord();

            // Assert
            Assert.Null(GetPrivateField<Dictionary>(consoleUI, "dict").GetWordByText(unknownWord));
        }

        private T GetPrivateField<T>(object obj, string fieldName)
        {
            var fieldInfo = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)fieldInfo.GetValue(obj);
        }
    }
}