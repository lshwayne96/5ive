using UnityEngine;
using UnityEngine.UI;

public class EnterCheatCodes : MonoBehaviour {

    private Image box;
    private InputField inputField;
    private Button[] levelButtons;

    private bool boxEnable;

    void Start() {
        box = GetComponent<Image>();
        box.enabled = false;
        inputField = GetComponent<InputField>();
        levelButtons = GameObject.FindGameObjectWithTag("LevelButtons")
                                 .GetComponentsInChildren<Button>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            boxEnable = !boxEnable;
            box.enabled = boxEnable;
        }
    }

    public void UnlockLevelButtons() {
        if (inputField.text == "Power Overwhelming") {
            for (int i = 0; i < 5; i++) {
                levelButtons[i].interactable = true;
            }
        }

        box.enabled = false;
        inputField.text = System.String.Empty;
    }
}
