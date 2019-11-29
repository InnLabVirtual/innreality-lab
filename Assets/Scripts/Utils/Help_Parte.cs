using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Help_Parte : MonoBehaviour
{

    public GameObject m_HelpObject;

    public GameObject m_SectionObject;

    private Button m_Button;
    private TextMeshProUGUI m_Text;

    private bool m_Active;

    void Awake()
    {
        m_Button = GetComponentInChildren<Button>();

        m_Button.onClick.AddListener(taskOnClick);

        //make the object false in start
        m_HelpObject.SetActive(false);

        //make the title true in start
        m_SectionObject.SetActive(true);

        //initial values
        m_Text = GetComponentInChildren<TextMeshProUGUI>();
        m_Text.text = "?";
        m_Active = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void taskOnClick()
    {
        m_HelpObject.SetActive(!m_HelpObject.activeSelf);
        m_SectionObject.SetActive(!m_SectionObject.activeSelf);
        m_Active = !m_Active;

        if (m_Active)
        {
            m_Text.text = "x";
        }
        else
        {
            m_Text.text = "?";
        }
    }
}
