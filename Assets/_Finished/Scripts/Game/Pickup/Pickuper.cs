using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickuper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MultiTag"))
        {
            var MTags = other.GetComponent<MultiTag>();
        }
    }
}
