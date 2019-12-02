using System.Collections;
using UnityEngine;
using SocketIO;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class SocketManager : MonoBehaviour
{

    public static SocketManager instance { get; private set; }

    private SocketIOComponent m_SocketIo;

   // public GameObject m_PlayerPrefab;
   // public GameObject m_Player { get; set; }

    public UserData m_CurrentUserData { get; set; }
    public ProjectData m_CurrentProjectData { get; set; }
    public PostInProject[] m_PostsInProject { get; set; }
    public GameManager m_GameManager { get; set; }

    private void Awake()
    {

        m_SocketIo = GetComponent<SocketIOComponent>();
        StartCoroutine(ConnectToServer());

        if (!instance) { 
            instance = this;

        } else if (instance != this) { 
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SocketIo.On("LOGIN: success", OnRecieveUserData);
        m_SocketIo.On("LOGIN: failed", OnRecieveFailedUserData);

        m_SocketIo.On("PROJECT: cleaned", OnRecieveCleanedProject);
        m_SocketIo.On("PROJECT: selected", OnRecieveProjectData);
        m_SocketIo.On("PROJECT: user added", OnRecieveUsersAddedInProject);
        m_SocketIo.On("PROJECT: user changes", OnRecieveUsersChangesInProject);
        m_SocketIo.On("PROJECT: posts added", OnRecievePostsAddedInProject);
        m_SocketIo.On("PROJECT: posts changes", OnRecievePostsChangesInProject);
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    #region Commands

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
    }

    #endregion

    #region JSONMessages

    [Serializable]
    public class UserLogin
    {
        public string email;
        public string pass;
        public UserLogin(string email, string pass)
        {
            this.email = email;
            this.pass = pass;
        }

        public UserLogin()
        {

        }
    }

    [Serializable]
    public class UserData
    {
        public string name;
        public string uid;
        public ProjectInUser[] projects;

        public UserData(string name, string uid, ProjectInUser[] projects)
        {
            this.name = name;
            this.uid = uid;
            this.projects = projects;
        }
    }

    public class InvitationsData
    {
        public string project_id;
        public string id;

        public InvitationsData(string project_id, string id)
        {
            this.id = id;
            this.project_id = project_id;
        }
    }

    [Serializable]
    public class UserPosition
    {
        public string pos;

        public UserPosition(string pos)
        {
            this.pos = pos;
        }
    }

    [Serializable]
    public class ProjectData
    {
        public string name;
        public string id;

        public ProjectData(string name, string id)
        {
            this.name = name;
            this.id = id;
        }
    }

    [Serializable]
    public class UserInProject
    {
        public string uid;
        public string pos;
        public bool connected;
        public string name;
        public bool pointer;
        public string pointer_pos;

        public UserInProject(bool connected, string pos, string uid, string name, bool pointer, string pointer_pos)
        {
            this.connected = connected;
            this.pos = pos;
            this.uid = uid;
            this.name = name;
            this.pointer = pointer;
            this.pointer_pos = pointer_pos;
        }
    }

    [Serializable]
    public class PointerUser
    {
        public string pointer_pos;
        public bool pointer;

        public PointerUser(string pointer_pos, bool pointer)
        {
            this.pointer = pointer;
            this.pointer_pos = pointer_pos;
        }
    }

    [Serializable]
    public class ProjectInUser
    {
        public string id;
        public string name;

        public ProjectInUser(string id, string name)
        {
            this.name = name;
            this.id = id;
        }
    }

    [Serializable]
    public class PostInProject
    {
        public string autor;
        public string color;
        public string data;
        public string pos;
        public string rot;
        public string id;
        public string type;

        public PostInProject(string autor, string color, string data, string pos, string rot, string id, string type)
        {
            this.autor = autor;
            this.color = color;
            this.data = data;
            this.pos = pos;
            this.rot = rot;
            this.id = id;
            this.type = type;
        }
    }

    [Serializable]
    public class MSJ
    {
        public string msj;

        public MSJ(string msj)
        {
            this.msj = msj;
        }

        public MSJ()
        {

        }
    }
    #endregion

    #region Listening
    void OnRecieveUserData(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();
        UserData userData = JsonUtility.FromJson<UserData>(stringData);

        m_CurrentUserData = userData;

        SceneManager.LoadScene("Projects", LoadSceneMode.Single);
    }

    void OnRecieveProjectData(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();
        ProjectData projectData = JsonUtility.FromJson<ProjectData>(stringData);

        m_CurrentProjectData = projectData;
        
        Debug.Log("Project Readed: " + m_CurrentProjectData.name);

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    void OnRecievePostInProject(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();
        PostInProject[] postsData = JsonHelper.FromJson<PostInProject>(stringData);

        m_PostsInProject = postsData;
    }

    void OnRecieveUsersAddedInProject(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();

        UserInProject userData = JsonUtility.FromJson<UserInProject>(stringData);

        if (m_GameManager)
        {
            m_GameManager.AddUser(userData);
        }

        //Debug.Log("Agregado: " + userData.name);
    }

    void OnRecieveUsersChangesInProject(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();

        UserInProject userData = JsonUtility.FromJson<UserInProject>(stringData);

        if (m_GameManager)
        {
            m_GameManager.UpdateUser(userData);
        }

       // Debug.Log("Actualizado: " + userData.name);
    }

    void OnRecievePostsAddedInProject(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();

        PostInProject postsData = JsonUtility.FromJson<PostInProject>(stringData);

        if (m_GameManager)
        {
            m_GameManager.AddPost(postsData);
        }
    }

    void OnRecievePostsChangesInProject(SocketIOEvent socketIOEvent)
    {
        string stringData = socketIOEvent.data.ToString();

        PostInProject postsData = JsonUtility.FromJson<PostInProject>(stringData);

        if (m_GameManager)
        {
            m_GameManager.UpdatePost(postsData);
        }
    }

    void OnRecieveFailedUserData(SocketIOEvent socketIOEvent)
    {
        Debug.Log("Error with Login");
    }

    void OnRecieveCleanedProject(SocketIOEvent socketIOEvent)
    {
        SceneManager.LoadScene("Projects", LoadSceneMode.Single);

        string stringData = socketIOEvent.data.ToString();
        UserData userData = JsonUtility.FromJson<UserData>(stringData);

        m_CurrentUserData = userData;
    }

    #endregion

    #region Actions
    public void Action_LoginEmailPass(string email, string pass)
    {
        UserLogin login = new UserLogin(email, pass);
        string stringLogin = JsonUtility.ToJson(login);
        JSONObject loginJson = new JSONObject(stringLogin);

        m_SocketIo.Emit("USER: login", loginJson);
    }
    public void Action_SignOut()
    {
        MSJ msj = new MSJ("");
        string msjString = JsonUtility.ToJson(msj);
        JSONObject emptyMsj = new JSONObject(msjString);
        m_SocketIo.Emit("USER: sign out", emptyMsj);
    }
    public void Action_ToLogin()
    {
        SceneManager.LoadScene("Login", LoadSceneMode.Single);
    }
    public void Action_WritePlayerPosition(float x, float y, float z)
    {
        string pos = x + "/" + y + "/" + z;

        UserPosition position = new UserPosition(pos);
        string str = JsonUtility.ToJson(position);
        JSONObject json = new JSONObject(str);

        m_SocketIo.Emit("USER: update position", json);
    }

    public void Action_WritePointerPosition(float x, float y, float z, bool state)
    {
        string pos = x + "/" + y + "/" + z;

        PointerUser data = new PointerUser(pos, state);
        string str = JsonUtility.ToJson(data);
        JSONObject json = new JSONObject(str);

        m_SocketIo.Emit("USER: update pointer", json);
    }

    public void Action_SelectProject(string id)
    {
        MSJ m = new MSJ(id);
        Debug.Log("Selected Project ID: " + m.msj);

        string stringData = JsonUtility.ToJson(m);
        JSONObject json = new JSONObject(stringData);

        m_SocketIo.Emit("PROJECT: select", json);
    }
    public void Action_CleanProject()
    {
        MSJ m = new MSJ("");

        string stringData = JsonUtility.ToJson(m);
        JSONObject json = new JSONObject(stringData);

        m_SocketIo.Emit("PROJECT: clean", json);
    }
    public void Action_UpdatePost(PostInProject data)
    {
        string str = JsonUtility.ToJson(data);
        JSONObject json = new JSONObject(str);

        m_SocketIo.Emit("POST: update", json);
    }

    #endregion

    #region Helper
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    #endregion

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
        }

        if (scene.name == "Login")
        {
            m_CurrentProjectData = null;
            m_CurrentUserData = null;
        }

        if (scene.name == "Projects")
        {
            m_CurrentProjectData = null;
        }

        Debug.Log("OnSceneLoaded: " + scene.name);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
    }

    
}
