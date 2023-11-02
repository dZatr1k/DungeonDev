using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameBoard
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        private GameObject _hero;

        public GameObject Hero => _hero;

        public static UnityAction<Cell> CellClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            CellClicked?.Invoke(this);
        }

        public void SetHero(GameObject hero)
        {
            _hero = hero;
        }

        private void RemoveHero()
        {
            _hero = null;
        }

    }

}
