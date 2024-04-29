using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float Health => hp;
    public Action OnCatIsHungry;
    public Action OnCatIsFull;

    [SerializeField] private UnityEngine.UI.Image progressBarFill;

    private float hp = 0f;
    private float maxHp = 100.0f;
    private float healthDecreaseAmount = 10f;
    private bool isDeadNotified = false;
    private bool isHungryNotified = false;

    private void Awake()
    {
        hp = maxHp;
        progressBarFill.fillAmount = 1f;
    }
    private void Update()
    {
        CheckHp();
        SubtractHealth(Time.deltaTime * healthDecreaseAmount);
    }


    private void CheckHp()
    {
        if (hp <= 0)
        {
            if (!isDeadNotified)
            {
                isDeadNotified = true;
                isHungryNotified = false;
            }
        }
        else if (hp <= 30)
        {
            if (!isHungryNotified)
            {
                OnCatIsHungry?.Invoke();
                isHungryNotified = true;
                isDeadNotified = false;
            }
        }
        else
        {
            if (isHungryNotified || isDeadNotified)
            {
                OnCatIsFull?.Invoke();
                isHungryNotified = false;
                isDeadNotified = false;
            }
        }
    }

    public void SetHealth(float health)
    {
        hp = Mathf.Min(health, maxHp);
        FillHealthBar();
    }

    public void SubtractHealth(float health)
    {
        hp = Mathf.Max(hp - health, 0f);
        FillHealthBar();
    }
    public void AddHealth(float health)
    {
        hp = Mathf.Min(hp + health, maxHp);
        FillHealthBar();
    }
    private void FillHealthBar()
    {
        progressBarFill.fillAmount = (float) hp / maxHp;
    }
}
