using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogUnpause : UnityEvent<bool> { }

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textObj;
    //public GameObject marker;

    public StringDatabase database;

    public DialogUnpause dialogUnpause;

    public float letterTime = 0.4f;
    private float letterTimer = 0.0f;

    private bool typeText = false;

    private string textToSet;
    private string currentText;
    private int textPos = 0;

    private bool clicked = false;

    StringData currentData;

    public Image img;
    public Image bgImg;

    private bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        //marker.SetActive(false);
        if (dialogUnpause == null) dialogUnpause = new DialogUnpause();
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            if (!paused)
            {
                if (typeText)
                {
                    typeText = false;
                    currentText = textToSet;
                    textObj.text = currentText;
                    //marker.SetActive(true);
                }
                else
                {
                    if (currentData != null && currentData.nextId != -1)
                    {
                        SetText(currentData.nextId, true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        dialogUnpause.Invoke(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (typeText)
        {
            if (letterTimer >= letterTime)
            {
                if (textPos > textToSet.Length - 1)
                {
                    typeText = false;
                    //marker.SetActive(true);
                }
                else
                {
                    if (textToSet[textPos] != '<')
                        currentText += $"{textToSet[textPos++]}";
                    else
                    {
                        while (textToSet[textPos] != '>')
                        {
                            currentText += $"{textToSet[textPos++]}";
                        }
                        currentText += $"{textToSet[textPos++]}";
                    }
                }
                textObj.text = currentText;
                letterTimer = 0.0f;
            }
            letterTimer += Time.deltaTime;
        }
    }

    public void SetText(string text, bool typing)
	{
        gameObject.SetActive(true);
        if (!typing)
        {
            currentText = text;
            //marker.SetActive(true);
        }
        else
        {
            textToSet = text;
            typeText = true;
            currentText = "";
            textPos = 0;
            letterTimer = letterTime;
            //marker.SetActive(false);
        }
        textObj.text = currentText;
    }

    public void SetText(int id, bool typing)
	{
        currentData = database.GetStringData(id);
        if (currentData == null) currentData = database.GetStringData(0);
        img.sprite = currentData.image;
        bgImg.sprite = currentData.backgroundImage;
        SetText(currentData.data, typing);
	}

	public void OnMenuPause(bool pause, bool menu)
    {
        if (menu) paused = pause;
	}
}
