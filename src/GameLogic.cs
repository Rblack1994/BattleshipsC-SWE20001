
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
////using System.Data;
using System.Diagnostics;
using SwinGameSDK;
namespace Battleships
{
static class GameLogic
{
	public static void Main()
	{
		//Opens a new Graphics Window("Battleships", 800, 600);
			SwinGame.OpenGraphicsWindow("Battleships", 800, 600);

		//Load Resources
			GameResources.LoadResources();

			SwinGame.PlayMusic(GameResources.GameMusic("Background"));

		//Game Loop
		do {
				GameController.HandleUserInput();
			GameController.DrawScreen();
				if (SwinGame.KeyTyped (KeyCode.vk_F10))
				{
					SwinGame.ToggleFullScreen ();
				}
		} while (!(SwinGame.WindowCloseRequested() == true | GameController.CurrentState == GameState.Quitting));

		SwinGame.StopMusic();

		//Free Resources and Close Audio, to end the program.
			GameResources.FreeResources();
	}
}
}
//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
