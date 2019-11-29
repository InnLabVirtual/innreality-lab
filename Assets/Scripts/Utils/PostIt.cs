using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PostIt : MonoBehaviour
{

    public GameObject m_WorldContextMenu { get; set; }

    private Animator m_Animator;

    private bool inHand;

    private Outline m_CubeOutline;

    public SocketManager m_SocketManager { get; set; }

    public SocketManager.PostInProject m_Data { get; set; }

    private void Awake()
    {
        inHand = false;
        m_Animator = GetComponent<Animator>();

        m_CubeOutline = GetComponentInChildren<Outline>();
        m_CubeOutline.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator.Play("PostIt_Start");
    }

    // Update is called once per frame
    void Update()
    {

        //interactions hand off
        if (!inHand)
        {

            //change the y position
            if (transform.position.y < 0.6f)
            {
                transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
            }
        }

        if (transform.hasChanged && m_SocketManager && inHand)
        {

            Vector3 posV = transform.position;
            Quaternion rotQ = transform.rotation;

            string pos = posV.x + "/" + posV.y + "/" + posV.z;
            string rot = rotQ.eulerAngles.x + "/" + rotQ.eulerAngles.y + "/" + rotQ.eulerAngles.z;

            m_Data.pos = pos;
            m_Data.rot = rot;

            m_SocketManager.Action_UpdatePost(m_Data);

            transform.hasChanged = false;
        }

    }
    public void SetState(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetData(SocketManager.PostInProject data)
    {
        m_Data = data;

        string[] pos = new string[3];
        string[] rot = new string[3];

        if (!inHand && m_Data.pos != null)
        {
            pos = data.pos.Split(char.Parse("/"));

            transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        }

        if (!inHand && m_Data.rot != null)
        {
            rot = data.rot.Split(char.Parse("/"));

            transform.rotation = Quaternion.Euler(float.Parse(rot[0]), float.Parse(rot[1]), float.Parse(rot[2]));
        }

        if (m_Data.color != null)
        {
            ChangeMaterialOnStart(m_Data.color);
        }

        if (m_Data.data != null)
        {
            GetComponentInChildren<TMPro.TextMeshPro>().text = m_Data.data;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VRController>())
        {
            SetHighLight(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<VRController>())
        {
            SetHighLight(false);
        }
    }

    public void InZoneAnimation()
    {
        m_Animator.Play("PostIt_InZone");
    }

    public void OutZoneAnimation()
    {
        m_Animator.Play("PostIt_OutZone");
    }
    public void ChangeMaterialOnStart(string i)
    {
        Material postItMaterial = Resources.Load("Materials/Post/" + i +"", typeof(Material)) as Material;
        GetComponentsInChildren<Renderer>()[0].material = postItMaterial;
    }

    public void SetHighLight(bool value)
    {
        m_CubeOutline.enabled = value;
    }

    public void setInHand(bool inHand)
    {
        this.inHand = inHand;
    }

    public bool isInHand()
    {
        return inHand;
    }

}
