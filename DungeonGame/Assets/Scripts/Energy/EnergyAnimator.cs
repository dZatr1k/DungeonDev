using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class EnergyAnimator : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    [SerializeField] private Transform _targetTransform;

    private IEnumerator AnimateAfterCollectCourutine(Energy energy)
    {
        var tween = energy.transform.DOMove(_targetTransform.position, _moveTime);
        Fade(energy, _moveTime);
        yield return tween.WaitForCompletion();
        energy.Kill();
    }

    private IEnumerator AnimateDeathCoroutine(Energy energy, float time)
    {
        yield return Fade(energy, time).WaitForCompletion();
        energy.Kill();
    }

    private Tween Fade(Energy energy, float time)
    {
        return energy.GetComponent<SpriteRenderer>().DOColor(new Color(1,1,1,0), time).SetEase(Ease.InExpo);
    }

    public void AnimateDeath(Energy energy)
    {
        StartCoroutine(AnimateDeathCoroutine(energy, _moveTime));
    }

    public void AnimateAfterCollect(Energy energy)
    {
        StartCoroutine(AnimateAfterCollectCourutine(energy));
    }


}
