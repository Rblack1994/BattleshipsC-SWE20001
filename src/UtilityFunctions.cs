using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;
/// <summary>
/// This includes a number of utility methods for
/// drawing and interacting with the Mouse.
/// </summary>
namespace Battleships
{
static class UtilityFunctions
{
		public static KeyCode EscapeKey = KeyCode.vk_ESCAPE;
		public static KeyCode UpKey = KeyCode.vk_UP;
		public static KeyCode DownKey = KeyCode.vk_DOWN;
		public static KeyCode LeftKey = KeyCode.vk_LEFT;
		public static KeyCode RandomKey = KeyCode.vk_r;
		public static KeyCode BlueKey = KeyCode.vk_b;
		public static KeyCode PinkKey = KeyCode.vk_p;
		public static KeyCode CheatsKey = KeyCode.vk_LSHIFT;
		public static KeyCode MaxKey = KeyCode.vk_F10;
		public static bool ShowShipsCheat = false;

	public const int FIELD_TOP = 122;
	public const int FIELD_LEFT = 349;
	public const int FIELD_WIDTH = 418;

	public const int FIELD_HEIGHT = 418;

	public const int MESSAGE_TOP = 548;
	public const int CELL_WIDTH = 40;
	public const int CELL_HEIGHT = 40;

	public const int CELL_GAP = 2;

	public const int SHIP_GAP = 3;
	private static readonly Color SMALL_SEA = SwinGame.RGBAColor(6, 60, 94, 255);
	private static readonly Color SMALL_SHIP = Color.Gray;
	private static readonly Color SMALL_MISS = SwinGame.RGBAColor(1, 147, 220, 255);

	private static readonly Color SMALL_HIT = SwinGame.RGBAColor(169, 24, 37, 255);
	private static readonly Color LARGE_SEA = SwinGame.RGBAColor(6, 60, 94, 255);
	private static readonly Color LARGE_SHIP = Color.Gray;
	private static readonly Color LARGE_MISS = SwinGame.RGBAColor(1, 147, 220, 255);

	private static readonly Color LARGE_HIT = SwinGame.RGBAColor(252, 2, 3, 255);
	private static readonly Color OUTLINE_COLOR = SwinGame.RGBAColor(5, 55, 88, 255);
	private static readonly Color SHIP_FILL_COLOR = Color.Gray;
	private static readonly Color SHIP_OUTLINE_COLOR = Color.White;

	private static readonly Color MESSAGE_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);
	public const int ANIMATION_CELLS = 7;
		private static int _currentBackground = 1;

		public static KeyCode KeyTyped (KeyCode oldcode)
		{
			//Console.ReadKey().KeyChar;
			foreach (KeyCode value in keycodes)
			{
				if (SwinGame.KeyTyped (value))
					return value;
			}
			return oldcode;
		} 
	
	public const int FRAMES_PER_CELL = 4;
	/// <summary>
	/// Determines if the mouse is in a given rectangle.
	/// </summary>
	/// <param name="x">the x location to check</param>
	/// <param name="y">the y location to check</param>
	/// <param name="w">the width to check</param>
	/// <param name="h">the height to check</param>
	/// <returns>true if the mouse is in the area checked</returns>
	public static bool IsMouseInRectangle(int x, int y, int w, int h)
	{
		Point2D mouse = default(Point2D);
		bool result = false;

		mouse = SwinGame.MousePosition();


		//if the mouse is inline with the button horizontally
		if (mouse.X >= x & mouse.X <= x + w) {
			//Check vertical position
			if (mouse.Y >= y & mouse.Y <= y + h) {
				result = true;
			}
		}

		return result;
	}

	/// <summary>
	/// Draws a large field using the grid and the indicated player's ships.
	/// </summary>
	/// <param name="grid">the grid to draw</param>
	/// <param name="thePlayer">the players ships to show</param>
	/// <param name="showShips">indicates if the ships should be shown</param>
	public static void DrawField(ISeaGrid grid, Player thePlayer, bool showShips)
	{
		DrawCustomField(grid, thePlayer, false, showShips, FIELD_LEFT, FIELD_TOP, FIELD_WIDTH, FIELD_HEIGHT, CELL_WIDTH, CELL_HEIGHT,
		CELL_GAP);
	}

	/// <summary>
	/// Draws a small field, showing the attacks made and the locations of the player's ships
	/// </summary>
	/// <param name="grid">the grid to show</param>
	/// <param name="thePlayer">the player to show the ships of</param>
	public static void DrawSmallField(ISeaGrid grid, Player thePlayer)
	{
		const int SMALL_FIELD_LEFT = 39;
		const int SMALL_FIELD_TOP = 373;
		const int SMALL_FIELD_WIDTH = 166;
		const int SMALL_FIELD_HEIGHT = 166;
		const int SMALL_FIELD_CELL_WIDTH = 13;
		const int SMALL_FIELD_CELL_HEIGHT = 13;
		const int SMALL_FIELD_CELL_GAP = 4;

		DrawCustomField(grid, thePlayer, true, true, SMALL_FIELD_LEFT, SMALL_FIELD_TOP, SMALL_FIELD_WIDTH, SMALL_FIELD_HEIGHT, SMALL_FIELD_CELL_WIDTH, SMALL_FIELD_CELL_HEIGHT,
		SMALL_FIELD_CELL_GAP);
	}

	/// <summary>
	/// Draws the player's grid and ships.
	/// </summary>
	/// <param name="grid">the grid to show</param>
	/// <param name="thePlayer">the player to show the ships of</param>
	/// <param name="small">true if the small grid is shown</param>
	/// <param name="showShips">true if ships are to be shown</param>
	/// <param name="left">the left side of the grid</param>
	/// <param name="top">the top of the grid</param>
	/// <param name="width">the width of the grid</param>
	/// <param name="height">the height of the grid</param>
	/// <param name="cellWidth">the width of each cell</param>
	/// <param name="cellHeight">the height of each cell</param>
	/// <param name="cellGap">the gap between the cells</param>
	private static void DrawCustomField(ISeaGrid grid, Player thePlayer, bool small, bool showShips, int left, int top, int width, int height, int cellWidth, int cellHeight,
	int cellGap)
	{
		//SwinGame.FillRectangle(Color.Blue, left, top, width, height)

		int rowTop = 0;
		int colLeft = 0;

		//Draw the grid
		for (int row = 0; row <= 9; row++) {
			rowTop = top + (cellGap + cellHeight) * row;

			for (int col = 0; col <= 9; col++) {
				colLeft = left + (cellGap + cellWidth) * col;

				Color fillColor = default(Color);
				bool draw = false;

				draw = true;

				switch (grid[row, col]) {
					case TileView.Ship:
						draw = false;
						break;
					//If small Then fillColor = _SMALL_SHIP Else fillColor = _LARGE_SHIP
					case TileView.Miss:
						if (small)
							fillColor = SMALL_MISS;
						else
							fillColor = LARGE_MISS;
						break;
					case TileView.Hit:
						if (small)
							fillColor = SMALL_HIT;
						else
							fillColor = LARGE_HIT;
						break;
					case TileView.Sea:
						if (small)
							fillColor = SMALL_SEA;
						else
							draw = false;
						break;
				}

				if (draw) {
					SwinGame.FillRectangle(fillColor, colLeft, rowTop, cellWidth, cellHeight);
					if (!small) {
						SwinGame.DrawRectangle(OUTLINE_COLOR, colLeft, rowTop, cellWidth, cellHeight);
					}
				}
			}
		}

		if (!showShips) {
			return;
		}

		int shipHeight = 0;
		int shipWidth = 0;
		string shipName = null;

		//Draw the ships
		foreach (Ship s in thePlayer) {
			if (s == null || !s.IsDeployed)
				continue;
			rowTop = top + (cellGap + cellHeight) * s.Row + SHIP_GAP;
			colLeft = left + (cellGap + cellWidth) * s.Column + SHIP_GAP;
				int j = (int)s.CurrentShipColour;
			if (s.Direction == Direction.LeftRight) {
					shipName = "ShipLR" + s.Size+ "a" + j;
				shipHeight = cellHeight - (SHIP_GAP * 2);
				shipWidth = (cellWidth + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap;
			} else {
				//Up down
					shipName = "ShipUD" + s.Size + "a" + j;
				shipHeight = (cellHeight + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap;
				shipWidth = cellWidth - (SHIP_GAP * 2);
			}

			if (!small) {
				SwinGame.DrawBitmap(GameResources.GameImage(shipName), colLeft, rowTop);
			} else {
				SwinGame.FillRectangle(SHIP_FILL_COLOR, colLeft, rowTop, shipWidth, shipHeight);
				SwinGame.DrawRectangle(SHIP_OUTLINE_COLOR, colLeft, rowTop, shipWidth, shipHeight);
			}
		}
	}

		public static void ChangeBackground()
		{
			if (_currentBackground >= 4)
			{
				_currentBackground = 1;
			}
			else
				_currentBackground++;
		}

	private static string _message;
	/// <summary>
	/// The message to display
	/// </summary>
	/// <value>The message to display</value>
	/// <returns>The message to display</returns>
	public static string Message {
		get { return _message; }
		set { _message = value; }
	}

	/// <summary>
	/// Draws the message to the screen
	/// </summary>
	public static void DrawMessage()
	{
			SwinGame.DrawText(Message, MESSAGE_COLOR, GameResources.GameFont("Courier"), FIELD_LEFT, MESSAGE_TOP);
	}

	/// <summary>
	/// Draws the background for the current state of the game
	/// </summary>

	public static void DrawBackground()
	{
		switch (GameController.CurrentState) {
			case GameState.ViewingMainMenu:
			case GameState.ViewingGameMenu:
			case GameState.AlteringSettings:
			case GameState.ViewingHighScores:
				SwinGame.DrawBitmap(GameResources.GameImage("Menu"), 0, 0);
				//SwinGame.DrawText(GameController.CurrentState.ToString(),Color.Red,300,100);
				break;
			case GameState.Discovering:
			case GameState.EndingGame:
				SwinGame.DrawBitmap(GameResources.GameImage("Discovery_"+_currentBackground.ToString()), 0, 0);
				//SwinGame.DrawText(GameController.CurrentState.ToString(),Color.Red,300,100);
				break;
			case GameState.Deploying:
				SwinGame.DrawBitmap(GameResources.GameImage("Deploy"), 0, 0);
				//SwinGame.DrawText(GameController.CurrentState.ToString(),Color.Red,300,100);
				break;
			default:
				SwinGame.ClearScreen();
				//SwinGame.DrawText(GameController.CurrentState.ToString(),Color.Red,300,100);
				break;
		}

			SwinGame.DrawFramerate(675, 585, GameResources.GameFont("CourierSmall"));
	}
	/// <summary>
	/// add's an explosion animation to the game screen , takes parameters for row and column on game board
	/// </summary>
	/// <param name="row">Row.</param>
	/// <param name="col">Col.</param>
	public static void AddExplosion(int row, int col)
	{
		AddAnimation(row, col, "Explosion");
	}
		/// <summary>
		/// add's an splash animation to the game screen , takes parameters for row and column on game board
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
	public static void AddSplash(int row, int col)
	{
		AddAnimation(row, col, "Splash");
	}


	private static List<Sprite> _Animations = new List<Sprite>();
	private static void AddAnimation(int row, int col, string image)
	{
		Sprite s = default(Sprite);
		Bitmap imgObj = default(Bitmap);

			imgObj = GameResources.GameImage(image);
		imgObj.SetCellDetails(40, 40, 3, 3, 7);

		AnimationScript animation = default(AnimationScript);
		animation = SwinGame.LoadAnimationScript("splash.txt");

		s = SwinGame.CreateSprite(imgObj, animation);
		s.X = FIELD_LEFT + col * (CELL_WIDTH + CELL_GAP);
		s.Y = FIELD_TOP + row * (CELL_HEIGHT + CELL_GAP);

		s.StartAnimation("splash");
		_Animations.Add(s);
	}
		/// <summary>
		/// Updates the animations in the battleships game.
		/// </summary>
	public static void UpdateAnimations()
	{
		List<Sprite> ended = new List<Sprite>();
		foreach (Sprite s in _Animations) {
			SwinGame.UpdateSprite(s);
			if (s.AnimationHasEnded) {
				ended.Add(s);
			}
		}

		foreach (Sprite s in ended) {
			_Animations.Remove(s);
			SwinGame.FreeSprite(s);
		}
	}
		/// <summary>
		/// Draws the sprites in the battleships window.
		/// </summary>
	public static void DrawAnimations()
	{
		foreach (Sprite s in _Animations) {
			SwinGame.DrawSprite(s);
		}
	}
		/// <summary>
		/// Draws the animation sequence.
		/// </summary>
	public static void DrawAnimationSequence()
	{
		int i = 0;
		for (i = 1; i <= ANIMATION_CELLS * FRAMES_PER_CELL; i++) {
			UpdateAnimations();
			GameController.DrawScreen();
		}
	} 
		public static KeyCode[] keycodes = {
			KeyCode.vk_Unknown,
			KeyCode.vk_BACKSPACE,
			KeyCode.vk_TAB,
			KeyCode.vk_CLEAR,
			KeyCode.vk_RETURN,
			KeyCode.vk_PAUSE,
			KeyCode.vk_ESCAPE,
			KeyCode.vk_SPACE,
			KeyCode.vk_EXCLAIM,
			KeyCode.vk_QUOTEDBL,
			KeyCode.vk_HASH,
			KeyCode.vk_DOLLAR,
			KeyCode.vk_AMPERSAND,
			KeyCode.vk_QUOTE,
			KeyCode.vk_LEFTPAREN,
			KeyCode.vk_RIGHTPAREN,
			KeyCode.vk_ASTERISK,
			KeyCode.vk_PLUS,
			KeyCode.vk_COMMA,
			KeyCode.vk_MINUS,
			KeyCode.vk_PERIOD,
			KeyCode.vk_SLASH,
			KeyCode.vk_0,
			KeyCode.vk_1,
			KeyCode.vk_2,
			KeyCode.vk_3,
			KeyCode.vk_4,
			KeyCode.vk_5,
			KeyCode.vk_6,
			KeyCode.vk_7,
			KeyCode.vk_8,
			KeyCode.vk_9,
			KeyCode.vk_COLON,
			KeyCode.vk_SEMICOLON,
			KeyCode.vk_LESS,
			KeyCode.vk_EQUALS,
			KeyCode.vk_GREATER,
			KeyCode.vk_QUESTION,
			KeyCode.vk_AT,
			KeyCode.vk_LEFTBRACKET,
			KeyCode.vk_BACKSLASH,
			KeyCode.vk_RIGHTBRACKET,
			KeyCode.vk_CARET,
			KeyCode.vk_UNDERSCORE,
			KeyCode.vk_BACKQUOTE,
			KeyCode.vk_a,
			KeyCode.vk_b,
			KeyCode.vk_c,
			KeyCode.vk_d,
			KeyCode.vk_e,
			KeyCode.vk_f,
			KeyCode.vk_g,
			KeyCode.vk_h,
			KeyCode.vk_i,
			KeyCode.vk_j,
			KeyCode.vk_k,
			KeyCode.vk_l,
			KeyCode.vk_m,
			KeyCode.vk_n,
			KeyCode.vk_o,
			KeyCode.vk_p,
			KeyCode.vk_q,
			KeyCode.vk_r,
			KeyCode.vk_s,
			KeyCode.vk_t,
			KeyCode.vk_u,
			KeyCode.vk_v,
			KeyCode.vk_w,
			KeyCode.vk_x,
			KeyCode.vk_y,
			KeyCode.vk_z,
			KeyCode.vk_DELETE,
			KeyCode.vk_WORLD_0,
			KeyCode.vk_WORLD_1,
			KeyCode.vk_WORLD_2,
			KeyCode.vk_WORLD_3,
			KeyCode.vk_WORLD_4,
			KeyCode.vk_WORLD_5,
			KeyCode.vk_WORLD_6,
			KeyCode.vk_WORLD_7,
			KeyCode.vk_WORLD_8,
			KeyCode.vk_WORLD_9,
			KeyCode.vk_WORLD_10,
			KeyCode.vk_WORLD_11,
			KeyCode.vk_WORLD_12,
			KeyCode.vk_WORLD_13,
			KeyCode.vk_WORLD_14,
			KeyCode.vk_WORLD_15,
			KeyCode.vk_WORLD_16,
			KeyCode.vk_WORLD_17,
			KeyCode.vk_WORLD_18,
			KeyCode.vk_WORLD_19,
			KeyCode.vk_WORLD_20,
			KeyCode.vk_WORLD_21,
			KeyCode.vk_WORLD_22,
			KeyCode.vk_WORLD_23,
			KeyCode.vk_WORLD_24,
			KeyCode.vk_WORLD_25,
			KeyCode.vk_WORLD_26,
			KeyCode.vk_WORLD_27,
			KeyCode.vk_WORLD_28,
			KeyCode.vk_WORLD_29,
			KeyCode.vk_WORLD_30,
			KeyCode.vk_WORLD_31,
			KeyCode.vk_WORLD_32,
			KeyCode.vk_WORLD_33,
			KeyCode.vk_WORLD_34,
			KeyCode.vk_WORLD_35,
			KeyCode.vk_WORLD_36,
			KeyCode.vk_WORLD_37,
			KeyCode.vk_WORLD_38,
			KeyCode.vk_WORLD_39,
			KeyCode.vk_WORLD_40,
			KeyCode.vk_WORLD_41,
			KeyCode.vk_WORLD_42,
			KeyCode.vk_WORLD_43,
			KeyCode.vk_WORLD_44,
			KeyCode.vk_WORLD_45,
			KeyCode.vk_WORLD_46,
			KeyCode.vk_WORLD_47,
			KeyCode.vk_WORLD_48,
			KeyCode.vk_WORLD_49,
			KeyCode.vk_WORLD_50,
			KeyCode.vk_WORLD_51,
			KeyCode.vk_WORLD_52,
			KeyCode.vk_WORLD_53,
			KeyCode.vk_WORLD_54,
			KeyCode.vk_WORLD_55,
			KeyCode.vk_WORLD_56,
			KeyCode.vk_WORLD_57,
			KeyCode.vk_WORLD_58,
			KeyCode.vk_WORLD_59,
			KeyCode.vk_WORLD_60,
			KeyCode.vk_WORLD_61,
			KeyCode.vk_WORLD_62,
			KeyCode.vk_WORLD_63,
			KeyCode.vk_WORLD_64,
			KeyCode.vk_WORLD_65,
			KeyCode.vk_WORLD_66,
			KeyCode.vk_WORLD_67,
			KeyCode.vk_WORLD_68,
			KeyCode.vk_WORLD_69,
			KeyCode.vk_WORLD_70,
			KeyCode.vk_WORLD_71,
			KeyCode.vk_WORLD_72,
			KeyCode.vk_WORLD_73,
			KeyCode.vk_WORLD_74,
			KeyCode.vk_WORLD_75,
			KeyCode.vk_WORLD_76,
			KeyCode.vk_WORLD_77,
			KeyCode.vk_WORLD_78,
			KeyCode.vk_WORLD_79,
			KeyCode.vk_WORLD_80,
			KeyCode.vk_WORLD_81,
			KeyCode.vk_WORLD_82,
			KeyCode.vk_WORLD_83,
			KeyCode.vk_WORLD_84,
			KeyCode.vk_WORLD_85,
			KeyCode.vk_WORLD_86,
			KeyCode.vk_WORLD_87,
			KeyCode.vk_WORLD_88,
			KeyCode.vk_WORLD_89,
			KeyCode.vk_WORLD_90,
			KeyCode.vk_WORLD_91,
			KeyCode.vk_WORLD_92,
			KeyCode.vk_WORLD_93,
			KeyCode.vk_WORLD_94,
			KeyCode.vk_WORLD_95,
			KeyCode.vk_KP0,
			KeyCode.vk_KP1,
			KeyCode.vk_KP2,
			KeyCode.vk_KP3,
			KeyCode.vk_KP4,
			KeyCode.vk_KP5,
			KeyCode.vk_KP6,
			KeyCode.vk_KP7,
			KeyCode.vk_KP8,
			KeyCode.vk_KP9,
			KeyCode.vk_KP_PERIOD,
			KeyCode.vk_KP_DIVIDE,
			KeyCode.vk_KP_MULTIPLY,
			KeyCode.vk_KP_MINUS,
			KeyCode.vk_KP_PLUS,
			KeyCode.vk_KP_ENTER,
			KeyCode.vk_KP_EQUALS,
			KeyCode.vk_UP,
			KeyCode.vk_DOWN,
			KeyCode.vk_RIGHT,
			KeyCode.vk_LEFT,
			KeyCode.vk_INSERT,
			KeyCode.vk_HOME,
			KeyCode.vk_END,
			KeyCode.vk_PAGEUP,
			KeyCode.vk_PAGEDOWN,
			KeyCode.vk_F1,
			KeyCode.vk_F2,
			KeyCode.vk_F3,
			KeyCode.vk_F4,
			KeyCode.vk_F5,
			KeyCode.vk_F6,
			KeyCode.vk_F7,
			KeyCode.vk_F8,
			KeyCode.vk_F9,
			KeyCode.vk_F10,
			KeyCode.vk_F11,
			KeyCode.vk_F12,
			KeyCode.vk_F13,
			KeyCode.vk_F14,
			KeyCode.vk_F15,
			KeyCode.vk_NUMLOCK,
			KeyCode.vk_CAPSLOCK,
			KeyCode.vk_SCROLLOCK,
			KeyCode.vk_RSHIFT,
			KeyCode.vk_LSHIFT,
			KeyCode.vk_RCTRL,
			KeyCode.vk_LCTRL,
			KeyCode.vk_RALT,
			KeyCode.vk_LALT,
			KeyCode.vk_RMETA,
			KeyCode.vk_LMETA,
			KeyCode.vk_LSUPER,
			KeyCode.vk_RSUPER,
			KeyCode.vk_MODE,
			KeyCode.vk_COMPOSE,
			KeyCode.vk_HELP,
			KeyCode.vk_PRINT,
			KeyCode.vk_SYSREQ,
			KeyCode.vk_BREAK,
			KeyCode.vk_MENU,
			KeyCode.vk_POWER,
			KeyCode.vk_EURO
		};
}
}
//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
