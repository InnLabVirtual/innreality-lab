using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt_Ambiente : MonoBehaviour
{

    private void Awake()
    {

        string[] data = new string[15];

        data[0] = "Da mucho estrés";
        data[1] = "Se demora en llegar";
        data[2] = "Es muy costoso";
        data[3] = "Le hace sentir importante";
        data[4] = "Hay mucho ruido";
        data[5] = "No conoce los medios";
        data[6] = "Es dificil de usar";
        data[7] = "Necesita ahorrar tiempo";
        data[8] = "Quiere ganar más dinero";
        data[9] = "Le aburre";
        data[10] = "Requiere de acompañamiento";
        data[11] = "No tiene tanto dinero";
        data[12] = "Es muy caro";
        data[13] = "Le queda lejos";
        data[14] = "Madruga todos los días";

        int rColor = Mathf.RoundToInt(Random.Range(0, 4f));

        int rData = Mathf.RoundToInt(Random.Range(0, 14f));

        ChangeMaterialOnStart(rColor + "");
        ChangeTextOnStart(data[rData] + "");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeMaterialOnStart(string i)
    {
        Material postItMaterial = Resources.Load("Materials/Ambient/Post/" + i + "", typeof(Material)) as Material;
        GetComponentsInChildren<Renderer>()[0].material = postItMaterial;
    }

    private void ChangeTextOnStart(string s)
    {
        GetComponentsInChildren<TMPro.TextMeshPro>()[0].text = s;
    }
}
