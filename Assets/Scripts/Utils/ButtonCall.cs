using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCall : MonoBehaviour
{

    public Button m_ButtonCall;
    public Button m_ButtonOffCall;
    public Button m_ButtonMic;
    public Button m_ButtonOffMic;

    private bool m_State;

    private void Awake()
    {
        m_State = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool value)
    {
        gameObject.SetActive(value);
    }
}
