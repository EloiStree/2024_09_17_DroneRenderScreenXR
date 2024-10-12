using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoveCameraToSpotAngleMono : MonoBehaviour
{
    public Transform m_cameraToMove;
    public Camera m_camera;
    public DroneCameraSpotTagGroupMono m_groupToUse;
    public CameraSpotAngleMono m_defaultValue;

    public int m_currentSpotIndex = 0;

    public bool m_allowsFPV = true;
    public bool m_allowsTPS = true;
    [Header("Current")]
    public DroneCameraSpotTagMono m_currentSpot;
    public CameraSpotAngleMono m_currentAngle;
    public Transform m_currentToFollow;

    public UnityEvent<Transform> m_onTargetChanged;



    [ContextMenu("Next")]
    public void Next() { 
        SetWithIndex(m_currentSpotIndex + 1);
    }
    [ContextMenu("Previous")]
    public void Previous() {
        SetWithIndex(m_currentSpotIndex - 1);
    }
    private void Awake()
    {
        ReSelectCurrent();
    }
    public void ReSelectCurrent() { 
        SetWithIndex(m_currentSpotIndex);
    }


    public void SetWithIndex(int index) { 
    

        m_currentSpotIndex = index;
        if (m_groupToUse == null || m_groupToUse.GetLenght() <= 0)
            return;
        int count = m_groupToUse.GetLenght();


        if(m_currentSpotIndex >= count)
            m_currentSpotIndex = m_currentSpotIndex%count;

        m_currentSpot = m_groupToUse.GetSpot(m_currentSpotIndex);

        if (m_currentSpot != null) { 
            m_currentAngle = m_currentSpot.transform.GetComponentInChildren<CameraSpotAngleMono>();
            m_currentToFollow = m_currentSpot.GetTransform();
            if (m_currentAngle == null) { 
                m_currentAngle = m_defaultValue;
            }
        }
        if (m_camera!=null)
        {
            m_camera.fieldOfView = m_currentAngle.m_cameraAngle;
            m_camera.nearClipPlane = m_currentAngle.m_nearClipPlane;
            m_camera.farClipPlane = m_currentAngle.m_farClipPlane;
        }
        if (m_onTargetChanged != null)
            m_onTargetChanged.Invoke(m_currentToFollow);
        MoveCamera();
    }

    public void OnEnable()
    {
        StartCoroutine(CameraCoroutine());
    }

    private IEnumerator CameraCoroutine()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            MoveCamera();
        }
    }

    //public void Update()
    //{
    //    MoveCamera();
    //}

    //public void LateUpdate()
    //{

    //    MoveCamera();
    //}

    private void MoveCamera()
    {
        if (m_cameraToMove != null && m_currentToFollow != null)
        {
            m_cameraToMove.position = m_currentToFollow.position;
            m_cameraToMove.rotation = m_currentToFollow.rotation;
        }
    }


}

