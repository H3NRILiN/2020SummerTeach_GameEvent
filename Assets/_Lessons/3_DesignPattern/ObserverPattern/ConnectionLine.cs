using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLine : MonoBehaviour
{
    [SerializeField] LineRenderer m_LineRenderer;
    public Transform m_StartPoint;
    public Transform m_EndPoint;
    [SerializeField] int m_Steps = 10;
    [SerializeField] float m_MidYOffset = -1;
    [SerializeField] Color m_NormalColor;
    [SerializeField] Color m_ProcessColor;
    [Range(0, 1)] public float m_Process;
    Vector3 m_MidPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_StartPoint || !m_EndPoint || !m_LineRenderer)
            return;

        m_MidPoint = (m_StartPoint.position + m_EndPoint.position) / 2;
        m_MidPoint.y += m_MidYOffset;
        DrawQuadraticBezierCurve(m_LineRenderer, m_Steps, m_StartPoint.position, m_MidPoint, m_EndPoint.position);

        Gradient gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(m_ProcessColor, m_Process), new GradientColorKey(m_NormalColor, 1) },
                         new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) });
        gradient.mode = GradientMode.Fixed;


        var colorGradient = m_LineRenderer.colorGradient;
        colorGradient.colorKeys[0].color = m_ProcessColor;
        m_LineRenderer.colorGradient = gradient;
    }

    //來源: https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/
    void DrawQuadraticBezierCurve(LineRenderer lineRenderer, int steps, Vector3 point0, Vector3 point1, Vector3 point2)
    {
        lineRenderer.positionCount = steps;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            //B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            B = Vector3.Lerp(Vector3.Lerp(point0, point1, t), Vector3.Lerp(point1, point2, t), t);
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
        }
    }
}
