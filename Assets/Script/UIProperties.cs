using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProperties : MonoBehaviour
{
    public GameObject PortalHint;
    public GameObject BoxHint;
    public GameObject MenuSkillHint;

    public GameObject getPortalHint()
    {
        return PortalHint;
    }
    public GameObject getBoxHint()
    {
        return BoxHint;
    }

    public GameObject getMenuSkillHint()
    {
        return MenuSkillHint;
    }
}
