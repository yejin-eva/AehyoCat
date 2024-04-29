using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private CookieManager cookieManager;

    [SerializeField] private WeatherButton weatherButton;
    [SerializeField] private StickyNotesButton stickyNotesButton;
    [SerializeField] private SadCatButton sadCatButton;

    private void OnEnable()
    {
        cookieManager.OnAteCookie += OnCatAteCookie;
        cat.OnCatIsHungry += OnCatIsHungry;
        cat.OnCatIsFull += OnCatIsFull;
    }

    private void OnCatIsFull()
    {
        ActivateButtons(true);
    }

    private void OnCatIsHungry()
    {
        ActivateButtons(false);
        sadCatButton.gameObject.SetActive(true);
    }

    private void ActivateButtons(bool status)
    {
        weatherButton.gameObject.SetActive(status);
        stickyNotesButton.gameObject.SetActive(status);
    }

    private void OnCatAteCookie()
    {
        cat.AddHealth(10f);
    }
}
