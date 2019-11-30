using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{

    public SocketManager.UserInProject m_UserData { get; set; }

    public GameObject m_Dot;
    private LineRenderer m_Line;

    public GameObject m_Container;

    private void Awake()
    {
        m_Line = GetComponent<LineRenderer>();
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

    public void SetData(SocketManager.UserInProject data)
    {
        m_UserData = data;

        string[] pos = new string[3];

        if (m_UserData.pos != null)
        {
            pos = data.pos.Split(char.Parse("/"));

            m_Container.transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        }

        if (m_UserData.connected)
        {
            SetState(true);
        }
        else
        {
            SetState(false);
            m_Container.transform.position = new Vector3(0, -3, 0);
        }

        if (m_UserData.name != null)
        {
            m_Container.GetComponentInChildren<TMPro.TextMeshPro>().text = m_UserData.name;
        }

        if (m_UserData.pointer)
        {
            m_Dot.gameObject.SetActive(true);
            m_Line.enabled = true;

            string[] p = data.pointer_pos.Split(char.Parse("/"));

            m_Dot.gameObject.transform.position = new Vector3(float.Parse(p[0]), float.Parse(p[1]), float.Parse(p[2]));

            m_Line.SetPosition(0, m_Container.transform.position);
            m_Line.SetPosition(1, m_Dot.gameObject.transform.position);
        }
        else
        {
            m_Dot.gameObject.SetActive(false);
            m_Line.enabled = false;
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }
}
