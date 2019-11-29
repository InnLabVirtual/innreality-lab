using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help_MapaEmpatia : MonoBehaviour
{

    public GameObject m_Img, m_Video, m_Sample;

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

    public void SetPart(int i)
    {
        switch (i)
        {
            case 0:
                m_Img.SetActive(true);
                m_Video.SetActive(false);
                m_Sample.SetActive(false);
                break;
            case 1:
                m_Img.SetActive(false);
                m_Video.SetActive(true);
                m_Sample.SetActive(false);
                break;
            case 2:
                m_Img.SetActive(false);
                m_Video.SetActive(false);
                m_Sample.SetActive(true);
                break;
        }
    }
}
