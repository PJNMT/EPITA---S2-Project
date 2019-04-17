﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class tresor : MonoBehaviour
{
    private static List<int> _path= new List<int>();
    private Enigma _enigma;
    private static string _enigmefile = "enigme.txt";
    private int or;
    
    // Place le trésor aléatoirement sur la map
    void Start()
    {
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);


        transform.Translate(x, 1f, z);
      
    }

    // Cherche si le le capitaine à trouvé le trésor
    void Update()
    {
        OnTriggerEnter(gameObject.GetComponent<Collider>());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BlindPirate")
        {
            //On cherche une enigme aléatoire pas encore rencontré
            int a = Search();      
            
            //crée l'objet enigme choisit et _enigma prends sa valeur
            Generateenigme(a);                                       
            
            //Reconnaissance de la réponse du joueur
            Recognition.Function Traitement = Answertreatement;
            
            //Dis l'énigme au joueur
            SpeakEnigma(_enigma);
            string lecturetraitement;
            
            do
            {
                Recognition.start_recognition(20000,"repeats "+_enigma._answer+" indice", Traitement);  //Reconnait tant qu'une réponse est attendue
                lecturetraitement = Console.ReadLine();

            } while (lecturetraitement== "repeat" || lecturetraitement == "indice");

            
            //récupération de l'issue de la réponse
            if (Console.ReadLine() == "good answer")
            {
                _path.Add(_enigma._number);
                //dire si c'est la bonne réponse !!
                BlindShip_Stat.Money += or;
            }
        }

    }
    
    private int Search()
    {
        using (StreamReader lire = new StreamReader(_enigmefile))
        {
            float nb = Random.Range(0.0f, 100f);

            foreach (int occu in _path)
            {
                if ((int)nb == occu)
                {
                    Search();
                }
            }

            return (int) nb;

        }
    }

    
        
    void Generateenigme(int nb)
    {
        using (StreamReader read = new StreamReader(_enigmefile))
        {
            
            while (read.Read()-48 != nb)
            {
                read.ReadLine();
                
            }
            _enigma = gameObject.AddComponent<Enigma>();

            string lecture = read.ReadLine();
            string[] division = lecture.Split(':');

            _enigma._enigme = division[0];
            _enigma._indice = division[1];
            _enigma._answer = division[2];
            or = int.Parse(division[3]);

        }
    
    }

    static void SpeakEnigma(Enigma enigma)
    {
        
    }

    static void SpeakIndice(Enigma enigma)
    {
        
    }
    
    void Answertreatement(string reponse)
    {
        Console.Clear();
        switch (reponse)
        {
          case "repeats":
              tresor.SpeakEnigma(_enigma);
              Console.WriteLine("repeat");
              break;
          case "indice":
              tresor.SpeakIndice(_enigma);
              Console.WriteLine("indice");
              break;
             
          default:
              Console.WriteLine("correct answer");
              break;
        }
    }

}

public  class Enigma : MonoBehaviour
{
    public int _number;
    public string _enigme;
    public string _indice;
    public string _answer;

    public Enigma(int number, string enigme, string indice, string answer)
    {
        _number = number;
        _enigme = enigme;
        _indice = indice;
        _answer = answer;

    }

    private void Start()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        throw new System.NotImplementedException();
    }
}
