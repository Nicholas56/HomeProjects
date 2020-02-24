using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public float lifeTime;

    public int spellNumber;
    public GameObject spell;

    GameObject[] orbs;

    private void Start()
    {
        Invoke("SelfDestroy", lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.SetParent(collision.transform);
        Destroy(GetComponent<Rigidbody>());

        FindOrbs();
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public void FindOrbs()
    {
        orbs = GameObject.FindGameObjectsWithTag("Orb");
        if (orbs.Length == spellNumber)
        {
            Vector3 midPoint = Vector3.zero;
            for(int i = 0; i < orbs.Length - 1; i++)
            {
                if (midPoint==Vector3.zero) { midPoint = (orbs[i].transform.position + orbs[i + 1].transform.position) / 2; } else
                {
                    midPoint = (midPoint + orbs[i + 1].transform.position) / 2;
                }
            }
            Instantiate(spell, midPoint, Quaternion.identity);
            foreach(GameObject orb in orbs)
            {
                Destroy(orb);
            }
        }
    }
}
