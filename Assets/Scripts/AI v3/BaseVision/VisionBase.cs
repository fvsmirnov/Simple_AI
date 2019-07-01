using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisionBase : MonoBehaviour, IDetection
{
    public abstract GameObject Detect();
}
