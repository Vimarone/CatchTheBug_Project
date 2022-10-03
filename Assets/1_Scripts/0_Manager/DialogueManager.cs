using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Text TextDialogue;
    [SerializeField] GameObject[] Buttons;

    static DialogueManager _uniqueInstance;
    private static bool _pausePressed = false;


    GameObject dialogueWindow;
    

    public static DialogueManager _instance
    {
        get { return _uniqueInstance; }
    }

    public static bool Paused
    {
        get { return _pausePressed; }
    }

    void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);

        dialogueWindow = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            //Debug.Log(SceneControlManager._instance.NowScene().ToString());
            CreateDialogueBox(DefineHelper.eDialogType.Exit);
        }
    }

    public void CreateDialogueBox(DefineHelper.eDialogType kind, string txt = "종료하시겠습니까?")
    {
        TextDialogue.text = txt;
        switch (kind)
        {
            case DefineHelper.eDialogType.Notification:
                TextDialogue.color = Color.white;
                TextDialogue.fontSize = 40;
                PauseSelect();
                Buttons[2].SetActive(true);
                break;
            case DefineHelper.eDialogType.Warning:
                TextDialogue.color = Color.red;
                TextDialogue.fontSize = 50;
                PauseSelect();
                Buttons[2].SetActive(true);
                break;
            case DefineHelper.eDialogType.Exit:
                //Debug.Log("Escape key was pressed");
                
                if (_pausePressed == true)
                {
                    _pausePressed = !_pausePressed;
                    TextDialogue.color = Color.white;
                    TextDialogue.fontSize = 60;
                    PauseSelect();
                    Buttons[0].SetActive(true);
                    Buttons[1].SetActive(true);
                }
                else
                    PauseCancelled();
                break;
        }
    }

    void ExitBtnPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();         // application에서만 작동(editor 상에서는 적용되지 않음)
#endif
    }

    void YesBtnPressed() // Dialogue | Warning 확인 버튼 (2)
    {
        PauseCancelled();
    }

    void NoBtnPressed() // 종료 안함 버튼 (1)
    {
        PauseCancelled();
    }

    void EscapeBtnPressed() // 종료 버튼
    {
        PauseCancelled();
    }

    void PauseSelect()
    {

        if (SceneControlManager._instance.NowScene() == DefineHelper.eSceneIndex.IngameScene) 
        {
            dialogueWindow.SetActive(true);
            Time.timeScale = 0;
        }
        else if (SceneControlManager._instance.NowScene() == DefineHelper.eSceneIndex.MainScene)
        {
            dialogueWindow.SetActive(true);
            Time.timeScale = 1;
        }
        else { }
            

    }

    void PauseCancelled()
    {
        for (int i = 0; i < Buttons.Length; i++)
            Buttons[i].SetActive(false);
        _pausePressed = !_pausePressed;
        Time.timeScale = 1;
        dialogueWindow.SetActive(false);
    }
}
