using UnityEngine;
using UnityEngine.UI;

namespace Card 
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image _haze;

        private void OnEnable()
        {
            _haze.enabled = false;
        }

        public void EnableHaze()
        {
            _haze.enabled = true;
        }

        public void DisableHaze()
        {
            _haze.enabled = false;
        }
    }
}