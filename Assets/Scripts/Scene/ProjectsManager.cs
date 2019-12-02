using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectsManager : MonoBehaviour
{
    private SocketManager m_SocketManager;

    public GameObject m_CanvasObject;

    public ButtonProject m_PrefabButtonProject;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SocketManager = SocketManager.instance;

        if (m_SocketManager && m_CanvasObject)
        {
            int i = 0;

            foreach (SocketManager.ProjectInUser s in m_SocketManager.m_CurrentUserData.projects)
            {
                ButtonProject button = Instantiate(m_PrefabButtonProject, new Vector3(0f, 0f, 0f), Quaternion.identity);
                button.gameObject.transform.SetParent(m_CanvasObject.transform);

                button.m_ProjectsManager = this;
                button.SetData(s);

                button.gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                button.gameObject.transform.localPosition = new Vector3(-249f - (110f * i), 0f, 0f);

                i++;
            }

            if (i == 0)
            {
                Debug.Log("No hay proyectos");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectProject(string id)
    {
        m_SocketManager.Action_SelectProject(id);
    }

    public void TrySignOut()
    {
        m_SocketManager.Action_SignOut();
        m_SocketManager.Action_ToLogin();
    }

}
