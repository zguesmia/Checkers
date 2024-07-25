using System;

namespace Checkers
{
	/// <summary>
	/// 
	
	public class MoveList
	{
		public int [,] List;
		public int NMoves;
		public bool MustTake;

		public MoveList()
		{
			List = new int[Game.MAX_MOVES_PER_POSITION,Game.MAX_MOVE_LENGTH];
			for(int i = 0; i <Game.MAX_MOVES_PER_POSITION; i++)
				for(int j = 0; j < Game.MAX_MOVE_LENGTH; j++)
					List[i,j] = 0;
			NMoves = 0;
			MustTake = false;
		}
	}
}
