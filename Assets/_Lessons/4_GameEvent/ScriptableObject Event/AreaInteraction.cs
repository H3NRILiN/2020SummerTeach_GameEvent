using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaInteractionState
{
    PickFirst,
    PickSecond
}
public class AreaInteraction : MonoBehaviour
{
    [SerializeField] Transform m_DetectCenter;
    [SerializeField] float m_DetectRadius = 1;
    [SerializeField] LayerMask m_DetectLayers = 9;

    Vector3 m_DetectCenterP { get { return m_DetectCenter.position; } }
    AreaInteractionState m_CurrentState;
    AreaInteractObject m_CurrentInteracting;
    Collider[] m_DetectedObjects = new Collider[10];

    private void Start()
    {
        if (!m_DetectCenter) m_DetectCenter = this.transform;
        m_CurrentState = AreaInteractionState.PickFirst;
    }

    private void Update()
    {

    }

    void Interaction()
    {
        FindColsestObject(m_CurrentState);
    }

    AreaInteractObject FindColsestObject(AreaInteractionState targetState)
    {
        var count = Physics.OverlapSphereNonAlloc(
        m_DetectCenterP,
        m_DetectRadius,
        m_DetectedObjects,
        m_DetectLayers);

        AreaInteractObject closestObject = null;

        if (count > 0)
        {
            float distence = Mathf.Infinity;

            for (int i = 0; i < count; i++)
            {
                var interacting = AreaInteractObject.GetObject(m_DetectedObjects[i].transform);
                var currentDistence = Vector3.Distance(m_DetectCenterP, m_DetectedObjects[i].transform.position);

                if (!interacting || interacting.m_StateCanInteract != targetState)
                    continue;

                if (currentDistence < distence)
                {
                    distence = currentDistence;
                    closestObject = interacting;
                }
            }
        }

        return closestObject;
    }

    void OnDrawGizmos()
    {

    }
}
