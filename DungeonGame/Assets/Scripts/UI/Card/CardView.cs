using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [Space]
        [SerializeField] private TextMeshProUGUI _costLabel;
        [SerializeField] private Image _heroView;
        [Space]
        [SerializeField] private Image _haze;
        [SerializeField] private Image _hazeToAnimate;
        [SerializeField] private Image _error;
        [SerializeField] private float _errorTime;
        [SerializeField] private int _loopCount;


        private void OnEnable()
        {
            _haze.enabled = false;
        }

        private void Start()
        {
            _costLabel.text = _card.CardSettings.Cost.ToString();
            _heroView.sprite = _card.Hero.GetComponent<SpriteRenderer>().sprite;
        }

        private IEnumerator HideBuyHazeCoroutine()
        {
            var tween = _hazeToAnimate.DOFillAmount(0, _card.CardSettings.ReloadingTimeAfterBuy).SetEase(Ease.Linear);
            yield return tween.WaitForCompletion();
            _haze.enabled = false;
            _hazeToAnimate.enabled = false; 
            _card.Activate();
        }

        public void HideBuyHaze()
        {
            _hazeToAnimate.enabled = true;
            _hazeToAnimate.fillAmount = 1;
            StartCoroutine(HideBuyHazeCoroutine());
        }

        public void EnableHaze()
        {
            _haze.enabled = true;
        }

        public void DisableHaze()
        {
            _haze.enabled = false;
        }

        public void PlayError()
        {
            _error.enabled = true;
            _error.DOKill();
            var sequence = DOTween.Sequence();
            sequence.Append(_error.DOColor(new Color(1, 0, 0, 0.5f), _errorTime));
            sequence.Append(_error.DOColor(new Color(0, 0, 0, 0), _errorTime));
            sequence.SetLoops(_loopCount).OnComplete(
                () => _error.enabled = false);
            sequence.Play();
        }
    }
}