using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private CookieManager cookieManager;

    [SerializeField] private QuoteBubble quoteBubble;
    [SerializeField] private WeatherButton weatherButton;
    [SerializeField] private StickyNotesButton stickyNotesButton;
    [SerializeField] private SadCatButton sadCatButton;

    private void OnEnable()
    {
        cookieManager.OnAteCookie += OnAteCookie;
        cat.OnCatIsHungry += OnCatIsHungry;
        cat.OnCatIsFull += OnCatIsFull;
        sadCatButton.OnSadCatButton += SetHungerMessage;
    }


    private void OnCatIsFull()
    {
        ActivateButtons(true);
        sadCatButton.gameObject.SetActive(false);
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

    private void OnAteCookie()
    {
        cat.AddHealth(10f);
    }

    private void SetHungerMessage()
    {
        quoteBubble.gameObject.SetActive(true);
        quoteBubble.SetQuoteText("I'm too hungry for work. Feed me!");
    }
}
