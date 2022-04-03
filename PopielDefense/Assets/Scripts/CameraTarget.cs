using UnityEngine;
using UnityEngine.InputSystem;
public class CameraTarget : MonoBehaviour
{
    public float moveSpeed = 5f;

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
            direction += Vector3.forward;
        }
        else if (currentKeyboard.sKey.isPressed)
        {
            direction += Vector3.back;
        }

        if(currentKeyboard.aKey.isPressed)
        {
            direction += Vector3.left;
        }
        else if (currentKeyboard.dKey.isPressed)
        {
            direction += Vector3.right;
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
