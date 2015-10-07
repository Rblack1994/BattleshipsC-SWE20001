
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
////using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
namespace Battleships
{
static class DiscoveryController
{

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput()
	{
		if (SwinGame.KeyTyped(UtilityFunctions.EscapeKey)) {
			GameController.AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.KeyTyped(UtilityFunctions.CheatsKey)) {
				UtilityFunctions.ShowShipsCheat = !UtilityFunctions.ShowShipsCheat;
		}
		
		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}
			if (SwinGame.KeyTyped (KeyCode.vk_b))
			{
				UtilityFunctions.ChangeBackground ();
			}

			if (SwinGame.KeyTyped (KeyCode.vk_s))
			{
				GameController.SaveGame ();
			}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack()
	{
		Point2D mouse = default(Point2D);

		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
			row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
			col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

			if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) 
			{
				if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) 
				{
					GameController.Attack (row, col);
				}
			}
	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;
		const int AI_LEFT = 50;
		const int AI_TOP = 350;
		
			if (UtilityFunctions.ShowShipsCheat) {
				UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
		} else {
				UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

			UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
			UtilityFunctions.DrawMessage();

			SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
			SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
			SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
			SwinGame.DrawText(GameController.AiGameSetting(),Color.White, GameResources.GameFont("Menu"), AI_LEFT,AI_TOP);

			//Draws Ships
			foreach (ShipName sn in Enum.GetValues(typeof(ShipName))) {
				if (object.ReferenceEquals (GameController.ComputerPlayer.Ship (sn), null))
				{
				}
				else
				{
					if (GameController.ComputerPlayer.Ship (sn).IsDestroyed)
					{
						int i = 0;
						i = (int)sn - 1;
						int rowTop = 122 + (2 + 40) * GameController.ComputerPlayer.Ship (sn).Row + 3;
						int colLeft = 349 + (2 + 40) * GameController.ComputerPlayer.Ship (sn).Column + 3;
						if (i >= 0)
						{
							string shipName = null;
							int shipHeight = 0;
							int shipWidth = 0;
							int cellHeight =  40;
							int SHIP_GAP = 3;
							int cellWidth = 40;
							int cellGap = 2;
							int j = (int)GameController.ComputerPlayer.Ship (sn).CurrentShipColour;
							if (GameController.ComputerPlayer.Ship (sn).Direction == Direction.LeftRight) {
								shipName = "ShipLR" + GameController.ComputerPlayer.Ship (sn).Size+ "a" + j;
								shipHeight = cellHeight - (SHIP_GAP * 2);
								shipWidth = (cellWidth + cellGap) * GameController.ComputerPlayer.Ship (sn).Size - (SHIP_GAP * 2) - cellGap;
							} else {
								//Up down
								shipName = "ShipUD" + GameController.ComputerPlayer.Ship (sn).Size + "a" + j;
								shipHeight = (cellHeight + cellGap) * GameController.ComputerPlayer.Ship (sn).Size - (SHIP_GAP * 2) - cellGap;
								shipWidth = cellWidth - (SHIP_GAP * 2);
							}
							//FIX HERE!!!
							SwinGame.DrawBitmap(GameResources.GameImage (shipName), colLeft, rowTop);

							//    SwinGame.FillRectangle(Color.LightBlue, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
							//Else
							//    SwinGame.FillRectangle(Color.Gray, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
						}

						//SwinGame.DrawRectangle(Color.Black, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
						//SwinGame.DrawText(sn.ToString(), Color.Black, GameFont("Courier"), SHIPS_LEFT + TEXT_OFFSET, SHIPS_TOP + i * SHIPS_HEIGHT)
					}
				}
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
