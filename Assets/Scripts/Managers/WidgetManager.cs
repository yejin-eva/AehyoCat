using System;
using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private CookieManager cookieManager;

    [SerializeField] private QuoteBubble quoteBubble;
    [SerializeField] private WeatherButton weatherButton;
    [SerializeField] private StickyNotesButton stickyNotesButton;
    [SerializeField] private SadCatButton sadCatButton;
    [SerializeField] private CatNameButton catNameButton;
    [SerializeField] private SaveNameButton saveNameButton;
    [SerializeField] private ChatButton chatButton;
    [SerializeField] private GameObject catNamePopup;
    [SerializeField] private VivoxLoginUI vivoxLoginUI;
    private void OnEnable()
    {
        catNamePopup.SetActive(false);
    }
    private void Start()
    {
        cookieManager.OnAteCookie += OnAteCookie;
        cat.OnCatIsHungry += OnCatIsHungry;
        cat.OnCatIsFull += OnCatIsFull;
        sadCatButton.OnSadCatButton += SetHungerMessage;
        chatButton.onChatButton += OnChatButtonClicked;
        catNameButton.onCatNameButtonClicked += OnCatNameButtonClicked;
        saveNameButton.onSaveNameButtonClicked += OnSaveNameButtonClicked;
    }

    private void OnChatButtonClicked()
    {
        vivoxLoginUI.OnUserLoggedOut();
    }

    private void OnSaveNameButtonClicked()
    {
        cat.CatName = saveNameButton.GetCatName();
        catNameButton.SetCatName(cat.CatName);
        catNamePopup.SetActive(false);

        Debug.Log(cat.CatName);
    }

    private void OnCatNameButtonClicked()
    {
        if (!catNamePopup.activeSelf)
        {
            catNamePopup.SetActive(true);
        }
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
