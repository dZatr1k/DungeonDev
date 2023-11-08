using UnityEngine;

namespace Card
{
    public class CardsPanel : MonoBehaviour
    {
        [SerializeField] protected CardPlace[] _places;

        protected void OnValidate()
        {
            _places ??= GetComponentsInChildren<CardPlace>();
        }
    }
}