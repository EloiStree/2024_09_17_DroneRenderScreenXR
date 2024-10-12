using UnityEngine;

public class POVMainCameraLookAtMono : MonoBehaviour
{

    public Transform m_targetToLookAt;
    public Transform m_anchorToMove;
    public Camera m_cameraToAffect;

    public bool m_useLerp = true;
    public float m_lerpSpeed = 1.0f;

    public AnimationCurve m_cameraFieldOfViewPercent = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public float m_cameraFieldOfViewMin = 20.0f;
    public float m_cameraFieldOfViewMax = 60.0f;
    public float m_minDistance=1;
    public float m_maxDistance=30;

    void LateUpdate()
    {

        if (m_targetToLookAt == null || m_anchorToMove == null)
            return;

        Vector3 whatToLookAt = m_targetToLookAt.position;
        Vector3 direction = whatToLookAt - m_anchorToMove.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        if (m_useLerp)
        {
            m_anchorToMove.rotation = Quaternion.Lerp(m_anchorToMove.rotation, toRotation, Time.deltaTime * m_lerpSpeed);
        }
        else
        {
            m_anchorToMove.rotation = toRotation;
        }

        float distance = Vector3.Distance(m_anchorToMove.position, m_targetToLookAt.position);
        float percent = Mathf.InverseLerp(m_minDistance, m_maxDistance, distance);
        float fieldOfView = Mathf.Lerp(m_cameraFieldOfViewMin, m_cameraFieldOfViewMax, m_cameraFieldOfViewPercent.Evaluate(percent));
        m_cameraToAffect.fieldOfView = fieldOfView;


        
    }
}
