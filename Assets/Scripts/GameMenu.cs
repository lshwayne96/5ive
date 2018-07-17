using UnityEngine;

public class GameMenu {
    public static GameObject SetParentMenu(GameObject parentMenu) {
        // Determine if the parent of this button is the SaveMenu or the LoadMenu
        parentMenu = GameObject.FindWithTag("SaveMenu");
        if (parentMenu == null) {
            parentMenu = GameObject.FindWithTag("LoadMenu");
        }

        return parentMenu;
    }

    public static bool IsSaveMenu(GameObject menu) {
        return menu.CompareTag("SaveMenu");
    }

    public static bool IsLoadMenu(GameObject menu) {
        return menu.CompareTag("LoadMenu");
    }
}