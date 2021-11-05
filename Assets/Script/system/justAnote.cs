using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justAnote : MonoBehaviour
{
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "This component shouldn't be removed, it does important Dev.";
}
