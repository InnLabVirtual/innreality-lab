using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

public class AudioManager : MonoBehaviour
{

    public GameManager m_GameManager;

    private SocketManager m_SocketManager;

    public Recorder m_Recorder;

    public VoiceConnection m_Voice;

    public bool m_Recording { get; set;  }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Recording = false;
        MuteState();

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

    public void MuteState()
    {
        m_Recording = !m_Recording;
        m_Recorder.TransmitEnabled = m_Recording;
    }


}
