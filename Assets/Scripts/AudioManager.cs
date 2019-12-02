using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

public class AudioManager : MonoBehaviour
{

    public GameManager m_GameManager;

    public Recorder m_Recorder;

    public ConnectAndJoin_Custom m_Voice;

    public bool m_Recording { get; set;  }

    public bool m_CallState { get; set;  }

    public bool m_State { get; set; }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CallState = false;
        SetCallState();

        m_Recording = false;
        MuteState();
    }

    // Update is called once per frame
    void Update() { 
    }

    private void OnDestroy()
    {
    }

    public void SetCallState()
    {
        m_CallState = !m_CallState;
      //  m_Voice.enabled = m_CallState;
    }

    public void MuteState()
    {
        m_Recording = !m_Recording;
      //  m_Recorder.TransmitEnabled = m_Recording;
    }

}
