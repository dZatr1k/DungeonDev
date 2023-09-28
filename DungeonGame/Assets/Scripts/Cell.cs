using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Field
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        private GameObject _hero;

        public GameObject Hero => _hero;

        public static UnityAction<Cell> OnCellClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click");
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;  // ��� ����� ������ � Start,
                                                                           // �� � ���������� ��� ������� ������ ���������
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

    }

}
