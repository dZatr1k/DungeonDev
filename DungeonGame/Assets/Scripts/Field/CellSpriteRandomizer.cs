using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpriteRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;

    private void Start()
    {
        int randomSpriteIndex = Random.Range(0, _sprites.Count);
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = _sprites[randomSpriteIndex];
    }
}
