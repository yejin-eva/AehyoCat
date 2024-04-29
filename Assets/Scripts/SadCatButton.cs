using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadCatButton : MonoBehaviour
{
    public Action OnSadCatButton;

    private UnityEngine.UI.Button sadCatButton;
    private Vector3 originalLocalScale;

    private void Awake()
    {
        originalLocalScale = transform.localScale;
        sadCatButton = GetComponent<UnityEngine.UI.Button>();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.DOScale(originalLocalScale, 0.3f).From(originalLocalScale * 0.5f).SetEase(Ease.OutBack);
        sadCatButton.onClick.AddListener(() => OnSadCatButtonClicked());
    }
    private void OnDisable()
    {
        sadCatButton.onClick.RemoveAllListeners();
    }
    private void OnSadCatButtonClicked()
    {
        OnSadCatButton?.Invoke();
    }

}
