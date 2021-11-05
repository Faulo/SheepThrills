using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private Image _canvasImage;
    [SerializeField] private int _givenFoodAmount;
    [SerializeField] private int _neededSheepCount;
    [SerializeField] private float _areaRadius;
    [SerializeField] private int _currentSheepCount;
    private Coroutine _destroyRoutine;


    private void Update()
    {
        var colliders = Physics.OverlapSphere(transform.position, _areaRadius);
        _currentSheepCount = 0;
        for (var i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Sheep"))
            {
                _currentSheepCount++;
            }
        }

        var fillAmount = 0f;
        if (_currentSheepCount < _neededSheepCount)
        {
            fillAmount = (float)_currentSheepCount / _neededSheepCount;
            _canvasImage.color = Color.white;
        }
        else
        {
            fillAmount = 1f;
            _canvasImage.color = Color.green;
        }

        _canvasImage.fillAmount = fillAmount;

        if (_currentSheepCount >= _neededSheepCount)
        {
            _destroyRoutine = StartCoroutine(DestroyCoroutine());
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _areaRadius);
    }
}