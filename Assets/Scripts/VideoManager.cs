using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class VideoManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Quad that is parented to controller and renders video")]
    public GameObject m_VideoQuad;

    [Header("Scene Objects")]
    public FrontCamera m_FrontCamera;

    public GameObject m_PrefabPostIt;
    public GameObject m_WorldContextObject;

    private bool cameraState;

    private void Awake()
    {
        cameraState = false;
        SetCameraVideoState(false);
    }

    void Start()
    {
    }

    private void Update()
    {
    }

    public void SetCameraVideo()
    {
        cameraState = !cameraState;
        SetCameraVideoState(cameraState);
    }

    private void SetCameraVideoState(bool value)
    {
        m_VideoQuad.SetActive(value);
        m_FrontCamera.enabled = value;
        m_FrontCamera.gameObject.SetActive(value);

        Debug.Log("Camara");
    }
}