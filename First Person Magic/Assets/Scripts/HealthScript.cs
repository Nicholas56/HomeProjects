using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float maxHealth = 100f;
    float health;

    public Image healthBar;

    public float spellDamage = 20f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void SpellAttack()
    {
        health -= spellDamage;
    }
}
