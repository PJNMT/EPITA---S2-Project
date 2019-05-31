﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlindCaptain_Stat : MonoBehaviour
{
    public static int Lvl = 1;
    private static int Max_Lvl = 50;
    
    public static int HP = Lvl * 100;
    public static int Max_HP = Lvl * 100;
    
    public static int XP = 0;
    public static int Max_XP = Lvl * 100;

    public static int GunDamage = 40;
    public static int Max_GunDamage = Lvl * 40;
    
    public static int SwordDamage = 15;
    public static int Max_SwordDamage = Lvl * 15;

    public static int Reputation = 0;
    public static string Name = "BlindPirate";
    
    [SerializeField]
    private AudioClip level;
    [SerializeField]
    private AudioClip gundamage;
    [SerializeField]
    private AudioClip sworddamage;
    
    public static Dictionary<string, bool> Tuto = new Dictionary<string, bool>()
    {
        {"SeaBattle", false},
        {"ChasseAuTresor", false},
        {"Simon", false},
        {"Combat", false},
        {"Coco", false},
    };

    private void Update()
    {
        if (HP <= 0) Dead();
        if (XP >= Max_XP) GetLVL();
        if (Lvl > Max_Lvl) Lvl = Max_Lvl;
        if (GunDamage > Max_GunDamage) GunDamage = Max_GunDamage;
        if (SwordDamage > Max_SwordDamage) SwordDamage = Max_SwordDamage;
    }

    public static void GetLVL()
    {
        Lvl += 1;
        SetStat();
        SwordDamage += 10;
        GunDamage += 30;
    }

    public static void SetStat()
    {
        XP = 0;
        Max_XP = Lvl * 100;
        
        Max_HP = Lvl * 100;
        HP = Max_HP;
        
        Max_GunDamage = Lvl * 15;
        
        Max_SwordDamage = Lvl * 40;
    }

    public static void Dead()
    {
        Recognition.stop_recognition();
        SetStat();
        BlindShip_Stat.SetStat();
        BlindShip_Stat.Money -= BlindShip_Stat.Money / 5;
        BlindShip_Stat.Dead();
        Scene S = SceneManager.GetActiveScene();
        LoadScene.Load(LoadScene.Scene.Navigation, (LoadScene.Scene) S.buildIndex);
    }
    
   public void AddStat(AudioSource audioSource, string stat)
    {
        audioSource.clip = level;
        audioSource.Play();
        switch (stat)
        {
            case  "level":
                Lvl += 1;
                Synthesis.synthesis("Vous avez gagné 1 niveau");
                break;
            case "reputation":
                Reputation += 5;
                Synthesis.synthesis("Vous avez gagné 5 points de réputation");
                break;
            case "XP":
                XP += 100;
                Synthesis.synthesis("Vous avez gagné 100 XP");
                break;
            case "HP":
                HP = Max_HP;
                Synthesis.synthesis("Vos HP sont à leur maximum, Capitaine, vous êtes en pleine forme !");
                break;
        }
        
    }

    public void AddSworddamage(AudioSource audioSource, int added)
    {
        audioSource.clip = sworddamage;
        audioSource.Play();
        SwordDamage += added;
    }
    
    public void AddGundamage(AudioSource audioSource, int added)
    {
        audioSource.clip = gundamage;
        audioSource.Play();
        GunDamage += added;
    }
}

