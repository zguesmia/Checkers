using System;
using System.Diagnostics;
namespace Checkers
{
	/// <summary>
	/// 
	/// </summary>
	public class MoveGenerator
	{
		public MoveGenerator()
		{
			JumpValue[0]=	4;
			JumpValue[1]=	5;
			JumpValue[2]=	-5;
			JumpValue[3]=	-4;
			JumpValue[4]=	4;
			JumpValue[5]=	5;
			AuxPlayer=TPlayer.BLACK;
		
		}
		int[]AuxBoard = new int[55];
		int[] JumpValue = new int[6];
		static TPlayer AuxPlayer;
		MoveList AuxMoveList = new MoveList();
		static int[] PiecePath = new int[64];
		int[] TempPath = new int[64];
		static int NSteps;
		  
		public bool GetNextState(int square)
		{
			int i= (AuxBoard[square]%2)*2 ; // one player 0-->(JV = 4) other player 2-->(JV = -5)
			int EndLoop= i + ( AuxBoard[square]>Game.BKING?2:4 );

			for( ; i<EndLoop ; i++)
			{
				int Dx= JumpValue[i];
				if(Board.IsPiece(AuxBoard[square+Dx]) &&
					Board.BelongsTo(AuxBoard[square+Dx],Board.Other(AuxPlayer)) && // next square is occupied by other player. eat it.
					AuxBoard[square+2*Dx]==Game.EMPTY)		
					return false;
			}
			return true;
		}

		  public int Getselection(int first, int last)
		{
			if(first == last)
				return 0;
			bool state = false;
			for(int j = 0;j< AuxMoveList.NMoves;++j)
			{
				for(int i = 0; i < Game.MAX_MOVE_LENGTH; i++)
				{
					int Last =AuxMoveList.List[j,i], First = AuxMoveList.List[j,0];
					if(AuxMoveList.List[j,i] == 0 && i > 0)
					{
						Last = AuxMoveList.List[j,i-1];
						if((First) == first && Last == last)
							return j + 1;
						break;
					}
					if(First == first && Last == last /*&& CTGame::Board[last] == EMPTY*/)
						state = true;

				}
			}
			if(state)
				return -1;
			return 0;
		}

		public void SaveMove()
		{
			int oldvalue = -1;
			PiecePath[NSteps] = 0; // 0 means end of move
			
			int i = 0;
			int j = 0;
			do
			{
				if(oldvalue != PiecePath[i])
				{
					if(AuxMoveList.NMoves >= Game.MAX_MOVES_PER_POSITION
						|| j >= Game.MAX_MOVE_LENGTH)
						break;
					AuxMoveList.List[AuxMoveList.NMoves,j++]=oldvalue=PiecePath[i];
				}
			}while( PiecePath[i++]!=0 );
			 AuxMoveList.NMoves++;
			return;
		}

		public void SaveTempMove()
		{
			int oldvalue = -1;
			TempPath[NSteps]=0; // 0 means end of move
			  
			int i = 0;
			 int j = 0;
			do
			{
				if(oldvalue != TempPath[i])
				{
					if(AuxMoveList.NMoves >= Game.MAX_MOVES_PER_POSITION
						|| j >= Game.MAX_MOVE_LENGTH)
						break;
					AuxMoveList.List[AuxMoveList.NMoves,j]=oldvalue=TempPath[i];
					j++;
				}
			}while(TempPath[i++]!=0 );
			  AuxMoveList.NMoves++;
			for(i = 0; i < 64; i++)
				TempPath[i] = 0;
			
			return;
		}

		  public bool NoMovesLeft(int[]board, TPlayer Player)
		{
			for( int Sq=0 ; Sq<45 ; ++Sq )
				if( Board.IsPiece(board[Sq]) && Board.BelongsTo(board[Sq],Player))
				{
					int i= (board[Sq]%2)*2; // w --> 0; b --> 2
					int EndLoop= i + ( board[Sq]>Game.BKING?2:4 ); // noking --> i + 2; king --> i + 4
					for( ; i<EndLoop ; i++)
					{
						int Dx = JumpValue[i];
						if(Sq+Dx < 0 || Sq+Dx > 45)
							continue;
						if( board[Sq+Dx]==Game.EMPTY )
						{
							return false;  //can move
						}
						if( Board.IsPiece(board[Sq+Dx])&& Board.BelongsTo(board[Sq+Dx],Board.Other(Player))&&
							board[Sq+2*Dx]==Game.EMPTY)
						{
							return false; //can take a piece so at least a valid move
						}//elseif
					}//for
				}//if and for
			return true;  //no moves left
		}

		  public void InitBoard(int[]board)
		{
			for(int i = 0;i<55;i++)
				board[i]=Game.BORDER;
			for(int  i=10;i<45;i++)
				board[i]=Game.EMPTY;

			for(int i=10;i<23;i++)
				board[i]=Game.WMAN;

			for(int  i=32;i<45;i++)
				board[i]=Game.BMAN;

			board[18]= board[27]= board[36]= Game.BORDER;
		}

		  public int MakeMove(int[]Move,int[]board)
		{
			int Piece=board[Move[0]];
			int[]Last = Move;
			int i = 0;
			do
			{
				Last[i]= Move[i];
				board[Move[i]]=Game.EMPTY;
			}while(Move[++i]!=0);
			if( (Last[i-1]<14 || Last[i-1]>40) && Piece>Game.BKING ) //Check if man becomes a King
				Piece-=2;
			board[Last[i-1]]=Piece;
			return Last[i-1];
		}

		public void MakeMoveList(int[]board, TPlayer Player, MoveList MList)
		{
			if(AuxMoveList.NMoves >= Game.MAX_MOVES_PER_POSITION)
				AuxMoveList.NMoves = 0;
			NSteps = 0;
			MList.NMoves =  0;
			MList.MustTake= false;
			AuxBoard = board;
			AuxPlayer= Player;
			AuxMoveList = MList;
			for(int i = 10 ; i < 45 ; i++)
				if( Board.IsPiece(board[i]) && Board.BelongsTo(board[i],Player) )
				{

					MakeMoveListAux(i,0,-1,-1,false,false,false,false);
					int j = 0;
					while(AuxMoveList.List[i,j]!=0)
						Debug.Write(AuxMoveList.List[i,j++].ToString());
				}

			
		}

		public void MakeMoveListAux(int Square, int Depth, int index, int ndx, bool state, bool go_on, bool wasdam, bool stop)
		{
			PiecePath[NSteps++]= Square;
			bool MustSaveMove= true;
			int i= (AuxBoard[Square]%2)*2 ; // one player 0-->(JV = 4) other player 2-->(JV = -5)
			int EndLoop= i + ( AuxBoard[Square]> Game.BKING?2:4 );

			for( ; i<EndLoop ; i++)
			{
				int Dx= JumpValue[i];
				if( AuxBoard[Square+Dx] == Game.EMPTY && (!AuxMoveList.MustTake || go_on))
				{ 
					// next square is empty. Simple move
					int temp = Square+Dx;
					
					//Debug.WriteLine(temp.ToString());
					//Debug.WriteLine(AuxBoard[Square].ToString());
					if(Board.m_Damma && AuxBoard[Square] <=  Game.BKING) // Mediterranean style
					{	
						//Debug.WriteLine("18\n");
						if(NSteps==1)
						{
							index = Dx;
							//state =  true;
						}
						else if(ndx != -1 && go_on)
						{
							if(ndx != Dx)
								continue;
						}
						else if(index != Dx)
							continue;
						if(go_on && AuxMoveList.MustTake && NSteps >1)
						{
						for(int n = 0; n < NSteps; n++)
								TempPath[n] = PiecePath[n];
				
							SaveTempMove();
						
						}
						else if(!AuxMoveList.MustTake && NSteps >1)
						{
	
							for(int m  = 0; m < NSteps; m++)
								TempPath[m] = PiecePath[m];
							SaveTempMove();
						}
						PiecePath[NSteps++]= Square+Dx;
			
						AuxBoard[Square+Dx]= AuxBoard[Square];
						AuxBoard[Square]=  Game.EMPTY;
						//bool st = true;
						bool stp = false;
						/*
						if(stop)
						{
							m_square = NSteps;
							stp =false;
		
						}
						*/
			
			
						
							MakeMoveListAux(Square+Dx, Depth+1, index, ndx, true, true, true, stp);
			
			
						AuxBoard[Square]= AuxBoard[Square + Dx];
						AuxBoard[Square+Dx]=  Game.EMPTY;
						NSteps--;
						MustSaveMove= false;
						index =-1;
				

					}

					else if( NSteps==1 )
					{
						PiecePath[NSteps++]= Square+Dx;
						SaveMove();
						--NSteps;
					}
				}
				else if( Board.IsPiece(AuxBoard[Square+Dx]) &&
					 Board.BelongsTo(AuxBoard[Square+Dx],Board.Other(AuxPlayer)) && // next square is occupied by other player. eat it.
					AuxBoard[Square+2*Dx]== Game.EMPTY /*&& !stop*/)
				{
				//	Debug.WriteLine("2\n");
					if(index != Dx && state)
						continue;

					if(!AuxMoveList.MustTake)
					{
						AuxMoveList.MustTake= true;
						AuxMoveList.NMoves= 0;
					}

					if(Board.m_Damma && AuxBoard[Square] <=  Game.BKING && !go_on)
						wasdam = true;
					PiecePath[NSteps++]= Square+Dx;

					AuxBoard[Square+2*Dx]= AuxBoard[Square];
					int Save= AuxBoard[Square+Dx];
					AuxBoard[Square+Dx]= AuxBoard[Square]=  Game.EMPTY;
					bool s = false;
					if(wasdam && GetNextState(Square+2*Dx))
						s = true;

					MakeMoveListAux(Square+2*Dx, Depth+1,-1, Dx, false, s, wasdam,false);
					AuxBoard[Square]= AuxBoard[Square+2*Dx];
					AuxBoard[Square+Dx]= Save;
					AuxBoard[Square+2*Dx]=  Game.EMPTY;

					NSteps--;
					MustSaveMove= false;
					index =-1;
				}//elseif
			}//for
			if( Depth != 0 && MustSaveMove )
				SaveMove();
			--NSteps;
			return;
		}
	}
}
