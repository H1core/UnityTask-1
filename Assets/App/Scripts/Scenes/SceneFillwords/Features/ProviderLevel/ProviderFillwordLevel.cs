using System;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        private int GetNearestSquare(int N)
        {
            int i;
            for (i = 1; i * i < N; i++) { }
            return i;
        }
        public GridFillWords LoadModel(int index)
        {
            char[] loadedField = new char[1000];
            int maximalIndex = 0;

            var levelData = Resources.Load<TextAsset>("Fillwords/pack_0").ToString().Split('\n')[index-1].Split(' ');
            var wordDictionary = Resources.Load<TextAsset>("Fillwords/words_list").ToString().Split('\n');

            for(int i = 0; i < levelData.Length; i+=2)
            {
                string currentWord = wordDictionary[Convert.ToInt32(levelData[i])];
                int wordIndex = 0;
                foreach (var letter in levelData[i + 1].Split(';'))
                {
                    if (loadedField[Convert.ToInt32(letter)] != 0)
                        return null;
                    loadedField[Convert.ToInt32(letter)] = currentWord[wordIndex];
                    maximalIndex = Mathf.Max(maximalIndex, Convert.ToInt32(letter));
                    wordIndex++;
                }
            }

            for(int i = 0; i < maximalIndex; i++)
            {
                if(loadedField[i] == 0)
                {
                    return null;
                }
            }

            int nearestSize = GetNearestSquare(maximalIndex);
            if (maximalIndex + 1 != nearestSize * nearestSize)
                return null;

            GridFillWords result = new GridFillWords(new Vector2Int(nearestSize, nearestSize));
            for(int i = 0; i <= maximalIndex; i++)
                result.Set(i / nearestSize, i % nearestSize, new CharGridModel(loadedField[i]));
            return result;
        }
    }
}