using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameBoard
{
    public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _spawnPoint;

        private SpriteRenderer _renderer;
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

        private GameObject _hero;

        public SpriteRenderer Renderer => _renderer;

        public GameObject Hero => _hero;
        public GameObject SpawnPoint => _spawnPoint;

        public static UnityAction<Cell> OnCellClick;

        private void OnValidate()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnCellClick?.Invoke(this);
        }

        public void SetHero(GameObject hero)
        {
            _hero = hero;
        }

        private void RemoveHero()
        {
            _hero = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponentInParent<Field>().Fog(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GetComponentInParent<Field>().Defog(this);
        }
    }

}
