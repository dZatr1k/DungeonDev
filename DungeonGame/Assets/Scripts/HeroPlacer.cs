using UnityEngine;
using LevelLogic;
using Units.Heroes;

namespace GameBoard
{
    public class HeroPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _heroParent;
        
        private Hero _heroToPlace = null;

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
            if (cell.IsCorrupted == false && IsSelected())
            {
                Hero hero = Instantiate(_heroToPlace.gameObject, 
                    cell.SpawnPoint.transform.position, 
                    Quaternion.identity, _heroParent.transform)
                    .GetComponent<Hero>();
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

        public void SetHeroToPlace(Hero hero)
        {
            _heroToPlace = hero;
        }

    }

}
