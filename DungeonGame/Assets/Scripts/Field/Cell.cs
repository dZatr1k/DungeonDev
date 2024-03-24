using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Units.Heroes;
using Units;
using System.Collections.Generic;

namespace GameBoard
{
    public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private const string ENEMY_TAG_NAME = "Enemy";

        [SerializeField] private GameObject _spawnPoint;

        private SpriteRenderer _renderer;
        private Collider2D _collider;
        private int _lineNumber;
        private int _index;

        public int LineNumber
        {
            get
            {
                return _lineNumber;
            }
            set
            {
                if (value < 0)
                {
                    Debug.LogError("You are trying set negative line number to Cell");
                    return;
                }
                _lineNumber = value;
            }
        }

        public int Index
        {
            get 
            {
                return _index;
            }
            set
            {
                if (value < 0)
                {
                    Debug.LogError("You are trying set negative index to Cell");
                    return;
                }
                _index = value;
            }
        }

        private Hero _hero;

        public SpriteRenderer Renderer => _renderer;
        public Hero Hero => _hero;
        public GameObject SpawnPoint => _spawnPoint;

        public static UnityAction<Cell> OnCellClicked;

        private void OnValidate()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnCellClicked?.Invoke(this);
        }

        public void SetHero(Hero hero)
        {
            _hero = hero;
            _hero.OnUnitDied += RemoveHero;
        }

        private void RemoveHero(Unit unit)
        {
            _hero.OnUnitDied -= RemoveHero;
            _hero = null;
        }

        public bool IsCellFree()
        {
            var colliders = GetCollidersWithTag(
                Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.bounds.size, 0f), ENEMY_TAG_NAME);

            if (_hero == null && colliders.Count == 0)
                return true;
            return false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponentInParent<Field>().Fog(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GetComponentInParent<Field>().Defog(this);
        }

        private List<Collider2D> GetCollidersWithTag(Collider2D[] colliders, string tag)
        {
            var collidersWithTag = new List<Collider2D>();
            foreach (Collider2D collider in colliders)
                if (collider.CompareTag(tag))
                    collidersWithTag.Add(collider);

            return collidersWithTag;
        }
    }
}

