using UnityEngine;

namespace LevelLogic
{
    [CreateAssetMenu(fileName = "New LevelData", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _numberOfLevel;
        [SerializeField]
        private Object[] _enemies; //я не знаю как мы потом реализуем наполнение врагами,
                                   //через Enum или GameObject или вообще через Dictionary, так что оставлю пока просто Object
        public int NumberOfLevel => _numberOfLevel;
        public Object[] Enemies => _enemies;
    }
}
