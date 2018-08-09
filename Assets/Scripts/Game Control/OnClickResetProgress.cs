using UnityEngine;
using UnityEngine.UI;

public class OnClickResetProgress : MonoBehaviour {

    private GameDataManager gameDataManager;
    private Text newButtonText;

    private void Awake() {
        /*
        // This is called in Awake() so that the New Button reference can be obtained
        newButtonText = GameObject.FindGameObjectWithTag("NewButton")
                                  .GetComponentInChildren<Text>();
        */
        // Set the Options Menu to inactive after getting the New Button reference
        GameObject.FindGameObjectWithTag("OptionsMenu").SetActive(false);
    }

    public void ResetProgress() {
        GameDataManager.ResetProgress();
    }
}
