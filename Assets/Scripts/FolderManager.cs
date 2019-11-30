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
        if (m_Hand)
        {
            Vector3 pos = m_Hand.gameObject.transform.position;

            Vector3 newPost = new Vector3(pos.x, pos.y + 0.4f, pos.z + 0.1f);

            m_Folder.gameObject.transform.position = newPost;

        }

        if (m_Folder.gameObject.activeSelf && m_VRCamera)
        {
                Vector3 relativePos = m_Folder.gameObject.transform.position - m_VRCamera.transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rot = Quaternion.LookRotation(relativePos, Vector3.up);
                m_Folder.gameObject.transform.rotation = rot;
        }
    }

    public void SetFolder()
    {
        m_FolderState = !m_FolderState;
        m_Folder.SetState(m_FolderState);
    }


}
