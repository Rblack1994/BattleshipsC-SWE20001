	using Microsoft.VisualBasic;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	// //using System.Data;
	using System.Diagnostics;

	/// <summary>
	/// The AIMediumPlayer is a type of AIPlayer where it will try and destroy a ship
	/// if it has found a ship
	/// </summary>
	namespace Battleships
	{
		public class AIEasyPlayer : AIPlayer
		{
			public AIEasyPlayer(BattleShipsGame controller) : base(controller)
			{
			}

			/// <summary>
			/// GenerateCoordinates should generate random shooting coordinates
			/// only when it has not found a ship, or has destroyed a ship and
			/// needs new shooting coordinates
			/// </summary>
			/// <param name="row">the generated row</param>
			/// <param name="column">the generated column</param>
			protected override void GenerateCoords(ref int row, ref int column)
			{
				do
				{
					SearchCoords(ref row, ref column);
				} while ((row < 0 || column < 0 || row >= EnemyGrid.Height || column >= EnemyGrid.Width || EnemyGrid[row, column] != TileView.Sea));
				//while inside the grid and not a sea tile do the search
			}


			/// <summary>
			/// SearchCoords will randomly generate shots within the grid as long as its not hit that tile already
			/// </summary>
			/// <param name="row">the generated row</param>
			/// <param name="column">the generated column</param>
			private void SearchCoords(ref int row, ref int column)
			{
				row = _Random.Next(0, EnemyGrid.Height);
				column = _Random.Next(0, EnemyGrid.Width);
			}

			/// <summary>
			/// ProcessShot will be called uppon when a ship is found.
			/// It will create a stack with targets it will try to hit. These targets
			/// will be around the tile that has been hit.
			/// </summary>
			/// <param name="row">the row it needs to process</param>
			/// <param name="col">the column it needs to process</param>
			/// <param name="result">the result og the last shot (should be hit)</param>

			protected override void ProcessShot(int row, int col, AttackResult result)
			{
			}

			/// <summary>
			/// AddTarget will add the targets it will shoot onto a stack
			/// </summary>
			/// <param name="row">the row of the targets location</param>
			/// <param name="column">the column of the targets location</param>
			private void AddTarget(int row, int column)
			{
			}
		}
}