using System;

public class GameButtonInfo {

	public string FileName { get; private set; }

	public string LevelName { get; private set; }

	public DateTime DateTime { get; private set; }

	public GameButtonInfo(string fileName, string levelName, DateTime dateTime) {
		FileName = fileName;
		LevelName = levelName;
		DateTime = dateTime;
	}
}