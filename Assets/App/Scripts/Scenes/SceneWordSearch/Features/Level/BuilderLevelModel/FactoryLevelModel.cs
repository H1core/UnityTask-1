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
            //Алгоритм делался через словари , т.к. мы получаем доступ к количеству букв за 0(1), однако в конце присутсвует обработка словаря за O(n)  
            //Если бы я использовал List<char> то каждый раз чтобы обновить результат мне нужно было бы пересчитывать кол-во букв , в худшем случае O(n) каждый раз в цикле
            Dictionary<char,int> globalLetterCount = new Dictionary<char, int>();

            foreach (var word in words)
            {
                Dictionary<char, int> keyValuePairs = new Dictionary<char, int>();
                for(int i = 0; i < word.Length; i++)
                {
                    if (keyValuePairs.ContainsKey(word[i]))
                    {
                        keyValuePairs[word[i]]++;
                        globalLetterCount[word[i]] = UnityEngine.Mathf.Max(globalLetterCount[word[i]], keyValuePairs[word[i]]);
                    }
                    else
                    {
                        keyValuePairs.Add(word[i],1);
                        if (!globalLetterCount.ContainsKey(word[i]))
                        {
                            globalLetterCount.Add(word[i],1);
                        }
                        globalLetterCount[word[i]] = UnityEngine.Mathf.Max(globalLetterCount[word[i]], keyValuePairs[word[i]]);
                    }
                }
            }
            List<char> result = new List<char>();
            foreach(var key in globalLetterCount.Keys)
            {
                for(int i = 0; i < globalLetterCount[key]; i++)
                {
                    result.Add(key);
                }
            }
            return result;
        }
    }
}