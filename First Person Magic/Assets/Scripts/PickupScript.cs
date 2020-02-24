using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int refillAmount;
    public bool willRespawn;
    public float respawnTime;
    bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player"&&isActive)
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<ShootingScript>().Refill(refillAmount);
            other.transform.GetChild(0).GetChild(0).GetComponent<ShootingScript>().UpdateAmmo();
            Despawn();
        }
    }

    void Despawn()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isActive = false;
        if (willRespawn)
        {
            Invoke("Respawn", respawnTime);
        }
    }

    void Respawn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        isActive = true;
    }
}
