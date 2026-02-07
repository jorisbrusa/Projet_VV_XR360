using UnityEngine;

public class Hotspot : MonoBehaviour
{
    public int targetZoneIndex;
    public Material highlightMaterial;

    private Material originalMaterial;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalMaterial = rend.material;
        }
    }

    void OnMouseEnter()
    {
        if (rend != null && highlightMaterial != null)
        {
            rend.material = highlightMaterial;
        }
    }

    void OnMouseExit()
    {
        if (rend != null && originalMaterial != null)
        {
            rend.material = originalMaterial;
        }
    }
}