using System.Collections.Generic;
using UnityEngine;

public class Zona_MapaEmpatia : MonoBehaviour
{

    [Header("Materials")]
    public Material m_DefaultMaterial;
    public Material m_HoverMaterial;

    private Outline m_ZoneOutline;

    void Awake()
    {
        GetComponent<Renderer>().material = m_DefaultMaterial;

        m_ZoneOutline = GetComponent<Outline>();
        m_ZoneOutline.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PostIt>())
        {
            other.GetComponent<PostIt>().InZoneAnimation();
        }

        if(other.GetComponent<VRController>())
        {
            SetHighLight(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PostIt>())
        {
            other.GetComponent<PostIt>().OutZoneAnimation();
        }

        if (other.GetComponent<VRController>())
        {
            SetHighLight(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PostIt>())
        {
            PostIt postIt = other.GetComponent<PostIt>();

            if (!other.GetComponent<PostIt>().isInHand())
            {
                postIt.gameObject.transform.rotation = gameObject.transform.rotation;
                postIt.gameObject.transform.position = new Vector3(postIt.gameObject.transform.position.x, postIt.gameObject.transform.position.y, transform.position.z);
            }
        }
    }

    public void SetHighLight(bool value)
    {
        m_ZoneOutline.enabled = value;
    }
}
