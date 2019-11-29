using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Help_VideoButtons : MonoBehaviour
{

    private VideoPlayer m_VideoManager;

    private void Awake()
    {
        m_VideoManager = GetComponent<VideoPlayer>();
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

    public void Pause()
    {
        if (m_VideoManager.isPlaying)
        {
            m_VideoManager.Pause();
        }
    }

    public void Play()
    {
        if (!m_VideoManager.isPlaying)
        { 
            m_VideoManager.Play();
        }
    }

    public void Stop()
    {
        m_VideoManager.Stop();
    }
}
