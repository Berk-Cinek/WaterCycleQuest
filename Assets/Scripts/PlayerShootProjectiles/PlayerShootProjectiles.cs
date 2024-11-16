using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System;

public class ShootProjectiles : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;

    private void Awake()
    {
        GetComponent<PlayerAimWeapon>().OnShoot += PlayerShootProjectiles_OnShoot;
    } 

    private void PlayerShootProjectiles_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
       Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);

       Vector3 shootDir = e.shootPosition - e.gunEndPointPosition.normalized;
       bulletTransform.GetComponent<Bullet>().Setup(shootDir);
            

    }


}
