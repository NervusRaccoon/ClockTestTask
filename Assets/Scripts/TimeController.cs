using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public Action OneHourPassed;

    private const int ONE_HOUR_TIME = 10;

    public void StartOneHourTimer()
    {
        StartCoroutine(RunCoroutineOneHour());
    }

    private IEnumerator RunCoroutineOneHour()
    {
        while (true)
        {
            yield return new WaitForSeconds(ONE_HOUR_TIME);
            OneHourPassed?.Invoke();
        }
    }

    public void StopOneHourTimer()
    {
        StopAllCoroutines();
    }
}
