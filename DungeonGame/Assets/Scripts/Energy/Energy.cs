using ObjectPool;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Energy : PoolItem, IPointerClickHandler
{
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private uint _energyUnits = 2;
    [SerializeField] private float _lifeTime = 2.5f;
    [SerializeField] private EnergyAnimator _energyAnimator;

    private Coroutine _lifeCoroutine;

    public override Type ItemType { get => GetType(); }
    public override GameObject GameObject => gameObject;
    public uint EnergyUnits => _energyUnits;

    public static event Action<Energy> EnergyCollected;

    private void OnValidate()
    {
        _energyAnimator ??= FindObjectOfType<EnergyAnimator>();
    }

    private void OnEnable()
    {
        _lifeCoroutine = StartCoroutine(LifeLoopCoroutine());
    }

    private IEnumerator LifeLoopCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        _energyAnimator.AnimateDeath(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _collider2D.enabled = false;
        EnergyCollected?.Invoke(this);
        _energyAnimator.AnimateAfterCollect(this);
        StopCoroutine(_lifeCoroutine);
    }

    public override void SetDefaultSettings()
    {
        _collider2D.enabled = true;
    }

    public void Kill()
    {
        ReleaseItem();
    }
}
