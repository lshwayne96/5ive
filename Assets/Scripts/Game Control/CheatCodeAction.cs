using UnityEngine;
using UnityEngine.UI;

public class CheatCodeAction : MonoBehaviour {

    private InputField inputField;
    private Button[] levelButtons;

    private void Awake() {
        inputField = GetComponent<InputField>();
        levelButtons = GameObject.FindGameObjectWithTag("LevelButtons")
                         .GetComponentsInChildren<Button>();
    }

    public void Action() {
        if (inputField.text == "a" || inputField.text == "Power Overwhelming") {
            for (int i = 0; i < 5; i++) {
                levelButtons[i].interactable = true;
            }
        }

        inputField.text = System.String.Empty;
        inputField.DeactivateInputField();
        gameObject.SetActive(false);
    }
}
