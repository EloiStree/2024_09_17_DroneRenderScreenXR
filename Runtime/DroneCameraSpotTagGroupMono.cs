using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneCameraSpotTagGroupMono :MonoBehaviour {
    public List<DroneCameraSpotTagMono> m_group = new List<DroneCameraSpotTagMono>();
    public void GetSpots(out List<DroneCameraSpotTagMono> spots)
    {
        m_group = m_group.Distinct().ToList();
        spots = m_group;
    }

    [ContextMenu("Add All Active")]
    public void AddAllSpotActive() {

        AddAllActiveFPV();
        AddAllActiveTPS();
    }

    [ContextMenu("Add All Active TPS")]
    public void AddAllActiveTPS()
    {
        
        m_group.AddRange(GameObject.FindObjectsByType<TPSDroneCameraSpotTagMono>(
            FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID));

    }

    [ContextMenu("Add All Active FPVS")]
    public void AddAllActiveFPV()
    {

        m_group.AddRange(GameObject.FindObjectsByType<FPVDroneCameraSpotTagMono>(
            FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID));
    }

    [ContextMenu("Remove TPS")]
    public void RemoveTPS()
    {
        m_group = m_group.Where(x => !(x is TPSDroneCameraSpotTagMono)).ToList();
    }
    [ContextMenu("Remove FPV")]
    public void RemoveFPV()
    {
        m_group = m_group.Where(x => !(x is FPVDroneCameraSpotTagMono)).ToList();
    }


    public int GetLenght()
    {
        if(m_group== null)
            return 0;   
        return m_group.Count;
    }

    public DroneCameraSpotTagMono GetSpot(int m_currentSpotIndex)
    {
        int c = GetLenght();
        if (c == 0)
            return null;
        return m_group[m_currentSpotIndex % c];
    }
}

