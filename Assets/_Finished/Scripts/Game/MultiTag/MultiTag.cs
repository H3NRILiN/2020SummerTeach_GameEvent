using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTag : MonoBehaviour
{
    public List<string> tags;

    private void Reset()
    {
        transform.tag = "MultiTag";
    }

    public bool Compare(string tag) => tags.Contains(tag);

}
