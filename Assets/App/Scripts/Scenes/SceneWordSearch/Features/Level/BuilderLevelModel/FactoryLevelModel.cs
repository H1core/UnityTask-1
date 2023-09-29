using System;
using System.Collections.Generic;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            List<char> result = new List<char>();

            foreach (var word in words)
            {
                List<char> wordLetters = new List<char>();
                for(int i = 0; i < word.Length; i++)
                {
                    wordLetters.Add(word[i]);
                }
                foreach(var letter in wordLetters)
                {
                    if(result.FindAll(obj => obj == letter).Count < wordLetters.FindAll(obj => obj == letter).Count)
                    {
                        result.Add(letter);
                    }
                }
            }
            return result;
        }
    }
}