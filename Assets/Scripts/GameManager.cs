using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeController _timeController;
    [SerializeField] private ArrowsController _arrowsController;
    [SerializeField] private DigitalClockController _digitClockController;

    private const string URL = "https://yandex.com/time/sync.json";
    private DateTime _time;
    private DateTime _oldTime;
    private Action _onCompleteResponse;

    private void Start()
    {
        _timeController.StartOneHourTimer();
        _timeController.OneHourPassed += CheckRealTime;
        _onCompleteResponse += StartAnimations;
        GetResponse();
    }

    private void CheckRealTime()
    {
        GetResponse();
    }

    private async void GetResponse()
    {
        HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            string time = responseBody.Split(',')[0].Split(':')[1];
            _time = new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(time));
        }
        catch (HttpRequestException e)
        {
            string messageResult = e.Message;
        }

        _onCompleteResponse?.Invoke();
    }

    private void StartAnimations()
    {
        DateTime _digitTime = _digitClockController.GetDigitClockTime();
        if (_digitTime.Hour != _time.Hour || _digitTime.Minute != _time.Minute || _digitTime.Second != _time.Second)
        {
            StopAnimations();
            _arrowsController.StartAnimation(_time);
            _digitClockController.StartAnimation(_time);
        }
    }

    private void StopAnimations()
    {
        _arrowsController.StopAndClearAnimation();
        _digitClockController.StopAndClearAnimation();
    }

    private void OnDestroy()
    {
        _timeController.OneHourPassed -= CheckRealTime;
        _onCompleteResponse -= StartAnimations;
    }
}
