using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help_MapaEmpatia : MonoBehaviour
{

    public GameObject[] m_Helps;

    private void Awake()
    {
        SetPart(0);
    }

    private void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPart(int index)
    {
        for (int i = 0; i < m_Helps.Length; i++)
        {
            m_Helps[i].SetActive(false);
        }

        m_Helps[index].SetActive(true);
    }
}
