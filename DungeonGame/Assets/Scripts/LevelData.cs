using UnityEngine;

namespace LevelLogic
{
    [CreateAssetMenu(fileName = "New LevelData", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _numberOfLevel;
        [SerializeField]
        private Object[] _enemies; //� �� ���� ��� �� ����� ��������� ���������� �������,
                                   //����� Enum ��� GameObject ��� ������ ����� Dictionary, ��� ��� ������� ���� ������ Object
        public int NumberOfLevel => _numberOfLevel;
        public Object[] Enemies => _enemies;
    }
}
