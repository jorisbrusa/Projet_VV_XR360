using UnityEngine;

[System.Serializable]
public class HotspotData
{
    public Vector3 position;           // Position du hotspot
    public Vector3 rotation;           // Rotation (Euler angles)
    public Vector3 scale = Vector3.one * 1f; // Scale (par défaut 1)
    public int targetZoneIndex;        // Vers quelle zone il mène
    public string label;               // Nom (optionnel, pour debug)
}