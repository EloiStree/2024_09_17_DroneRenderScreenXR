
using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneCameraSpotTagMono : MonoBehaviour
{

    public static List<DroneCameraSpotTagMono> m_allActiveInScene = new List<DroneCameraSpotTagMono>();
    public static List<TPSDroneCameraSpotTagMono> m_tpsActiveInScene = new List<TPSDroneCameraSpotTagMono>();
    public static List<FPVDroneCameraSpotTagMono> m_fpvActiveInScene = new List<FPVDroneCameraSpotTagMono>();


    [SerializeField] Transform m_currentSpot;

    public Transform GetTransform() { return m_currentSpot; }
    public Vector3 GetWorldPosition() { return m_currentSpot.position; }
    public Quaternion GetWorldRotation() { return m_currentSpot.rotation; }
    public bool IsActiveInHierarchy() { return m_currentSpot.gameObject.activeInHierarchy; }
    void Reset()
    {
        m_currentSpot = transform;

    }
    private void OnEnable()
    {
        if (this is TPSDroneCameraSpotTagMono)
            m_tpsActiveInScene.Add(this as TPSDroneCameraSpotTagMono);
        if (this is FPVDroneCameraSpotTagMono)
            m_fpvActiveInScene.Add(this as FPVDroneCameraSpotTagMono);
        m_allActiveInScene.Add(this);

    }
    private void OnDisable()
    {
        if (this is TPSDroneCameraSpotTagMono)
            m_tpsActiveInScene.Remove(this as TPSDroneCameraSpotTagMono);
        if (this is FPVDroneCameraSpotTagMono)
            m_fpvActiveInScene.Remove(this as FPVDroneCameraSpotTagMono);
        m_allActiveInScene.Remove(this);
    }
}

