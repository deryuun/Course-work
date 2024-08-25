using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    public float _offset = 0;
    private int _sortingOrderBase = 0;
    private Renderer _renderer;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    
    void LateUpdate()
    {
        _renderer.sortingOrder = (int)(_sortingOrderBase - transform.position.y + _offset);
    }
}
