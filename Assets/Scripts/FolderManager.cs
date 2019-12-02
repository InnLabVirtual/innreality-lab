using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{

    public FolderAction m_Folder;
    public VRController m_Hand;

    public GameObject m_VRCamera;

    public bool m_FolderState { get; set; }

    private void Awake()
    {
        m_FolderState = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetFolder()
    {
        m_FolderState = !m_FolderState;
        m_Folder.SetState(m_FolderState);
    }


}
