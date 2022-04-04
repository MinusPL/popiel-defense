using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CMTopDownCamera : MonoBehaviour
{
    // Start is called before the first frame update

    Mouse currentMouse;
    CinemachineVirtualCamera camera;

    public float zoomSpeed = 25f;
    public float zoomAcceleration = 2.5f;
    public float zoomInnerRange = 2.5f;
    public float zoomOuterRange = 50f;

    private float currentDistance = 10f;
    private float newDistance = 10f;

    private float zoomYAxis = 0f;

    public float ZoomYAxis
    {
        get { return zoomYAxis; }
        set
        {
            if (zoomYAxis == value) return;
            zoomYAxis = value;
            AdjustCameraZoomIndex(ZoomYAxis);
        }
    }

    void Start()
    {
        currentMouse = Mouse.current;
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    private void Update()
    {
        ZoomYAxis = currentMouse.scroll.y.ReadValue();
    }

    private void LateUpdate()
    {
        UpdateZoomLevel();
    }

    private void UpdateZoomLevel()
    {
        if (currentDistance == newDistance) { return; }

        currentDistance = Mathf.Lerp(currentDistance, newDistance, zoomAcceleration * Time.deltaTime);
        currentDistance = Mathf.Clamp(currentDistance, zoomInnerRange, zoomOuterRange);

        camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = currentDistance;
    }

    public void AdjustCameraZoomIndex(float zoomYAxis)
    {
        if (zoomYAxis == 0) return;
        if (zoomYAxis < 0)
        {
            newDistance = currentDistance + zoomSpeed;
        }
        if (zoomYAxis > 0)
        {
            newDistance = currentDistance - zoomSpeed;
        }
    }
}
