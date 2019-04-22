﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ship-Shop")
        {
            SceneManager.LoadScene("ShipShop");
            SceneManager.UnloadSceneAsync("Port");
        }

        if (other.gameObject.name == "Bar")
        {
            Debug.Log("papap");
            SceneManager.LoadScene("taverne");
            SceneManager.UnloadSceneAsync("Port");
        }
		
    }
}