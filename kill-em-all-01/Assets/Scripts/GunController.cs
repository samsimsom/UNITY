using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform weaponHold;
    [SerializeField] private Gun startingGun;
    private Gun equippedGun;


    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        equippedGun = Instantiate(gunToEquip,
            weaponHold.position,
            weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }


    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }


    void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }
}