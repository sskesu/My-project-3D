using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Substract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

}

public class PlayerConditions : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    public float noHungerHealthDecay;

    public UnityEvent onTakeDamage;

    private void Start()
    {
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        stamina.curValue = stamina.startValue;
    }

    private void Update()
    {
        hunger.Substract(hunger.decayRate * Time.deltaTime);
        stamina.Add(stamina.regenRate * Time.deltaTime);

        if (hunger.curValue == 0.0f)
        {
            health.Substract(noHungerHealthDecay * Time.deltaTime);
        }
        if (health.curValue == 0.0f) 
        {
            Die();  
        }

        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        stamina.uiBar.fillAmount = stamina.GetPercentage();

    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue < amount)
        {
            return false;
        }

        stamina.Substract(amount);
        return true;
    }

    public void Die()
    {
        Debug.Log("����");
    }
}
