using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public GameManager m_GameManager;

    private SocketManager m_SocketManager;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

        JoinCall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
    }

    public void JoinCall()
    {
    }

    public void LeaveCall()
    {
    }


}
