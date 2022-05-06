using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Helpers.UI
{


public class TargetUiAim : MonoBehaviour
{
    public Transform rotateTarget;
     void Start()
    {
        
    }

      void Update()
    {
        rotateTarget.localEulerAngles = -transform.localEulerAngles;
    }
}
}