using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using models;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    string lineaLeida = "";
    List<PreguntaMultiple> listaPreguntasMultiples;
    List<preguntaAbierta> listaPreguntasAbiertas;
    List<PreguntaMultiple> listaPMF;
    List<PreguntaMultiple> listaPMD;
    List<preguntaAbierta> listaPAF;
    List<preguntaAbierta> listaPAD;
    string respuestaPM;
    public bool dificiles = false;
    bool PMvacio = false;

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textPreguntaA;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;
    public TMP_InputField inputR;
    public GameObject PanelRC;
    public GameObject PanelRI;
    public GameObject PanelDif;
    public GameObject PanelPA;
    public GameObject PanelPM;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        PanelRC.SetActive(false);
        PanelRI.SetActive(false);
        PanelDif.SetActive(false);
        PanelPA.SetActive(false);
        listaPMF = new List<PreguntaMultiple>();
        listaPMD = new List<PreguntaMultiple>();
        listaPAF = new List<preguntaAbierta>();
        listaPAD = new List<preguntaAbierta>();
        listaPreguntasAbiertas = new List<preguntaAbierta>();
        listaPreguntasMultiples = new List<PreguntaMultiple>();
        LecturaPreguntasMultiples();
        LecturaPreguntasAbiertas();
        esFacil();
        mostrarPreguntasMultiples();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void esFacil() 
    { 
        for(int i = 0; i < listaPreguntasMultiples.Count; i++)
        {
            if (listaPreguntasMultiples[i].Dificultad == "facil")
            {
                listaPMF.Add(listaPreguntasMultiples[i]);
            }
            else 
            {
                listaPMD.Add(listaPreguntasMultiples[i]);
            }
        }
        Debug.Log("Lista faciles multiples: " + listaPMF.Count + "Lista dificiles multiples:" + listaPMD.Count);

        for (int i = 0; i < listaPreguntasAbiertas.Count; i++)
        {
            if (listaPreguntasAbiertas[i].Dificultad == "facil")
            {
                listaPAF.Add(listaPreguntasAbiertas[i]);
            }
            else
            {
                listaPAD.Add(listaPreguntasAbiertas[i]);
            }
        }
        Debug.Log("Lista faciles abiertas: " + listaPMF.Count + "Lista dificiles abiertas:" + listaPMD.Count);
    }
    public void mostrarPreguntasMultiples()
    {
        
        int tipo = UnityEngine.Random.Range(1,2);

        if (dificiles == false)
        {
           
            
            if (tipo == 1 && PMvacio != true)
            {
                Debug.Log("Tipo de pregunta:"+ tipo);
                PanelPA.SetActive(false);
                if (listaPMF.Count != 8)
                {
                    int i = UnityEngine.Random.Range(0, listaPMF.Count);
                    Debug.Log("Indice random: " + i);
                    if (listaPMF[i] != null)
                    {
                        textPregunta.text = listaPMF[i].Pregunta;
                        textResp1.text = listaPMF[i].Respuesta1;
                        textResp2.text = listaPMF[i].Respuesta2;
                        textResp3.text = listaPMF[i].Respuesta3;
                        textResp4.text = listaPMF[i].Respuesta4;
                        respuestaPM = listaPMF[i].RespuestaCorrecta;
                        listaPMF.Remove(listaPMF[i]);
                    }
                }
                else
                {
                    PMvacio = true; 
                    PanelPM.SetActive(false);
                    mostrarPreguntasMultiples();
                }
            }
            else 
            {
                Debug.Log("Tipo de pregunta:" + tipo);
                PanelPA.SetActive (true);
                if (listaPAF.Count != 0) 
                {
                    int i = UnityEngine.Random.Range(0, listaPAF.Count);
                    Debug.Log("Indice random: " + i);
                    if (listaPAF[i] != null)
                    {
                        textPreguntaA.text = listaPAF[i].Pregunta;
                        respuestaPM = listaPAF[i].Respuesta;
                        listaPAF.Remove(listaPAF[i]);
                    }
                }
            }

            
        }
        else 
        {
            if (listaPMD.Count != 0)
            {
                int i = UnityEngine.Random.Range(0, listaPMD.Count);
                if (listaPMD[i] != null)
                {
                    textPregunta.text = listaPMD[i].Pregunta;
                    textResp1.text = listaPMD[i].Respuesta1;
                    textResp2.text = listaPMD[i].Respuesta2;
                    textResp3.text = listaPMD[i].Respuesta3;
                    textResp4.text = listaPMD[i].Respuesta4;
                    respuestaPM = listaPMD[i].RespuestaCorrecta;
                    listaPMF.Remove(listaPMD[i]);
                }
            }
        }
        
    }

    #region comprobacion de respuesta
    public void comprobarRespuesta1()
    {
        if (textResp1.text.Equals(respuestaPM))
        {
           PanelRC.SetActive(true);
           PanelRI.SetActive(false);
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
        }
    }

    public void comprobarRespuesta2()
    {
        if (textResp2.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);

        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
        }
    }
    public void comprobarRespuesta3()
    {
        if (textResp3.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);

        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
        }
    }
    public void comprobarRespuesta4()
    {
        if (textResp4.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);

        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
        }
    }

    public void comprobarRespuestaAbierta()
    {
        inputR.text = inputR.text.ToLower();
        respuestaPM = respuestaPM.ToLower();
        Debug.Log(inputR.text);
        Debug.Log(respuestaPM);
        if (inputR.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
        }
        else 
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
        }
    }
    #endregion

    #region Lectura archivos
    public void LecturaPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta= lineaPartida[5];
                string versiculo = lineaPartida[6];
                string difucltad = lineaPartida[7];

                PreguntaMultiple objPM=new PreguntaMultiple(pregunta, respuesta1, respuesta2, respuesta3,
                    respuesta4, respuestaCorrecta, versiculo, difucltad);

                listaPreguntasMultiples.Add(objPM);

            }
            Debug.Log("El tamaño de la lista es " + listaPreguntasMultiples.Count);
        }
        catch(Exception e) 
        { 
            Debug.Log("ERROR!!!!! "+e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }
    public void LecturaPreguntasAbiertas() 
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/ArchivoPreguntasAbiertas.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                string dificultad = lineaPartida[3];
                

                preguntaAbierta objPA = new preguntaAbierta(pregunta, respuesta, versiculo, dificultad);

                listaPreguntasAbiertas.Add(objPA);

            }
            Debug.Log("El tamaño de la lista es " + listaPreguntasAbiertas.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }
    #endregion


}
