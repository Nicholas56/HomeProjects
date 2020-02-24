using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootingScript : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;

    public int maxAmmo;
    int ammo;
    public float shootDelay;
    float timer;

    public TMP_Text bulletCount;
    public Image reloadVisual;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        UpdateAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&&ammo>0&&Time.time>timer)
        {
            GameObject projectile = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
            ammo--;
            UpdateAmmo();
            timer = Time.time + shootDelay;
            reloadVisual.fillAmount = 1;
            anim.SetTrigger("Fire");
        }
        reloadVisual.fillAmount -= Time.deltaTime / shootDelay;
    }

    public void UpdateAmmo()
    {
        if (ammo > maxAmmo) { ammo = maxAmmo; }
        bulletCount.text = "Ammo\n" + ammo + "\\" + maxAmmo;
    }

    public void Refill(int amount)
    {
        ammo += amount;
    }
}
