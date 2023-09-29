using System;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;
namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        const int maxField = 1000;
        static string[] wordDictionary = null;
        static string[] levelsData = null;
        private static int previousIndex = 0;
        private void InitData()
        {
            if (levelsData == null)
                levelsData = Resources.Load<TextAsset>("Fillwords/pack_0").ToString().Split('\n');
            if (wordDictionary == null)
                wordDictionary = Resources.Load<TextAsset>("Fillwords/words_list").ToString().Split('\n');
        }
        private bool checkForIssues(ref char[] field, ref int maxIndex , ref int nearestSize)
        {
            for (int i = 0; i < maxIndex; i++)
            {
                if (field[i] == 0)
                    return false;
            }
            nearestSize = System.Convert.ToInt32(Mathf.Sqrt(maxIndex));
            if (maxIndex + 1 != nearestSize * nearestSize)
                return false;

            return true;
        }

        public GridFillWords LoadModel(int index)
        {
            int direction = index - previousIndex;
            char[] loadedField = new char[maxField];

            InitData();

            var levelData = levelsData[index - 1].Split(' ');
            int maximalIndex = 0 , nearestSize = 0;

            for (int i = 0; i < levelData.Length; i+=2)
            {
                string currentWord = wordDictionary[Convert.ToInt32(levelData[i])];
                int wordIndex = 0;
                foreach (var letter in levelData[i + 1].Split(';'))
                {
                    if (loadedField[Convert.ToInt32(letter)] != 0)
                    {
                        previousIndex = index;
                        return LoadModel(Mathf.Max((index + direction) % levelsData.Length, 0));
                    }
                    loadedField[Convert.ToInt32(letter)] = currentWord[wordIndex];
                    maximalIndex = Mathf.Max(maximalIndex, Convert.ToInt32(letter));
                    wordIndex++;
                }
            }
            if (!checkForIssues(ref loadedField, ref maximalIndex, ref nearestSize)) {
                previousIndex = index;
                return LoadModel(Mathf.Max((index + direction) % levelsData.Length,0));
            }

            previousIndex = index;
            GridFillWords result = new GridFillWords(new Vector2Int(nearestSize, nearestSize));
            for(int i = 0; i <= maximalIndex; i++)
                result.Set(i / nearestSize, i % nearestSize, new CharGridModel(loadedField[i]));
            return result;
        }
    }
}