using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxMana = 100;

    private int health;
    private int mana;

    public HealthBar healthBar;
    public HealthBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mana = maxMana;

        healthBar.SetMaxValue(maxHealth);
        manaBar.SetMaxValue(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health = health > damage ? health - damage : 0;
        healthBar.SetValue(health);
    }

    public void UseMana(int quantity)
    {
        mana = mana > quantity ? mana - quantity : 0;
        manaBar.SetValue(mana);
    }
}
