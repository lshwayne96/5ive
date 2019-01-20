/*

using UnityEngine;
using NUnit.Framework;

public class MenuUtilTest {

	private GameObject menu1;
	private GameObject menu2;

	[OneTimeSetUp]
	public void SetUp() {
		menu1 = new GameObject();
		menu1.tag = "SaveMenu";

		menu2 = new GameObject();
		menu2.tag = "LoadMenu";
	}

	[Test]
	public void IsSaveMenu_SaveMenu() {
		Assert.IsTrue(MenuUtil.IsSaveMenu(menu1));
		Assert.IsFalse(MenuUtil.IsSaveMenu(menu2));
	}

	[Test]
	public void IsLoadMenu_LoadMenu() {
		Assert.IsFalse(MenuUtil.IsLoadMenu(menu1));
		Assert.IsTrue(MenuUtil.IsLoadMenu(menu2));
	}
}

*/