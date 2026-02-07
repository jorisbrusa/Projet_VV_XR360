using UnityEngine;

[System.Serializable]
public class ZoneData
{
    public string zoneName;            // Nom de la zone
    public Texture texture360;         // Image 360° de cette zone
    public HotspotData[] hotspots;     // Liste des hotspots dans cette zone
}