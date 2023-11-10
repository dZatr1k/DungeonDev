using System;
using UnityEngine;
using LevelLogic;
using Units.Heroes;

namespace GameBoard
{
    public class HeroPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _heroParent;
        [SerializeField] private EnergyResourcesSystem _energyResourcesSystem;
        
        private Hero _heroToPlace = null;

        private bool _isLocked = true;

        public static Action<Cell> OnHeroPlaced;

        private void OnEnable()
        {
            Cell.CellClicked += PlaceHero;
            Level.Instance.CurrentStateMachine.OnGameStarted += Relock;
        }

        private void OnDisable()
        {
            Cell.CellClicked -= PlaceHero;
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
                OnHeroPlaced?.Invoke(cell);
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

        public void ResetHero()
        {
            _heroToPlace = null;
        }

        public bool SetHeroToPlace(Hero hero)
        {
            if(hero != null)
            {
                if (_energyResourcesSystem.IsAbleToBuy(hero))
                {
                    _heroToPlace = hero;
                    return true;
                }
            }
            else
            {
                throw new ArgumentException("You are trying assign heroToPlace with null. Use ResetHero instead.");
            }
            return false;
        }
    }

}
