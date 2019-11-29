using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonProject : MonoBehaviour
{
    public Button m_ProjectButton { get; set; }

    public TextMeshProUGUI m_InputText;
    public TextMeshProUGUI m_InputTextHover;

    public SocketManager.ProjectInUser m_Data { get; set; }

    public ProjectsManager m_ProjectsManager { get; set; }

    private void Awake()
    {
        m_ProjectButton = GetComponentInChildren<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_ProjectButton.onClick.AddListener(OnClickProject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(SocketManager.ProjectInUser newData)
    {
        m_Data = newData;

        m_InputText.text = m_Data.name;
        m_InputTextHover.text = m_Data.name;
    }

    public void OnClickProject()
    {
        if(m_Data == null)
        {
            return;
        }
        m_ProjectsManager.SelectProject(m_Data.id);
    }



    private void OnDestroy()
    {
        
    }



}
