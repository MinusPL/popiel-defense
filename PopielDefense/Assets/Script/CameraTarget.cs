using UnityEngine;
using UnityEngine.InputSystem;
public class CameraTarget : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool topDown = false;

    Keyboard currentKeyboard;
    void Start()
    {
        currentKeyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (currentKeyboard.wKey.isPressed)
        {
            direction += topDown ? Vector3.forward : Camera.main.transform.forward;
        }
        else if (currentKeyboard.sKey.isPressed)
        {
            direction -= topDown ? Vector3.forward : Camera.main.transform.forward;
        }

        if (currentKeyboard.aKey.isPressed)
        {
            direction -= topDown ? Vector3.right : Camera.main.transform.right;
        }
        else if (currentKeyboard.dKey.isPressed)
        {
            direction += topDown ? Vector3.right : Camera.main.transform.right;
        }

        if(!topDown) direction.y = 0;

        transform.position += direction * moveSpeed * Time.deltaTime;

    }
}
