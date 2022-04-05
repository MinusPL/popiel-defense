using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }
}
