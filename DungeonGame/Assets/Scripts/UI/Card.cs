using System;
using UnityEngine;
using UnityEngine.EventSystems;
using LevelLogic;

namespace Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<Card> OnClick;
        
        [SerializeField] private int _id;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}