using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectScript : MonoBehaviour
{
    public float spellRadius;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionDamage();
        Invoke("ExplosionDamage", 3);
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    void ExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spellRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].GetComponent<HealthScript>())
            {
                hitColliders[i].GetComponent<HealthScript>().SpellAttack();
            }
            i++;
        }
    }
}
