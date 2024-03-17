using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Units.Heroes;
using UnityEngine.UI;

namespace Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<Card> OnClicked;
        
        [SerializeField] private int _id;
        [SerializeField] private CardSettings _heroCardSettings;
        [SerializeField] private CardView _cardView;

        public CardView CardView => _cardView;
        public Hero Hero => _heroCardSettings.Hero;

        public CardSettings CardSettings => _heroCardSettings;



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
            OnClicked?.Invoke(this);
        }
    }
}