using System;
using UnityEngine;
using LevelLogic;
using Units.Heroes;
using ObjectPool;

namespace GameBoard
{
    public class HeroPlacer : MonoBehaviour
    {
        [SerializeField] private PoolsCatalog _poolsCatalog;
        [SerializeField] private EnergyResourcesSystem _energyResourcesSystem;
        
        private Hero _heroToPlace = null;

        private bool _isLocked = true;

        public static event Action<Cell> OnHeroPlaced;

        private void OnEnable()
        {
            Cell.OnCellClicked += PlaceHero;
            Level.Instance.CurrentStateMachine.OnGameStarted += Relock;
        }

        private void OnDisable()
        {
            Cell.OnCellClicked -= PlaceHero;
            Level.Instance.CurrentStateMachine.OnGameStarted -= Relock;
        }

        private void PlaceHero(Cell cell)
        {
            if (_isLocked)
                return;
            if (cell.IsCorrupted == false && IsSelected())
            {
                Hero hero = _poolsCatalog.GetPool(_heroToPlace).Get().GetComponent<Hero>();
                hero.transform.position = cell.SpawnPoint.transform.position;
                cell.SetHero(hero);
                hero.SetObserveArea();
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

        public bool SetHeroToPlace(Card.Card card)
        {
            if(card.Hero != null)
            {
                if (_energyResourcesSystem.IsAbleToBuy(card.Hero))
                {
                    _heroToPlace = card.Hero;
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
