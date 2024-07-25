using System;

namespace Checkers
{
	public enum  TPlayer { WHITE , BLACK };
	enum TPlayerType{HUMAN,COMPUTER};
	public class TMoveInfo
	{
		public TMoveInfo()
		{
			Evaluation = 0;
			for(int i = 0; i < Game.MAX_MOVE_LENGTH; i++)
				Move[i] = 0;

		}
		public int Evaluation;
		public int[]Move = new int[Game.MAX_MOVE_LENGTH];
	}
	/// <summary>
	/// 
	/// </summary>
	public class Game
	{
		public const int MAX_MOVES_PER_POSITION = 100;
		public const int MAX_MOVE_LENGTH = 200;
		public const int MAX_DEPTH = 100;
		public  const int WKING  =	0;
		public  const int BKING	 =	1;
		public  const int WMAN	 =	2;
		public  const int BMAN	 =	3;
		public  const int EMPTY	 =	4; 
		public  const int BORDER =	5;
		public Game()
		{

			TMoveGenerator.InitBoard(GBoard);
			NMove=0;
			m_EndGame = false;
			SarchDepth[(int)TPlayer.BLACK]=SarchDepth[(int)TPlayer.WHITE]= 5;
			Turn= TPlayer.BLACK;
		}
	
		public TSearcher Searcher = new TSearcher();
		  int NMove;
		int[] SarchDepth = new int[2];
		  bool m_EndGame;
		public   int m_CompLast;
		public   int m_CompFirst;
		public   TPlayer Turn;
		public   char[,] PlayerName = new char[2,12];
		public   int m_last;
		public   int m_first;
		public   int[] GBoard = new int[55]; 
		public MoveGenerator TMoveGenerator = new MoveGenerator();
		public void ComputerResponds(int[] Move)
		{
			NMove++;
			
			Searcher.SetSearchDepth(5);
			Searcher.GetComputerMove(GBoard,Turn,Move);
	   	

			m_CompFirst = Move[0];
			m_CompLast = TMoveGenerator.MakeMove(Move,GBoard);

			Turn= Board.Other(Turn);
		}
		//  MoveList MList;
		  public bool GetHumanMove(int[] Board, TPlayer Player, int[]Move)
		{
			MoveList MList = new MoveList();
			TMoveGenerator.MakeMoveList(Board,Player,MList);
			int Selection = TMoveGenerator.Getselection(m_first, m_last);
			if(Selection>=1)
			{
				int i = 0;
				do
				{
					Move[i]=MList.List[Selection-1,i];
				}while(MList.List[Selection-1,++i]!=0);
				Move[i]=0;

				//	Searcher.CopyMove(ref Move, ref MList.List, Selection-1);
			}
			return true;
		}

		  public bool GetMyMove()
		{
			//bool EndOfGame= false;
			int[] Move =  new int[Game.MAX_MOVE_LENGTH];
			NMove++;
			bool state = GetHumanMove(GBoard,Turn,Move);
			if(state)
				Turn= Board.Other(Turn);
		
			return state;
		}

		  public void SetEndGame(bool end)
		{
			m_EndGame = end;
			Turn= Board.Other(Turn);
		}
	}
}
