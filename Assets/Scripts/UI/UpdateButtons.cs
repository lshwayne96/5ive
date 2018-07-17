using UnityEngine;

public class UpdateButtons : MonoBehaviour {

    private FileButtonManager buttonManager;
    private bool hasInitialised;
    private GameObject parentMenu;

    private void Start() {
        hasInitialised = false;
        parentMenu = GameObject.FindWithTag("SaveMenu");
        if (parentMenu == null) {
            parentMenu = GameObject.FindWithTag("LoadMenu");
        }
    }

    private void OnEnable() {
        Start();
        if (buttonManager == null) {
            if (parentMenu.CompareTag("SaveMenu")) {
                buttonManager = GetComponentInChildren<SaveMenuFileButtonManager>();
            } else if (parentMenu.CompareTag("LoadMenu")) {
                buttonManager = GetComponentInChildren<LoadMenuFileButtonManager>();
            }
        }

        if (!hasInitialised) {
            buttonManager.Initialise();
        }

        buttonManager.UpdateButtons();
    }
}
