﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{

    public static GameObject Projectile;
    public int Force = 50;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(captainattack.gunatk))
        {
            GameObject Bullet = Instantiate(Projectile, transform.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * Force;
        }
    }
}
