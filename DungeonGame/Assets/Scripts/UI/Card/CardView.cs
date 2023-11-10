using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image _haze;
        [SerializeField] private Image _hazeToAnimate;
        [SerializeField] private Image _error;
        [SerializeField] private float _errorTime;
        [SerializeField] private int _loopCount;
        [SerializeField] private Card _cardToAnimate;

        private void OnEnable()
        {
            _haze.enabled = false;
        }

        private IEnumerator HideBuyHazeCoroutine()
        {
            var tween = _hazeToAnimate.DOFillAmount(0, _cardToAnimate.Hero.ReloadAfterBuyTime).SetEase(Ease.Linear);
            yield return tween.WaitForCompletion();
            _haze.enabled = false;
            _hazeToAnimate.enabled = false;
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