
using System;
using System.Collections;
using System.Diagnostics;
namespace Checkers
{
	

	/// <summary>
	/// 
	/// </summary>
	/// 

	public class TSearcher
	{
		public MoveGenerator TMoveGenerator = new MoveGenerator();
		public int [,] SavingArray;
		int MaxDepth;
		public int SearchDepth;
		char[] MoveInfoStr = new char[128];
	
		ArrayList MoveListArray = new ArrayList();
		MoveList[] AList  = new MoveList[Game.MAX_DEPTH];

		



		
		  long NEvaluated;
		  long NNodes;
		public TSearcher()
		{
			SavingArray = new int[Game.MAX_DEPTH,Game.MAX_MOVE_LENGTH];
			for(int i = 0; i <SavingArray.GetLength(0); i++)
				for(int j = 0; j < Game.MAX_MOVE_LENGTH; j++)
					SavingArray[i,j] = 0;
			for(int i = 0; i < Game.MAX_DEPTH; i++)
			{
				//MoveList ml = new MoveList();
				AList[i] = new MoveList();
			}
		}
		  public int AlphaBeta(int[]board, int Depth, TPlayer Player, int Lowest, int Highest, int RealDepth)
		{
			NNodes++;
			if(RealDepth>MaxDepth)
				MaxDepth=RealDepth;

			//MoveListArray[RealDepth] = new MoveList();
			//MoveList MList= (MoveList)MoveListArray[RealDepth];
			TMoveGenerator.MakeMoveList(board, Player, AList[RealDepth]);
			if(AList[RealDepth].NMoves == 0)
			{
				int result = -10000 + RealDepth-1;
				return result;
			}

			/*
			Quiescence search:
			Expands capturing moves
			*/
			if(Depth < 1 && AList[RealDepth].MustTake)
				Depth+= 1;

			if(Depth < 1 || RealDepth >= Game.MAX_DEPTH-1)
			{
				++NEvaluated;
				return Board.Evaluation(board,Player);
			}

			int Best =- Board.INFINITY;
			for(int i = 0; i < AList[RealDepth].NMoves && Best < Highest; i++)
			{
				MakeMove( board, AList[RealDepth].List, SavingArray,i,RealDepth);
				int Score =- AlphaBeta(board, Depth-1,Board.Other(Player),-Highest, -Lowest,
					RealDepth+1);
				UndoMove(board, AList[RealDepth].List,SavingArray,i,RealDepth);
				if(Score > Best)
				{
					Best = Score;
					if(Score > Lowest)
						Lowest = Best;
				}
			}
			return Best;
		}
		  //TMoveInfo Temp;
		 public TMoveInfo AlphaBeta0(int[]board, int Depth, TPlayer Player)
		{
			TMoveInfo Temp = new TMoveInfo();
			int Lowest= -Board.INFINITY;
			int Highest= Board.INFINITY;
			 
			
			// MoveListArray[0] = new MoveList();
		//	 MoveList MList = (MoveList)MoveListArray[0];
			TMoveGenerator.MakeMoveList(board, Player, AList[0]);

			int Best = -Board.INFINITY;
			int BestPos=0;
			for(int i=0;i<(AList[0]).NMoves&&Best<Highest;i++)
			{
				MakeMove(board,(AList[0]).List,SavingArray,i,0);
				int Score= -AlphaBeta(board, Depth-1, Board.Other(Player), -Highest, -Lowest,1);
				UndoMove(board, (AList[0]).List,SavingArray,i,0);
				if(Score>Best)
				{
					Best= Score;
					BestPos= i;
					if(Score>Lowest)
						Lowest= Best;
				}
			}
			
			Temp.Evaluation= Best;
			for(int h = 0; h < (AList[0]).List.GetLength(1); h++)
				Temp.Move[h] = (AList[0]).List[BestPos,h];
			
			return Temp;
		}

		 public void CopyMove(int[]Dest, int[]Src)
		{
			int i = 0;
			do
			{
				Dest[i]=Src[i];
			}while(Src[++i]!=0);
			Dest[i]=0;
		}
		 
		public void GetComputerMove(int[]Board, TPlayer Player, int[]Move)
		{
			TMoveInfo MoveInfo  = new TMoveInfo();
			MaxDepth=0;
			NEvaluated=0;
			NNodes=0;
			if(!IsOnlyMove(MoveInfo, Board, Player))
				MoveInfo= AlphaBeta0(Board,SearchDepth,Player);
			CopyMove(Move, MoveInfo.Move);
			//int i = 0;
			//do
			//{
				///string temp = String.Format("{0}",Move[i]);
				//Debug.WriteLine(Move[i].ToString());
			//}while(Move[++i]!=0);
	

		}

		public string GetMoveInfoStr()
		{
			return null;
		}

		public bool IsOnlyMove(TMoveInfo Info, int[]Board, TPlayer Player)
		{
			//MoveListArray[0] = new MoveList();
			//MoveList MList= (MoveList)MoveListArray[0];
			TMoveGenerator.MakeMoveList(Board,Player,AList[0]);
			if(AList[0].NMoves!=1)
				return false;
			for(int h = 0; h <AList[0].List.GetLength(1); h++)
				Info.Move[h] = AList[0].List[0,h];
	//		Info.Move= MoveListArray[0].List[0];
			return true;
		}

		public void MakeMove(int[]Board, int[,]Move, int[,]Save, int index,int Sidx)
		{
			int Piece= Board[Move[index,0]];
			int i = 0;
			do
			{
				Save[Sidx,i] = Board[Move[index,i]];
				Board[Move[index,i++]]= Game.EMPTY;
			}while(Move[index,i] != 0);
			
			int LastSquare= Move[index,i-1];
			if((LastSquare<14||LastSquare>40)&& Piece>Game.BKING)
				Piece-=2; //man becomes a king
			Board[LastSquare] = Piece;
		}

		  public void SetSearchDepth(int _SearchDepth)
		{
			 SearchDepth = _SearchDepth; 
		}

		public int SquareToNotation(int Sq)
		{
			int n= 45-Sq;
			if(Sq<36)n--;
			if(Sq<27)n--;
			if(Sq<18)n--;
			return n;
   
		}

		public void UndoMove(int[]Board, int[,]Move, int[,]Save, int index, int Sidx)
		{
			int i = 0;
			do
			{
				Board[Move[index,i]] = Save[Sidx,i];
			}while(Move[index,i++]!=0);
		}

		public bool IsWinOrLoseMove(int e)
		{
			return Math.Abs(e)>Board.INFINITY-500; 
		}

		
		
	}
}
