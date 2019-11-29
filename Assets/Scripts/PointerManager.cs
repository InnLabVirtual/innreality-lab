using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{

    [Header("Scene Objects")]
    public VR_Custom_Pointer m_Pointer;

    private GameObject m_PointerObject;

    private bool pointerState;

    public GameManager m_GameManager;

    private void Awake()
    {
        pointerState = false;
        m_PointerObject = m_Pointer.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameManager && pointerState && m_Pointer.m_Dot.gameObject.transform.hasChanged)
        {
            Vector3 pos = m_Pointer.m_Dot.gameObject.transform.position;
            m_GameManager.m_SocketManager.Action_WritePointerPosition(pos.x, pos.y, pos.z, pointerState);
            m_Pointer.m_Dot.gameObject.transform.hasChanged = false;
        }
    }

    public void SetPointerState()
    {
        if (m_PointerObject != null)
        {
            pointerState = !pointerState;
            m_PointerObject.SetActive(pointerState);

            if (m_GameManager)
            {
                Vector3 pos = m_Pointer.m_Dot.gameObject.transform.position;
                m_GameManager.m_SocketManager.Action_WritePointerPosition(pos.x, pos.y, pos.z, pointerState);
            }
        }
    }
}
