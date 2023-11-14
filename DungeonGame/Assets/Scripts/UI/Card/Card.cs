using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Units.Heroes;
using TMPro;
using UnityEngine.UI;

namespace Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<Card> OnClick;
        
        [SerializeField] private int _id;
        [SerializeField] private Hero _hero;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _heroView;
        [SerializeField] private CardView _cardView;

        public CardView CardView => _cardView;
        public Hero Hero => _hero;

        private void Start()
        {
            _text.text = _hero.Cost.ToString();
            _heroView.sprite = _hero.GetComponent<SpriteRenderer>().sprite;
        }

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if(_id >= 0)
                    _id = value;
                else
                    Debug.LogError("ID must be non-negative");
            }
        }

        public void Activate()
        {
            GetComponent<GraphicRaycaster>().enabled = true;
        }

        public void Deactivate()
        {
            GetComponent<GraphicRaycaster>().enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}