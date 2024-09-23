using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ArrowsController _arrowsController;
    [SerializeField] private DigitalClockController _digitClockController;

    private const string URL = "https://yandex.com/time/sync.json";

    private void Start()
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
            StartArrowsAnimation(new DateTime(1970, 1, 1).AddMilliseconds(double.Parse(time)));
        }
        catch (HttpRequestException e)
        {
            string messageResult = e.Message;
        }

    }

    private void StartArrowsAnimation(DateTime time)
    {
        _arrowsController.StartAnimation(time);
        _digitClockController.StartAnimation(time);
    }
}
