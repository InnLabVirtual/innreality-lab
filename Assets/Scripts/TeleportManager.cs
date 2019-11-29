using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{

    public bool pressed, state, released;

    private int framesPressed, framesReleased;


    // Start is called before the first frame update
    void Start()
    {
        framesPressed = 0;
        framesReleased = 0;

        pressed = false;
        state = false;
        released = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            framesPressed++;
            if (framesPressed > 1)
            {
                framesPressed = 0;
                pressed = false;
            }
        }

        if (released)
        {
            framesReleased++;
            if (framesReleased > 1)
            {
                framesReleased = 0;
                released = false;
            }
        }

    }

    public void ChangeStateTeleport()
    {
        if (state)
        {
            released = true;
            state = false;
            return;
        }
        else
        {
            pressed = true;
            state = true;
            return;
        }
    }


}
