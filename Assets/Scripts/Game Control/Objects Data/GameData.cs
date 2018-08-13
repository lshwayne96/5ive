<<<<<<< HEAD
﻿/*
 * This class represents the data of the game and is used to restore
 * the game to its saved state.
 */

using System;

[Serializable]
public class GameData {
    private int numLevelsCompleted;
    private bool[] collectibleLocations;

    private bool hasAdvancedInGame;
    private string lastSavedFileName;
    private int lastSavedLevel;
    private int lastUnlockedLevel;

    public GameData(int numLevels) {
        this.numLevelsCompleted = 1;
        this.hasAdvancedInGame = false;
        this.collectibleLocations = new bool[numLevels + 1];
        for (int i = 1; i < collectibleLocations.Length; i++) {
            collectibleLocations[i] = true;
        }
        // Default starting level
        this.lastUnlockedLevel = (int)Level.MainMenu;
    }

    public bool DoesSceneContainCollectible(int sceneBuildIndex) {
        return collectibleLocations[sceneBuildIndex];
    }

    public void UpdateCollectibleLocations(int sceneBuildIndex) {
        collectibleLocations[sceneBuildIndex] = false;
    }

    public void UpdateNumLevelsCompleted() {
        numLevelsCompleted++;
    }

    public int GetNumLevelsCompleted() {
        return numLevelsCompleted;
    }

    public bool HasAdvancedInGame() {
        return hasAdvancedInGame;
    }

    public void SetHasAdvancedInGame(bool value) {
        hasAdvancedInGame = value;
    }

    public void SetLastSavedFileName(string fileName) {
        lastSavedFileName = fileName;
    }

    public string GetLastSavedFileName() {
        return lastSavedFileName;
    }

    public bool HasUnlockedNewLevelWithoutSaving() {
        return lastUnlockedLevel > lastSavedLevel;
    }

    public int GetLastUnlockedLevel() {
        return lastUnlockedLevel;
    }

    public void SetLastSavedLevel(int lastSavedLevel) {
        this.lastSavedLevel = lastSavedLevel;
    }

    public void SetLastUnlockedLevel(int lastUnlockedLevel) {
        this.lastUnlockedLevel = lastUnlockedLevel;
    }
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDa : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
}
