using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogEvent : UnityEvent<int, bool> { }

[System.Serializable]
public class PauseEvent : UnityEvent<bool, bool> { }
public class LevelUIManager : MonoBehaviour
{
    public DialogEvent dialogEvent;
    public PauseEvent pauseEvent;

    public bool objectsPaused = false;
    public int startDialogID = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (dialogEvent == null) dialogEvent = new DialogEvent();
        Invoke("LateStart", 0.5f);
    }

    // Update is called once per frame
    private void LateStart()
    {
        StartDialog(startDialogID);
    }

    public void StartDialog(int id)
    {
        pauseEvent.Invoke(true, false);
        dialogEvent.Invoke(id, true);
        objectsPaused = true;
    }
}
