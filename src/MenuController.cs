
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>
namespace Battleships
{
static class MenuController
{

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string[][] _menuStructure = {
		new string[] {
			"PLAY",
			"SETUP",
			"SCORES",
			"QUIT",
			"HOTKEYS"
		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"QUIT"
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		}

	};
	private const int MENU_TOP = 575;
	private const int MENU_LEFT = 30;
	private const int MENU_GAP = 0;
	private const int BUTTON_WIDTH = 75;
	private const int BUTTON_HEIGHT = 15;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

	private const int TEXT_OFFSET = 0;
	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;
	private const int QUIT_MENU = 3;

	private const int SETUP_MENU = 2;
	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;

	private const int MAIN_MENU_QUIT_BUTTON = 3;
	private const int MAIN_MENU_HOTKEYS_BUTTON = 4;
	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;

	private const int SETUP_MENU_EXIT_BUTTON = 3;
	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;

	private const int GAME_MENU_QUIT_BUTTON = 2;
	private static readonly Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);

	private const int HOTKEYS_FUNC = 550;  // positioning of function name text
	private const int HOTKEYS_BIND = 350;   // positioning of key binding name text


	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleMainMenuInput()
	{
		HandleMenuInput(MAIN_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the  processing of user input when the quit menu is showing
	/// </summary>
	public static void HandleQuitMenuInput()
	{
			if (SwinGame.MouseClicked (MouseButton.LeftButton))
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () / 2 + 70, 400, 50, 25))
					GameController.AddNewState (GameState.ViewingMainMenu);
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () / 2 - 70, 400, 50, 25))
					GameController.AddNewState (GameState.Quitting);
			}
	}

	/// <summary>
	/// Handles the  processing of user input when the Hotkeys menu is showing
	/// </summary>
	public static void HandleHotkeysMenuInput()
	{
			if (SwinGame.KeyTyped(UtilityFunctions.EscapeKey)) {
				GameController.SwitchState(GameState.ViewingGameMenu);
			}
			if (SwinGame.AnyKeyPressed ())
			{
				

				// Escape key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 100, 50, 25))
					UtilityFunctions.EscapeKey = UtilityFunctions.KeyTyped (UtilityFunctions.EscapeKey);

				// Up key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 150, 50, 25))
					UtilityFunctions.UpKey = UtilityFunctions.KeyTyped (UtilityFunctions.UpKey);

				// Down key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 200, 50, 25))
					UtilityFunctions.DownKey = UtilityFunctions.KeyTyped (UtilityFunctions.DownKey);

				// Left key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 250, 50, 25))
					UtilityFunctions.LeftKey = UtilityFunctions.KeyTyped (UtilityFunctions.LeftKey);

				// Random key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 300, 50, 25))
					UtilityFunctions.RandomKey = UtilityFunctions.KeyTyped (UtilityFunctions.RandomKey);

				// Blue key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 350, 50, 25))
					UtilityFunctions.BlueKey = UtilityFunctions.KeyTyped (UtilityFunctions.BlueKey);

				// Pink key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 400, 50, 25))
					UtilityFunctions.PinkKey = UtilityFunctions.KeyTyped (UtilityFunctions.PinkKey);

				// Cheats key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 450, 50, 25))
					UtilityFunctions.CheatsKey = UtilityFunctions.KeyTyped (UtilityFunctions.CheatsKey);

				// Maximise screen key
				if (SwinGame.PointInRect (SwinGame.MousePosition (), SwinGame.ScreenWidth () - HOTKEYS_BIND, 450, 50, 25))
					UtilityFunctions.MaxKey = UtilityFunctions.KeyTyped (UtilityFunctions.MaxKey);
			}
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSetupMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(SETUP_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput()
	{
		HandleMenuInput(GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput(int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped(UtilityFunctions.EscapeKey)) {
			GameController.EndCurrentState();
			return true;
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu(i, level, xOffset)) {
					PerformMenuAction(menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState();
			}
		}

		return false;
	}

	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu()
	{
		//Clears the Screen to Black
			//SwinGame.DrawText ("Main Menu", Color.White, 50, 50);
		DrawButtons(MAIN_MENU);
	}

		public static void DrawHotkeysMenu()
		{
			SwinGame.DrawText ("Hover over the keybinding to edit", Color.AntiqueWhite, GameResources.GameFont("Menu") ,SwinGame.ScreenWidth () - 550, 25);
			SwinGame.DrawText ("Function", Color.AntiqueWhite, GameResources.GameFont("Menu"), SwinGame.ScreenWidth () - HOTKEYS_FUNC, 75);
			SwinGame.DrawText ("Key Bound", Color.AntiqueWhite, GameResources.GameFont("Menu"), SwinGame.ScreenWidth () - HOTKEYS_BIND, 75);

				// Escape key
				SwinGame.DrawText ("Escape key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 100);
				SwinGame.DrawText (UtilityFunctions.EscapeKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 100);
					// Up key
				SwinGame.DrawText ("Up key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 150);
				SwinGame.DrawText (UtilityFunctions.UpKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 150);
					// Down key
				SwinGame.DrawText ("Down key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 200);
				SwinGame.DrawText (UtilityFunctions.DownKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 200);
					// Left key
				SwinGame.DrawText ("Left key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 250);
				SwinGame.DrawText (UtilityFunctions.LeftKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 250);
					// Random key
				SwinGame.DrawText ("Random key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 300);
				SwinGame.DrawText (UtilityFunctions.RandomKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 300);
					// Blue key
				SwinGame.DrawText ("Blue key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 350);
				SwinGame.DrawText (UtilityFunctions.BlueKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 350);
					// Pink key
				SwinGame.DrawText ("Pink key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 400);
				SwinGame.DrawText (UtilityFunctions.PinkKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 400);
					// Cheats key
				SwinGame.DrawText ("Cheats key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 450);
				SwinGame.DrawText (UtilityFunctions.CheatsKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 450);	
					// maximise key
				SwinGame.DrawText ("Fullscreen key", Color.AntiqueWhite, SwinGame.ScreenWidth () - HOTKEYS_FUNC, 500);
				SwinGame.DrawText (UtilityFunctions.MaxKey.ToString(), Color.White, SwinGame.ScreenWidth () - HOTKEYS_BIND, 500);	
		}

	public static void DrawQuitMenu()
	{
		//Clears the Screen to Black
			SwinGame.DrawText ("Quit Menu", Color.White,  50, 50);
			SwinGame.DrawTextLines("Really Quit ?", Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, 0, 200, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			SwinGame.DrawText ("Yes", Color.White, GameResources.GameFont("Menu"), SwinGame.ScreenWidth()/2 - 60, 405);
			SwinGame.DrawText ("No", Color.White, GameResources.GameFont("Menu"), SwinGame.ScreenWidth()/2 + 80, 405);
			SwinGame.DrawRectangle (Color.Red,false,SwinGame.ScreenWidth () / 2 - 70, 400, 50, 25);
			SwinGame.DrawRectangle (Color.Red,false,SwinGame.ScreenWidth () / 2 + 70, 400, 50, 25);
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(GAME_MENU);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSettings()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(MAIN_MENU);
		DrawButtons(SETUP_MENU, 1, 1);
	}

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons(int menu)
	{
		DrawButtons(menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons(int menu, int level, int xOffset)
	{
		int btnTop = 0;

		btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int i = 0;
		for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
			int btnLeft = 0;
			btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
			//SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
			SwinGame.DrawTextLines(_menuStructure[menu][i], MENU_COLOR, Color.Black, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, btnLeft + TEXT_OFFSET, btnTop + TEXT_OFFSET, BUTTON_WIDTH, BUTTON_HEIGHT);

			if (SwinGame.MouseDown(MouseButton.LeftButton) & IsMouseOverMenu(i, level, xOffset)) {
				SwinGame.DrawRectangle(HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton(int button)
	{
		return IsMouseOverMenu(button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu(int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

			return UtilityFunctions.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction(int menu, int button)
	{
		switch (menu) {
			case MAIN_MENU:
				PerformMainMenuAction(button);
				break;
			case SETUP_MENU:
				PerformSetupMenuAction(button);
				break;
			case GAME_MENU:
				PerformGameMenuAction(button);
				break;
		}
	}

	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformMainMenuAction(int button)
	{
		switch (button) {
			case MAIN_MENU_PLAY_BUTTON:
				GameController.StartGame();
				break;
			case MAIN_MENU_SETUP_BUTTON:
				GameController.AddNewState(GameState.AlteringSettings);
				break;
			case MAIN_MENU_TOP_SCORES_BUTTON:
				GameController.AddNewState(GameState.ViewingHighScores);
				break;
			case MAIN_MENU_QUIT_BUTTON:
				GameController.EndCurrentState ();
				break;
			case MAIN_MENU_HOTKEYS_BUTTON:
				GameController.AddNewState (GameState.ChangingHotkeys);
				break;
		}
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction(int button)
	{
		switch (button) {
			case SETUP_MENU_EASY_BUTTON:
				GameController.SetDifficulty(AIOption.Easy);
				break;
			case SETUP_MENU_MEDIUM_BUTTON:
				GameController.SetDifficulty(AIOption.Medium);
				break;
			case SETUP_MENU_HARD_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
		}
		//Always end state - handles exit button as well
			GameController.EndCurrentState();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction(int button)
	{
		switch (button) {
			case GAME_MENU_RETURN_BUTTON:
				GameController.EndCurrentState();
				break;
			case GAME_MENU_SURRENDER_BUTTON:
				GameController.EndCurrentState();
				//end game menu
				GameController.EndCurrentState();
				//end game
				break;
			case GAME_MENU_QUIT_BUTTON:
				GameController.AddNewState(GameState.Quittingprompt);
				break;
		}
	}
}
}
//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
