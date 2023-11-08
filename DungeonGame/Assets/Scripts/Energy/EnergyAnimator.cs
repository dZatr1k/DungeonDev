using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnergyAnimator : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    [SerializeField] private Transform _targetTransform;
    private void OnEnable()
    {
        Energy.EnergyCollected += AnimateAfterCollect;
    }

    private void OnDisable()
    {
        Energy.EnergyCollected -= AnimateAfterCollect;
    }

    private void AnimateAfterCollect(Energy energy)
    {
        StartCoroutine(AnimateAfterCollectCourutine(energy));
    }

    private IEnumerator AnimateAfterCollectCourutine(Energy energy)
    {
        var tween = energy.transform.DOMove(_targetTransform.position, _moveTime);
        energy.GetComponent<SpriteRenderer>().DOColor(new Color(1,1,1,0), _moveTime).SetEase(Ease.InExpo);
        yield return tween.WaitForCompletion();
        energy.Kill();
    }
}
