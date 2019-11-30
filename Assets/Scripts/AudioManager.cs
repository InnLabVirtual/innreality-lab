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

    public ConnectAndJoin_Custom m_Voice;

    public ButtonCall m_VoiceInterface;

    public bool m_Recording { get; set;  }

    public bool m_CallState { get; set;  }

    public bool m_State { get; set; }

    public GameObject m_VRCamera;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        m_State = true;
        SetState();

        m_CallState = false;
        SetCallState();

        m_Recording = false;
        MuteState();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (m_VRCamera)
        {
            Vector3 pos = m_VRCamera.gameObject.transform.position;
            Vector3 newPost = new Vector3(pos.x, pos.y - 0.3f , pos.z + 0.3f);
            m_VoiceInterface.gameObject.transform.position = newPost;

            Vector3 relativePos = m_VoiceInterface.gameObject.transform.position - m_VRCamera.transform.position;
            Quaternion rot = Quaternion.LookRotation(relativePos, Vector3.up);
            m_VoiceInterface.gameObject.transform.rotation = rot;
        }
        */
    }

    private void OnDestroy()
    {
    }

    public void SetCallState()
    {
        m_CallState = !m_CallState;
        m_Voice.enabled = m_CallState;
    }

    public void MuteState()
    {
        m_Recording = !m_Recording;
        m_Recorder.TransmitEnabled = m_Recording;
    }

    public void SetState()
    {
        m_State = !m_State;
        m_VoiceInterface.SetState(m_State);
    }

}
