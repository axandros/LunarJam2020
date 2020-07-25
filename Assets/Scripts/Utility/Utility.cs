using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    static public bool ScreenRaycast(out RaycastHit raycastOut, LayerMask lm)
    {
        Ray ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool result = Physics.Raycast(ScreenRay, out raycastOut, Mathf.Infinity, lm);
        return result;
    }
}
