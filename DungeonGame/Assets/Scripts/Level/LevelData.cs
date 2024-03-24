using UnityEngine;
using Waves;

namespace LevelLogic
{
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "Scriptable Object/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _numberOfLevel;
        [SerializeField] private Wave _wave;
        public int NumberOfLevel => _numberOfLevel;
        public Wave Enemies => _wave;
    }
}
