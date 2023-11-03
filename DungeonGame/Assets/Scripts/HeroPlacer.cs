using UnityEngine;
using LevelLogic;

namespace GameBoard
{
    public class HeroPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _heroParent;
        [SerializeField] private GameObject _heroToPlace;

        private bool _isLocked = true;

        private void OnEnable()
        {
            Cell.OnCellClick += PlaceHero;
            Level.Instance.CurrentStateMachine.OnGameStarted += Relock;
        }

        private void OnDisable()
        {
            Cell.OnCellClick -= PlaceHero;
            Level.Instance.CurrentStateMachine.OnGameStarted -= Relock;
        }

        private void PlaceHero(Cell cell)
        {
            if (_isLocked)
                return;
            if (cell.Hero == null && _heroToPlace != null)
            {
                GameObject hero = Instantiate(_heroToPlace, cell.SpawnPoint.transform.position, 
                    Quaternion.identity, _heroParent.transform);
                cell.SetHero(hero);
            }
        }

        private void Relock()
        {
            _isLocked = !_isLocked;
        }

        public bool IsSelected()
        {
            return _heroToPlace != null;
        }

    }

}
