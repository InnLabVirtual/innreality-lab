using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FolderAction : MonoBehaviour
{

    public GameObject m_Container;

    public RectTransform[] m_Locations;
    public PostItButton m_PrefabPostItButton;
    public List<PostItButton> m_PostItFolder { get; set; }

    public GameManager m_GameManager;

    public Button m_PevButton;
    public Button m_NextButton;

    public TextMeshProUGUI m_Text;

    public int m_IndexPage { get; set; }

    private void Awake()
    {
        m_IndexPage = 0;
        m_Text.text = "Página " + m_IndexPage;

        m_PostItFolder = new List<PostItButton>();
    }

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        m_PevButton.onClick.AddListener(PrevPage);
        m_NextButton.onClick.AddListener(NextPage);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextPage()
    {
        m_IndexPage++;
        m_Text.text = "Página " + m_IndexPage;
        LoadPosts();
    }

    public void PrevPage()
    {

        m_IndexPage--;
        if (m_IndexPage <= 0)
        {
            m_IndexPage = 0;
        }
        m_Text.text = "Página " + m_IndexPage;
        LoadPosts();
    }

    public void LoadPosts()
    {

        foreach (PostItButton p in m_PostItFolder)
        {
            Destroy(p.gameObject);
        }

        m_PostItFolder = new List<PostItButton>();

        for (int i = 0; i < m_Locations.Length; i++)
        {
            int ii = i + (m_Locations.Length * m_IndexPage);
            if (m_GameManager.m_PostsFolder.Count > ii)
            {

                SocketManager.PostInProject postIt = m_GameManager.m_PostsFolder[ii];

                PostItButton m_PostButton = Instantiate(m_PrefabPostItButton, m_Container.gameObject.transform);

                m_PostButton.GetComponent<RectTransform>().anchoredPosition = m_Locations[i].anchoredPosition;
                m_PostButton.m_GameManager = m_GameManager;
                m_PostButton.m_Folder = this;
                m_PostButton.SetData(postIt);

                m_PostItFolder.Add(m_PostButton);
            }
        }
    }

    public void SetState(bool value)
    {
        gameObject.SetActive(value);
        if (value)
        {
            LoadPosts();
        }
    }

    private void OnDisable()
    {
        m_IndexPage = 0;
        m_Text.text = "Página " + m_IndexPage;
    }
}
