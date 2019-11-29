using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{

    [Header("Scene Objects")]
    public VR_Custom_Pointer m_Pointer;

    private GameObject m_PointerObject;

    private bool pointerState;

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
        
    }

    public void SetPointerState()
    {
        if (m_PointerObject != null)
        {
            pointerState = !pointerState;
            m_PointerObject.SetActive(pointerState);
        }
    }
}
