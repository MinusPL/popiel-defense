using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CMMainCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject freeLookCamera;
    public GameObject topDownCamera;

    private Keyboard currentKeyboard;
    void Start()
    {
        if (freeLookCamera == null || topDownCamera == null)
        {
            Debug.LogError("Nie podpi¹³eœ kamer w skrypcie!");
            return;
        }
        freeLookCamera.SetActive(true);
        topDownCamera.SetActive(false);

        currentKeyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentKeyboard.tKey.wasPressedThisFrame)
        {
            if(!topDownCamera.activeSelf)
            {
                freeLookCamera.SetActive(false);
                topDownCamera.SetActive(true);
            }
            else
            {
                freeLookCamera.SetActive(true);
                topDownCamera.SetActive(false);
            }
        }
    }
}
