using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{

    [Header("UI Elements")]
    public TMP_InputField m_InputEmail;
    public TMP_InputField m_InputPass;

    private SocketManager m_SocketManager;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SocketManager = SocketManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryLoginEmailPass()
    {
        if (!m_InputEmail || !m_InputPass)
            return;

        m_SocketManager.Action_LoginEmailPass(m_InputEmail.text, m_InputPass.text);
        //m_SocketManager.Action_LoginEmailPass("san.or.gue@gmail.com", "santy0630");
    }
}
