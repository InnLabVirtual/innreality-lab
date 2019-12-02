using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{

    public GameObject[] m_Interfaces;

    public GameObject m_VRCamera;
    public VRController m_Hand;

    public GameObject m_TempContainer;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        CleanInterfaces();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Hand)
        {
            Vector3 pos = m_Hand.gameObject.transform.position;

            Vector3 newPost = new Vector3(pos.x, pos.y + 0.4f, pos.z + 0.1f);

            m_TempContainer.gameObject.transform.position = newPost;
        }

        if (m_TempContainer.gameObject.activeSelf && m_VRCamera)
        {
            Vector3 relativePos = m_TempContainer.gameObject.transform.position - m_VRCamera.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rot = Quaternion.LookRotation(relativePos, Vector3.up);
            m_TempContainer.gameObject.transform.rotation = rot;
        }
    }

    public void SetInterfaces(int index)
    {
        CleanInterfaces();

        m_Interfaces[index].SetActive(true);
    }

    public void CleanInterfaces()
    {
        for (int i = 0; i < m_Interfaces.Length; i++)
        {
            m_Interfaces[i].SetActive(false);
        }
    }
}
