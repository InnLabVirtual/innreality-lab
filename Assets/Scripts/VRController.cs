using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{

    [Header("Scene Objects")]
    public RadialMenu m_RadialMenu = null;

    [Header("Actions")]
    private SteamVR_Action_Boolean m_Touch = null;
    private SteamVR_Action_Boolean m_Press = null;
    private SteamVR_Action_Vector2 m_TouchPosition = null;

    private SteamVR_Action_Boolean m_GrabPinchAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;


    void Awake()
    {

        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_GrabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

        if (m_Pose.inputSource == SteamVR_Input_Sources.LeftHand)
        { 
            m_Touch = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ActivateMenu_Left");
            m_Press = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SelectMenu_Left");
            m_TouchPosition = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("MenuPosition_Left");
        }

        if (m_Pose.inputSource == SteamVR_Input_Sources.RightHand)
        {
            m_Touch = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ActivateMenu_Right");
            m_Press = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SelectMenu_Right");
            m_TouchPosition = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("MenuPosition_Right");
        }

        m_Touch.onChange += RadialMenu_Touch;
        m_Press.onStateUp += RadialMenu_PressRelease;
        m_TouchPosition.onAxis += RadialMenu_Position;
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        //for both hands
        if (m_GrabPinchAction.GetStateDown(m_Pose.inputSource))
        {
            GrabPostIt();
        }

        if (m_GrabPinchAction.GetStateUp(m_Pose.inputSource))
        {
            ReleasePostIt();
        }

        if (m_RadialMenu.gameObject.activeSelf)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z + 0.05f);
            m_RadialMenu.gameObject.transform.position = pos;
        }
    }

    private void OnDestroy()
    {
        m_Touch.onChange -= RadialMenu_Touch;
        m_Press.onStateUp -= RadialMenu_PressRelease;
        m_TouchPosition.onAxis -= RadialMenu_Position;
    }

    //Post It Interactions

    private GameObject collidingObject;//To keep track of what objects have rigidbodies
    private GameObject objectInHand;//To track the object you're holding

    void OnTriggerEnter(Collider other)//Activate function in trigger zone, checking rigidbodies and ignoring if no rigidbodies 
    {
        if (!other.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = other.gameObject;//If rigidbody, then assign object to collidingObject variable
    }

    void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    private void GrabPostIt() // Picking up object and assigning objectInHand variable
    {
        if (collidingObject != null && collidingObject.GetComponent<PostIt>())
        {
            objectInHand = collidingObject;
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;
            objectInHand.GetComponent<PostIt>().setInHand(true);
            objectInHand.transform.SetParent(transform);
        }
    }

    // Releasing object and disabling kinematic functionality so other forces can affect object
    private void ReleasePostIt()
    {
        if (objectInHand != null && objectInHand.GetComponent<PostIt>())
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.GetComponent<PostIt>().setInHand(false);
            objectInHand.transform.SetParent(null);
            objectInHand = null;
        }
    }

    /** Menu Radial**/

    private void RadialMenu_Position(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        m_RadialMenu.setTouchPosition(axis);
    }

    private void RadialMenu_Touch(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        m_RadialMenu.Show(newState);
    }

    private void RadialMenu_PressRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        m_RadialMenu.ActivateHighlightedSection();
    }
}