using UnityEngine;

public class LookAround : MonoBehaviour
{
    [Header("Rotation")]
    public float speed = 5f;
    public float verticalLimit = 80f;

    [Header("Zoom")]
    public bool enableZoom = true; // NOUVEAU : Activer/désactiver le zoom
    public float zoomSpeed = 10f;
    public float minFOV = 20f;  // Zoom max (proche)
    public float maxFOV = 90f;  // Zoom min (loin)
    public float defaultFOV = 60f; // FOV par défaut

    private float yaw = 0f;
    private float pitch = 0f;
    private Camera cam;

    void Start()
    {
        Vector3 rot = transform.eulerAngles;
        yaw = rot.y;
        pitch = rot.x;
        if (pitch > 180) pitch -= 360;

        // Récupérer la caméra
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.fieldOfView = defaultFOV;
        }
    }

    void Update()
    {
        // Rotation
        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") * speed;
            pitch -= Input.GetAxis("Mouse Y") * speed;
            pitch = Mathf.Clamp(pitch, -verticalLimit, verticalLimit);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }

        // Zoom avec la molette (seulement si activé)
        if (enableZoom && cam != null) // Vérification
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0f)
            {
                // Ajuster le FOV (Field of View)
                cam.fieldOfView -= scroll * zoomSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
            }

            // Reset zoom avec clic molette (optionnel)
            if (Input.GetMouseButtonDown(2))
            {
                cam.fieldOfView = defaultFOV;
            }
        }
    }

    // NOUVEAU : Méthodes pour activer/désactiver depuis d'autres scripts
    public void SetZoomEnabled(bool enabled)
    {
        enableZoom = enabled;

        // Reset au FOV par défaut quand on désactive
        if (!enabled && cam != null)
        {
            cam.fieldOfView = defaultFOV;
        }
    }

    public void ToggleZoom()
    {
        SetZoomEnabled(!enableZoom);
    }
}