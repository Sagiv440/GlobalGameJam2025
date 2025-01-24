using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SmartSwitch
{
    private bool old_status;
    private bool new_status;

    public SmartSwitch(bool status = false)
    {
        Debug.Log("creation");
        old_status = status;
        new_status = status;
    }

    public void Update(bool state)
    {
        old_status = new_status;
        new_status = state;
    }
    // return true if Switch is Pressed (if the 'new_status = True' and 'the old_status = False')
    public bool OnPress() => new_status && !old_status;

    //return true if Switch is Held (if the current status = True)
    public bool OnHold() => new_status;

    // return true if Switch was Pressed (if the 'new_status = False' and 'the old_status = True')
    public bool OnRelese() => !new_status && old_status;

    // return true if Switch was Pressed or Relesed (if the 'new_state != old_status')
    public bool OnEvent() => new_status != old_status;
}
