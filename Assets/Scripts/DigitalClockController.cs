using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalClockController : MonoBehaviour
{
    [SerializeField] private Text _timerText;

    private DateTime _time;

    public void StartAnimation(DateTime time)
    {
        _time = time;
        _timerText.text = _time.ToLongTimeString();
        StartCoroutine(RunCoroutineOneSecond());
    }

    private IEnumerator RunCoroutineOneSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        _time = _time.AddSeconds(1);
        _timerText.text = _time.ToLongTimeString();
    }

    public DateTime GetDigitClockTime()
    {
        return _time;
    }

    public void StopAndClearAnimation()
    {
        StopAllCoroutines();
    }
}
