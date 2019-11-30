using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostItButton : MonoBehaviour
{

    public SocketManager.PostInProject m_Data { get; set; }

    private Image m_Image;
    private Button m_Button;
    private TextMeshProUGUI m_Text;

    public GameManager m_GameManager { get; set; }
    public FolderAction m_Folder { get; set; }

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Button = GetComponent<Button>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(SocketManager.PostInProject newData)
    {
        m_Data = newData;

        if (m_Data.data != null)
        {
            m_Text.text = m_Data.data;
        }

        if (m_Data.color != null)
        {
            LoadColor(m_Data.color);
        }

        m_Button.onClick.AddListener(OnClickCreate);
    }

    public void OnClickCreate()
    {
        m_GameManager.SetFolderPostIt(m_Data);
        m_Folder.LoadPosts();
    }

    public void LoadColor(string c)
    {
        if (c == "1")
        {
            ColorBlock block = m_Button.colors;
            block.normalColor = new Color32(255, 160, 190, 255);
            block.highlightedColor = new Color32(245, 245, 245, 255);
            block.pressedColor = new Color32(200, 200, 200, 200);
            m_Button.colors = block;
            return;
        }
        if (c == "2")
        {
            ColorBlock block = m_Button.colors;
            block.normalColor = new Color32(229, 170, 255, 255);
            block.highlightedColor = new Color32(245, 245, 245, 255);
            block.pressedColor = new Color32(200, 200, 200, 200);
            m_Button.colors = block;
            return;
        }
        if (c == "3")
        {
            ColorBlock block = m_Button.colors;
            block.normalColor = new Color32(162, 255, 198, 255);
            block.highlightedColor = new Color32(245, 245, 245, 255);
            block.pressedColor = new Color32(200, 200, 200, 200);
            m_Button.colors = block;
            return;
        }
        if (c == "4")
        {
            ColorBlock block = m_Button.colors;
            block.normalColor = new Color32(182, 210, 255, 255);
            block.highlightedColor = new Color32(245, 245, 245, 255);
            block.pressedColor = new Color32(200, 200, 200, 200);
            m_Button.colors = block;
            return;
        }
        if (c == "0")
        {
            ColorBlock block = m_Button.colors;
            block.normalColor = new Color32(255, 248, 160, 255);
            block.highlightedColor = new Color32(245, 245, 245, 255);
            block.pressedColor = new Color32(200, 200, 200, 200);
            m_Button.colors = block;
            return;
        }
    }
}
