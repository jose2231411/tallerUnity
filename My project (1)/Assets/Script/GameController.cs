using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using models;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;
//INTEGRANTES 

//JoseAndresCriolloEcheverri

//JuanCamiloSalgadoBaldion

//JuanDavidUrrea

//JuanDiegoPatiñoParra
public class GameController : MonoBehaviour
{
    #region declaracion de variables
    string lineaLeida = "";
    List<PreguntaMultiple> listaPreguntasMultiples;
    List<preguntaAbierta> listaPreguntasAbiertas;
    List<preguntaFV> listaPreguntasFV;
    List<preguntaFV> listaPFVF;
    List<preguntaFV> listaPFVD;
    List<PreguntaMultiple> listaPMF;
    List<PreguntaMultiple> listaPMD;
    List<preguntaAbierta> listaPAF;
    List<preguntaAbierta> listaPAD;
    string respuestaPM;
    public bool dificiles = false;
    public bool PMvacio = false;
    public bool PAvacio = false;
    public bool PFVvacio = false;
    public int RC = 0;
    public int RI = 0;
    List<int> tiposDisponibles = new List<int> { 1, 2, 3 };

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textPreguntaA;
    public TextMeshProUGUI textPreguntaFV;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;
    public TextMeshProUGUI textRC;
    public TextMeshProUGUI textRI;
    public TMP_InputField inputR;
    public GameObject PanelRC;
    public GameObject PanelRI;
    public GameObject PanelLvlup;
    public GameObject PanelDif;
    public GameObject PanelPA;
    public GameObject PanelPM;  
    public GameObject PanelPFV;
    public TextMeshProUGUI hahaha;
 

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region inicializacion
        
        PanelRC.SetActive(false);
        PanelRI.SetActive(false);
        PanelDif.SetActive(false);
        PanelPA.SetActive(false);
        PanelPFV.SetActive(false);
        PanelLvlup.SetActive(false);
        listaPMF = new List<PreguntaMultiple>();
        listaPMD = new List<PreguntaMultiple>();
        listaPAF = new List<preguntaAbierta>();
        listaPAD = new List<preguntaAbierta>();
        listaPFVF = new List<preguntaFV>();
        listaPFVD = new List<preguntaFV>();
        listaPreguntasAbiertas = new List<preguntaAbierta>();
        listaPreguntasMultiples = new List<PreguntaMultiple>();
        listaPreguntasFV = new List<preguntaFV>();
        LecturaPreguntasMultiples();
        LecturaPreguntasAbiertas();
        LecturaPreguntasFV();
        organizar();
        mostrarPreguntas();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    #region organizarpreguntas
    public void organizar()
    {
        for (int i = 0; i < listaPreguntasMultiples.Count; i++)
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
        Debug.Log("Lista faciles abiertas: " + listaPAF.Count + "Lista dificiles abiertas:" + listaPAD.Count);

        for (int i = 0; i < listaPreguntasFV.Count; i++)
        {
            if (listaPreguntasFV[i].Dificultad == "facil")
            {
                listaPFVF.Add(listaPreguntasFV[i]);
            }
            else
            {
                listaPFVD.Add(listaPreguntasFV[i]);
            }
        }
        Debug.Log("Lista faciles fv: " + listaPFVF.Count + "Lista dificiles fv:" + listaPFVD.Count);
    }
    #endregion

    #region tipo de pregunta y pase a dificil
    public void mostrarPreguntas()
    {
        if (tiposDisponibles.Count == 0 && dificiles)
        {
            PanelDif.SetActive(true);
            textRC.SetText(RC.ToString());
            textRI.SetText(RI.ToString());
            return;
        }
        if (tiposDisponibles.Count == 0)
        {
            dificiles = true;
            PanelLvlup.SetActive(true);
            PMvacio = false;
            PAvacio = false;
            PFVvacio = false;
            tiposDisponibles.Add(1);
            tiposDisponibles.Add(2);
            tiposDisponibles.Add(3);
            mostrarPreguntas();
            return;
        }
        Debug.Log("Mostrando pregunta...");
        int tipo = tiposDisponibles[UnityEngine.Random.Range(0, tiposDisponibles.Count)];
        Debug.Log("tipo: " + tipo);
       
        switch (tipo)
        {
            case 1:
                PanelPA.SetActive(false);
                PanelPFV.SetActive(false);
                MostrarPreguntaMultiple(dificiles ? listaPMD : listaPMF, ref PMvacio, PanelPM);
                break;
            case 2:
                PanelPM.SetActive(false);
                PanelPFV.SetActive(false);
                MostrarPreguntaAbierta(dificiles ? listaPAD : listaPAF, ref PAvacio, PanelPA);
                break;
            case 3:
                PanelPA.SetActive(false);
                PanelPM.SetActive(false);
                MostrarPreguntaFV(dificiles ? listaPFVD : listaPFVF, ref PFVvacio, PanelPFV);
                break;
        }
       
       
    }
    #endregion

    #region mostrarpreguntas
    private void MostrarPreguntaMultiple(List<PreguntaMultiple> lista, ref bool vacioFlag, GameObject panel)
    {
        if (lista.Count == 0)
        {
            vacioFlag = true;
            tiposDisponibles.Remove(1);
            panel.SetActive(false);
            Debug.Log("No hay más preguntas múltiples.");
            mostrarPreguntas();
            
            return;
        }

        panel.SetActive(true);
        int i = UnityEngine.Random.Range(0, lista.Count);

        
       

        textPregunta.text = lista[i].Pregunta;
        textResp1.text = lista[i].Respuesta1;
        textResp2.text = lista[i].Respuesta2;
        textResp3.text = lista[i].Respuesta3;
        textResp4.text = lista[i].Respuesta4;
        respuestaPM = lista[i].RespuestaCorrecta;

        lista.RemoveAt(i);  // Eliminar la pregunta seleccionada
        Debug.Log("PM actuales" +lista.Count);
    }

    private void MostrarPreguntaAbierta(List<preguntaAbierta> lista, ref bool vacioFlag, GameObject panel)
    {
        if (lista.Count == 0)
        {
            vacioFlag = true;
            panel.SetActive(false);
            tiposDisponibles.Remove(2);
            Debug.Log("No hay más preguntas abiertas.");
            mostrarPreguntas();
            
            return;
        }

        panel.SetActive(true);
        int i = UnityEngine.Random.Range(0, lista.Count);



        
        textPreguntaA.text = lista[i].Pregunta;
        hahaha.text = lista[i].Respuesta;
        respuestaPM = lista[i].Respuesta;

        lista.RemoveAt(i);
        Debug.Log("PA actuales" + lista.Count);
    }
   


    private void MostrarPreguntaFV(List<preguntaFV> lista, ref bool vacioFlag, GameObject panel)
    {
        if (lista.Count == 0)
        {
            vacioFlag = true;
            panel.SetActive(false);
            tiposDisponibles.Remove(3);
            Debug.Log("No hay más preguntas falso verdadero.");
            mostrarPreguntas();
            
            
            return;
        }

        panel.SetActive(true);
        int i = UnityEngine.Random.Range(0, lista.Count);



        textPreguntaFV.text = lista[i].Pregunta;
        respuestaPM = lista[i].Respuesta;

        lista.RemoveAt(i);

        Debug.Log("PFV actuales" + lista.Count);
    }

    #endregion

    #region comprobacion de respuesta
    public void comprobarRespuesta1()
    {
        if (textResp1.text.Equals(respuestaPM))
        {
           PanelRC.SetActive(true);
           PanelRI.SetActive(false);
           RC += 1;
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            RI += 1;
        }
    }

    public void comprobarRespuesta2()
    {
        if (textResp2.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            RC += 1;
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            RI += 1;
        }
    }
    public void comprobarRespuesta3()
    {
        if (textResp3.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            RC += 1;
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            RI += 1;
        }
    }
    public void comprobarRespuesta4()
    {
        if (textResp4.text.Equals(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            RC += 1;
        }
        else
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            RI += 1;
        }
    }

    public void comprobarRespuestaFV(bool escogido) 
    {
        if (escogido == bool.Parse(respuestaPM))
        {
            PanelRC.SetActive(true);
            PanelRI.SetActive(false);
            RC += 1;
        }
        else 
        {
            PanelRC.SetActive(false);
            PanelRI.SetActive(true);
            RI += 1;
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
            PanelRC.SetActive(true);
            //PanelRI.SetActive(true);
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
            Debug.Log("PreguntasM: " + listaPreguntasMultiples.Count);
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
            Debug.Log("PreguntasA " + listaPreguntasAbiertas.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }

    public void LecturaPreguntasFV()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/preguntasFalso_Verdadero.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                string dificultad = lineaPartida[3];


                preguntaFV objPA = new preguntaFV(pregunta, respuesta, versiculo, dificultad);

                listaPreguntasFV.Add(objPA);

            }
            Debug.Log("PreguntasFV:  " + listaPreguntasFV.Count);
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
