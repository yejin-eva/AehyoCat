using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float Health => hp;

    [SerializeField] private UnityEngine.UI.Image progressBarFill;

    private float hp = 0f;
    private float maxHp = 100.0f;
    private float healthDecreaseAmount = 2f;

    private void Awake()
    {
        hp = maxHp;
        progressBarFill.fillAmount = 1f;
    }
    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("Cat is dead!");
        }

        SubtractHealth(Time.deltaTime * healthDecreaseAmount);
        
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
