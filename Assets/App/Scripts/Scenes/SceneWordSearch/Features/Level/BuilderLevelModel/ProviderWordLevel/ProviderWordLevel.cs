using System;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using System.Collections.Generic;
using UnityEngine;
namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        public LevelInfo LoadLevelData(int levelIndex)
        {
            LevelInfo result = new LevelInfo();
            result.words = JsonUtility.FromJson<ProvideWordLevelStruct>(Resources.Load<TextAsset>($"WordSearch/Levels/{levelIndex}").text).words;
            return result;
        }
    }
}