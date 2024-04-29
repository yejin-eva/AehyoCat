using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private CookieManager cookieManager;

    private void OnEnable()
    {
        cookieManager.OnAteCookie += OnCatAteCookie;
    }

    private void OnCatAteCookie()
    {
        cat.AddHealth(10f);
    }
}
