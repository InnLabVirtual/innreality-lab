using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{

    public SocketManager.UserInProject m_UserData { get; set; }

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

            transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        }

        if (m_UserData.connected)
        {
            SetState(true);
        }
        else
        {
            SetState(false);
            transform.position = new Vector3(0, -3, 0);
        }

        if (m_UserData.name != null)
        {
            GetComponentInChildren<TMPro.TextMeshPro>().text = m_UserData.name;
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }
}
