using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour
{

    public SocketManager m_SocketManager { get; set; }

    public Camera m_Camera;

    public Other m_OtherPrefab;

    public PlayerData m_PlayerPrefab;
    public PlayerData m_PlayerVRReference;

    public GameObject m_Teleport;
    public GameObject m_VRSystem;

    public PostIt m_PostItPrefab;
    public GameObject m_PostItContext;

    public bool m_VRState;

    public GameObject m_TempPlayerObject;

    public PlayerData m_Player { get; set; }
    public List<Other> m_Others { get; set; }
    public List<PostIt> m_Posts { get; set; }
    public List<SocketManager.PostInProject> m_PostsFolder { get; set; }

    public ConnectAndJoin_Custom m_Voice;

    private void Awake()
    {
        m_Others = new List<Other>();
        m_Posts = new List<PostIt>();
        m_PostsFolder = new List<SocketManager.PostInProject>();
        m_Camera.gameObject.SetActive(true);

        if (m_VRState)
        {
            m_VRSystem.SetActive(true);
            m_Teleport.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SocketManager = SocketManager.instance;
        m_SocketManager.m_GameManager = this;

        m_Voice.m_RoomName = m_SocketManager.m_CurrentProjectData.id;
        m_Voice.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //SET ORIENTATION TO USER
        if (m_Player)
        {
            foreach (Other user in m_Others)
            {
                if (user.m_UserData.connected)
                {
                    Vector3 relativePos = user.gameObject.transform.position - m_Player.transform.position;

                    // the second argument, upwards, defaults to Vector3.up
                    Quaternion rot = Quaternion.LookRotation(relativePos, Vector3.up);
                    user.gameObject.transform.rotation = rot;
                }
            }
        }
        
    }

    public void UpdateUser(SocketManager.UserInProject newUser)
    {
        foreach (Other user in m_Others)
        {
            if (newUser.uid == user.m_UserData.uid)
            {
                user.SetData(newUser);
            }
        }
    }

    public void AddUser(SocketManager.UserInProject newUser)
    {

        if (newUser.uid == m_SocketManager.m_CurrentUserData.uid)
        {

            string[] pos = newUser.pos.Split(char.Parse("/"));

            float x = float.Parse(pos[0]);
            float y = float.Parse(pos[1]);
            float z = float.Parse(pos[2]);

            PlayerData temp;

            if (m_VRState)
            {
                temp = m_PlayerVRReference;
                temp.gameObject.transform.position = new Vector3(x, y, z);
                temp.gameObject.SetActive(true);
            }
            else
            {
                temp = Instantiate(m_PlayerPrefab, new Vector3(x, y, z), Quaternion.identity);
            }

            temp.SetData(newUser);
            temp.m_GameManager = this;

            m_Player = temp;

            m_Camera.gameObject.SetActive(false);
            return;
        }

        Other newOther = Instantiate(m_OtherPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
        newOther.SetData(newUser);

        m_Others.Add(newOther);
    }

    public void UpdatePost(SocketManager.PostInProject newPost)
    {
        foreach (PostIt post in m_Posts)
        {
            if (post.m_Data.id == newPost.id)
            {
                post.SetData(newPost);
            }
        }
    }

    public void SetFolderPostIt(SocketManager.PostInProject newPost)
    {
        m_PostsFolder.Remove(newPost);

        Vector3 posV = m_TempPlayerObject.transform.position;
        Quaternion rotQ = m_TempPlayerObject.transform.rotation;

        PostIt post = Instantiate(m_PostItPrefab, posV, rotQ, null);
        post.m_SocketManager = m_SocketManager;

        string pos = posV.x + "/" + posV.y + "/" + posV.z;
        string rot = rotQ.eulerAngles.x + "/" + rotQ.eulerAngles.y + "/" + rotQ.eulerAngles.z;

        newPost.pos = pos;
        newPost.rot = rot;
        newPost.type = "none";

        m_SocketManager.Action_UpdatePost(newPost);

        post.SetData(newPost);
        m_Posts.Add(post);

        
    }

    public void AddPost(SocketManager.PostInProject newPost)
    {
        if (newPost.type == "in")
        {
            m_PostsFolder.Add(newPost);
        }
        if (newPost.type == "none")
        {
            PostIt post = Instantiate(m_PostItPrefab, new Vector3(0, 0, 0), Quaternion.identity, null);
            post.m_SocketManager = m_SocketManager;
            post.SetData(newPost);

            m_Posts.Add(post);
        }
        if (newPost.type == "vr" && newPost.autor == m_Player.m_UserData.uid)
        {
            Vector3 posV = m_TempPlayerObject.transform.position;
            Quaternion rotQ = m_TempPlayerObject.transform.rotation;

            PostIt post = Instantiate(m_PostItPrefab, posV, rotQ, null);
            post.m_SocketManager = m_SocketManager;
            post.SetData(newPost);

            string pos = posV.x + "/" + posV.y + "/" + posV.z;
            string rot = rotQ.eulerAngles.x + "/" + rotQ.eulerAngles.y + "/" + rotQ.eulerAngles.z;

            post.m_Data.pos = pos;
            post.m_Data.rot = rot;

            post.m_Data.type = "none";

            m_Posts.Add(post);

            m_SocketManager.Action_UpdatePost(post.m_Data);
        }
    }

    public void TryCleanProject()
    {
        m_SocketManager.Action_CleanProject();
    }

    private void OnDestroy()
    {
        m_SocketManager.m_GameManager = null;
    }
}
