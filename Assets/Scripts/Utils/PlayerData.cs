using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameManager m_GameManager { get; set; }

    public SocketManager.UserInProject m_UserData { get; set; }

    private Rigidbody m_RigidBody;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //UPDATE USER WHEN CHANGED
        if (m_UserData != null && m_GameManager && transform.hasChanged)
        {
            Vector3 pos = transform.position;
            m_GameManager.m_SocketManager.Action_WritePlaterPosition(pos.x, pos.y, pos.z);
            transform.hasChanged = false;
        }
    }

    public void SetData(SocketManager.UserInProject data)
    {
        m_UserData = data;
    }

}
