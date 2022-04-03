using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CMFreelookCameraController : MonoBehaviour
{
    Mouse currentMouse;
    CinemachineFreeLook camera;

    public float zoomSpeed = 25f;
    public float zoomAcceleration = 2.5f;
    public float zoomInnerRange = 2.5f;
    public float zoomOuterRange = 50f;

    private float currentMiddleRigRadius = 10f;
    private float newMiddleRigRadius = 10f;
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
        CinemachineCore.GetInputAxis = GetAxisCustom;
        currentMouse = Mouse.current;
        camera = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        ZoomYAxis = currentMouse.scroll.y.ReadValue();
    }

    private void LateUpdate()
    {
        UpdateZoomLevel();
    }

    public float GetAxisCustom(string axisName)
    {
        if(axisName == "Mouse X")
        {
            return currentMouse.rightButton.isPressed ? currentMouse.delta.x.ReadValue() : 0;
           
        }
        if(axisName == "Mouse Y")
        {
            return currentMouse.rightButton.isPressed ? currentMouse.delta.y.ReadValue() : 0;
        }
        return 0;
    }

    private void UpdateZoomLevel()
    {
        if(currentMiddleRigRadius == newMiddleRigRadius) { return; }

        currentMiddleRigRadius = Mathf.Lerp(currentMiddleRigRadius, newMiddleRigRadius, zoomAcceleration * Time.deltaTime);
        currentMiddleRigRadius = Mathf.Clamp(currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);

        camera.m_Orbits[1].m_Radius = currentMiddleRigRadius;
        camera.m_Orbits[0].m_Height = camera.m_Orbits[1].m_Radius;
        camera.m_Orbits[2].m_Height = -camera.m_Orbits[1].m_Radius;
    }

    public void AdjustCameraZoomIndex(float zoomYAxis)
    {
        if (zoomYAxis == 0) return;
        if(zoomYAxis < 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius + zoomSpeed;
        }
        if(zoomYAxis > 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius - zoomSpeed;
        }
    }
}
