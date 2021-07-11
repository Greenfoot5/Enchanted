using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;

    public Transform firePoint;
    // Create the bullet and set the target
    public void Shoot()
    {
        var bulletGO = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        var bullet = bulletGO.GetComponent<BulletSpell>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
