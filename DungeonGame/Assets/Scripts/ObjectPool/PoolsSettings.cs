using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolsSettings
{
    [SerializeField] private string _objectType;
    [SerializeField] private int _size;
	[SerializeField] private GameObject _prefab;
    public int Size => _size;
	public GameObject Prefab => _prefab;

	public Type ObjectType
	{
		get 
		{
			return Type.GetType(_objectType);
        }
	}

}
