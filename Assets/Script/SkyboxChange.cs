using UnityEngine;
using System.Collections.Generic;

public class SkyboxChange : MonoBehaviour
{
    [Header("Configuration")]
    public Material skyboxMaterial;
    public Camera mainCamera;

    [Header("Zones")]
    public ZoneData[] zones; // Toutes tes zones avec leurs hotspots

    [Header("Prefab Hotspot")]
    public GameObject hotspotPrefab; // Préfab de sphère hotspot
    public Material hotspotMaterial;
    public Material hotspotHighlightMaterial;

    private int currentZoneIndex = 0;
    private List<GameObject> activeHotspots = new List<GameObject>();

    void Start()
    {
        if (zones.Length > 0)
        {
            ChangeZone(0);
        }
        RenderSettings.skybox = skyboxMaterial;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Hotspot hotspot = hit.collider.GetComponent<Hotspot>();
                if (hotspot != null)
                {
                    ChangeZone(hotspot.targetZoneIndex);
                }
            }
        }
    }

    public void ChangeZone(int zoneIndex)
    {
        if (zoneIndex < 0 || zoneIndex >= zones.Length)
        {
            Debug.LogWarning("Index de zone invalide : " + zoneIndex);
            return;
        }

        // Changer la texture skybox
        skyboxMaterial.SetTexture("_MainTex", zones[zoneIndex].texture360);
        currentZoneIndex = zoneIndex;

        // Détruire les anciens hotspots
        ClearHotspots();

        // Créer les nouveaux hotspots
        CreateHotspots(zones[zoneIndex].hotspots);

        Debug.Log("Zone changée vers : " + zones[zoneIndex].zoneName);
    }

    void ClearHotspots()
    {
        foreach (GameObject hotspot in activeHotspots)
        {
            Destroy(hotspot);
        }
        activeHotspots.Clear();
    }

    void CreateHotspots(HotspotData[] hotspotsData)
    {
        foreach (HotspotData data in hotspotsData)
        {
            // Créer le hotspot avec position et rotation
            GameObject hotspotObj = Instantiate(hotspotPrefab, data.position, Quaternion.Euler(data.rotation));

            // Appliquer le scale
            hotspotObj.transform.localScale = data.scale;

            // Configurer le composant Hotspot
            Hotspot hotspot = hotspotObj.GetComponent<Hotspot>();
            if (hotspot == null)
            {
                hotspot = hotspotObj.AddComponent<Hotspot>();
            }
            hotspot.targetZoneIndex = data.targetZoneIndex;
            hotspot.highlightMaterial = hotspotHighlightMaterial;

            // Appliquer le matériau
            Renderer rend = hotspotObj.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = hotspotMaterial;
            }

            // Ajouter à la liste
            activeHotspots.Add(hotspotObj);
        }
    }
}