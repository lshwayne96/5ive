/*
 * This is a utility class.
 * It provides methods which check if a menu is of a specific type
 * and also sets an uninitialised gameObject parentMenu as
 * one of the menu types available.
 */

using UnityEngine;

public class LevelMenu {

    // Hide the constructor
    private LevelMenu() {

    }

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