using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MenuManager : MonoBehaviour
{

    [Header("Scene Objects")]
    public RadialMenu m_RadialMenu = null;

    [Header("Actions")]
    private SteamVR_Action_Boolean m_Touch = null;
    private SteamVR_Action_Boolean m_Press = null;
    private SteamVR_Action_Vector2 m_TouchPosition = null;

    private void Awake()
    {
        m_Touch = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ActivateMenu");
        m_Press = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SelectMenu");
        m_TouchPosition = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("MenuPosition");

        m_Touch.onChange += RadialMenu_Touch;
        m_Press.onStateUp += RadialMenu_PressRelease;
        m_TouchPosition.onAxis += RadialMenu_Position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        m_Touch.onChange -= RadialMenu_Touch;
        m_Press.onStateUp -= RadialMenu_PressRelease;
        m_TouchPosition.onAxis -= RadialMenu_Position;
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
