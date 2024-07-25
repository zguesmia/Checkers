using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing.Printing;
using System.Reflection;

namespace Checkers
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class Board : System.Windows.Forms.UserControl
	{
		public string GetBitmapDir()
		{
			return @"C:\MyProjects\Checkers\CheckersBoard\";
		}
		private System.Windows.Forms.CheckBox DammaCheckBox;
		private System.Windows.Forms.RadioButton YouRadioButton;
		private System.Windows.Forms.RadioButton CompRadioButton;
		private System.Windows.Forms.RadioButton WhiteRadioButton;
		private System.Windows.Forms.RadioButton BlackRadioButton;
		private System.Windows.Forms.Button StartOverButton;
		private System.Windows.Forms.Button SwipeSidesButton;
		private System.ComponentModel.IContainer components;
		// ArrayList for board tile images
		ArrayList checkersTile = new ArrayList();

		
		ArrayList CheckersPieces = new ArrayList();
		public ArrayList WResultPieces = new ArrayList();
		public ArrayList BResultPieces = new ArrayList();
		public Game m_Game = new Game();

	
		int[] board = new int[BOARD_SIZE];
		int[] m_ImageBoard = new int[BOARD_SIZE];
		int[] m_OldImageBoard = new int[BOARD_SIZE];
		int[] m_OldBoard = new int[55];
		public ArrayList m_LastMove = new ArrayList();
		public ArrayList GB = new ArrayList();

		TPlayerType[] PlayerType = new TPlayerType[2];

		
		int selectedIndex = -1;
		int m_NumWhites;
		int m_NumBlacks;
		bool m_blackwhite;
		
		int m_StartPos;
		int m_InitImage;
		
		Rectangle m_Rect = new Rectangle(0,0,TILESIZE,TILESIZE);
		bool m_SideChanged;
		ArrayList m_PosList = new ArrayList();
		ArrayList m_PointList = new ArrayList();
		Point m_translation = new Point(0,0);
		int m_compos;
		int m_EndPos;
		int m_selectedImage;
		int square1, square2;
		int OldImage;
		static public bool m_Damma ;
		private System.Windows.Forms.PictureBox pieceBox; // board array
		private FileStream Output;
		private BinaryFormatter formatter = new BinaryFormatter();
		// define chess tile size in pixels
		private const int TILESIZE = 50;
		

		public  const int BOARD_SIZE =	64;

		public const int BACKRANK =	10;
		public const int CHECKER  =	100;
		public const int KING	  =	130;

		public const int INFINITY =				10000;

		public const int BLACK_FIELD =			0;
		public const int BKING_FIELD =			1;
		public const int WHITE_FIELD =			2;
		public const int WKING_FIELD =			3;
		public const int BLANK_FIELD =			4;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		Bitmap whitePieces;
		Bitmap RWhitePieces;
		
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		
		private System.Drawing.Printing.PrintDocument printDoc;
		
		
		private System.Windows.Forms.PictureBox ResultBox;

							
		

		public Board()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			m_NumWhites = 0;
			m_NumBlacks = 0;
			m_StartPos = 0;
			m_InitImage = 12;
			m_SideChanged = false;
			m_EndPos = 0;
			m_selectedImage = BLANK_FIELD;
			ReSetFigures();
			m_Damma = DammaCheckBox.Checked;
			m_blackwhite = BlackRadioButton.Checked;
			Assembly asbly =  Assembly.GetExecutingAssembly();
			Stream stream = asbly.GetManifestResourceStream("Checkers.transpar.png");
			whitePieces = new Bitmap(stream);
			stream = asbly.GetManifestResourceStream("Checkers.Rtranspar.png");
			RWhitePieces = new Bitmap(stream);
			

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.printDoc = new System.Drawing.Printing.PrintDocument();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.ResultBox = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.WhiteRadioButton = new System.Windows.Forms.RadioButton();
			this.BlackRadioButton = new System.Windows.Forms.RadioButton();
			this.DammaCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CompRadioButton = new System.Windows.Forms.RadioButton();
			this.YouRadioButton = new System.Windows.Forms.RadioButton();
			this.pieceBox = new System.Windows.Forms.PictureBox();
			this.SwipeSidesButton = new System.Windows.Forms.Button();
			this.StartOverButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Text = "Checkers";
			this.statusBarPanel2.ToolTipText = "Checkers";
			this.statusBarPanel2.Width = 62;
			// 
			// statusBar1
			// 
			this.statusBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.statusBar1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.statusBar1.Location = new System.Drawing.Point(-4, 424);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1,
																						  this.statusBarPanel2});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(608, 26);
			this.statusBar1.TabIndex = 23;
			this.statusBar1.Text = "Ready";
			this.statusBar1.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.statusBar1_PanelClick);
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Text = "Damma";
			this.statusBarPanel1.ToolTipText = "Damma";
			this.statusBarPanel1.Width = 530;
			// 
			// ResultBox
			// 
			this.ResultBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ResultBox.BackColor = System.Drawing.Color.Transparent;
			this.ResultBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ResultBox.Location = new System.Drawing.Point(8, 17);
			this.ResultBox.Name = "ResultBox";
			this.ResultBox.Size = new System.Drawing.Size(170, 258);
			this.ResultBox.TabIndex = 22;
			this.ResultBox.TabStop = false;
			this.ResultBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ResultBox_Paint);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.groupBox2.Controls.Add(this.WhiteRadioButton);
			this.groupBox2.Controls.Add(this.BlackRadioButton);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.ForeColor = System.Drawing.Color.Black;
			this.groupBox2.Location = new System.Drawing.Point(8, 337);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(170, 32);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "You Color Choice";
			// 
			// WhiteRadioButton
			// 
			this.WhiteRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.WhiteRadioButton.ForeColor = System.Drawing.Color.Black;
			this.WhiteRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.WhiteRadioButton.Location = new System.Drawing.Point(6, 14);
			this.WhiteRadioButton.Name = "WhiteRadioButton";
			this.WhiteRadioButton.Size = new System.Drawing.Size(54, 14);
			this.WhiteRadioButton.TabIndex = 0;
			this.WhiteRadioButton.Text = "White";
			this.WhiteRadioButton.Click += new System.EventHandler(this.WhiteRadioButton_CheckedChanged);
			// 
			// BlackRadioButton
			// 
			this.BlackRadioButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BlackRadioButton.Checked = true;
			this.BlackRadioButton.ForeColor = System.Drawing.Color.Black;
			this.BlackRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.BlackRadioButton.Location = new System.Drawing.Point(102, 12);
			this.BlackRadioButton.Name = "BlackRadioButton";
			this.BlackRadioButton.Size = new System.Drawing.Size(54, 14);
			this.BlackRadioButton.TabIndex = 1;
			this.BlackRadioButton.TabStop = true;
			this.BlackRadioButton.Text = "Black";
			this.BlackRadioButton.Click += new System.EventHandler(this.BlackRadioButton_CheckedChanged);
			// 
			// DammaCheckBox
			// 
			this.DammaCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.DammaCheckBox.Checked = true;
			this.DammaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DammaCheckBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DammaCheckBox.ForeColor = System.Drawing.Color.Black;
			this.DammaCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.DammaCheckBox.Location = new System.Drawing.Point(10, 279);
			this.DammaCheckBox.Name = "DammaCheckBox";
			this.DammaCheckBox.Size = new System.Drawing.Size(66, 25);
			this.DammaCheckBox.TabIndex = 16;
			this.DammaCheckBox.Text = "Damma";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.groupBox1.Controls.Add(this.CompRadioButton);
			this.groupBox1.Controls.Add(this.YouRadioButton);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 303);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(170, 34);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Who Start First?";
			// 
			// CompRadioButton
			// 
			this.CompRadioButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.CompRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.CompRadioButton.Location = new System.Drawing.Point(4, 12);
			this.CompRadioButton.Name = "CompRadioButton";
			this.CompRadioButton.Size = new System.Drawing.Size(74, 16);
			this.CompRadioButton.TabIndex = 0;
			this.CompRadioButton.Text = "Computer";
			this.CompRadioButton.Click += new System.EventHandler(this.CompRadioButton_CheckedChanged);
			// 
			// YouRadioButton
			// 
			this.YouRadioButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.YouRadioButton.Checked = true;
			this.YouRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.YouRadioButton.Location = new System.Drawing.Point(100, 12);
			this.YouRadioButton.Name = "YouRadioButton";
			this.YouRadioButton.Size = new System.Drawing.Size(42, 16);
			this.YouRadioButton.TabIndex = 1;
			this.YouRadioButton.TabStop = true;
			this.YouRadioButton.Text = "You";
			this.YouRadioButton.Click += new System.EventHandler(this.YouRadioButton_CheckedChanged);
			// 
			// pieceBox
			// 
			this.pieceBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pieceBox.BackColor = System.Drawing.Color.Transparent;
			this.pieceBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pieceBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pieceBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.pieceBox.Location = new System.Drawing.Point(194, 10);
			this.pieceBox.Name = "pieceBox";
			this.pieceBox.Size = new System.Drawing.Size(404, 402);
			this.pieceBox.TabIndex = 21;
			this.pieceBox.TabStop = false;
			this.pieceBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pieceBox_Paint_1);
			this.pieceBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pieceBox_MouseUp);
			this.pieceBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pieceBox_MouseMove);
			this.pieceBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pieceBox_MouseDown);
			// 
			// SwipeSidesButton
			// 
			this.SwipeSidesButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.SwipeSidesButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SwipeSidesButton.ForeColor = System.Drawing.Color.Black;
			this.SwipeSidesButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.SwipeSidesButton.Location = new System.Drawing.Point(8, 393);
			this.SwipeSidesButton.Name = "SwipeSidesButton";
			this.SwipeSidesButton.Size = new System.Drawing.Size(170, 20);
			this.SwipeSidesButton.TabIndex = 20;
			this.SwipeSidesButton.Text = "Swipe Sides";
			this.SwipeSidesButton.Click += new System.EventHandler(this.SwipeSidesButton_Click);
			// 
			// StartOverButton
			// 
			this.StartOverButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.StartOverButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StartOverButton.ForeColor = System.Drawing.Color.Black;
			this.StartOverButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.StartOverButton.Location = new System.Drawing.Point(8, 372);
			this.StartOverButton.Name = "StartOverButton";
			this.StartOverButton.Size = new System.Drawing.Size(170, 20);
			this.StartOverButton.TabIndex = 19;
			this.StartOverButton.Text = "Start Over";
			this.StartOverButton.Click += new System.EventHandler(this.StartOverButton_Click);
			// 
			// Board
			// 
			this.BackColor = System.Drawing.Color.Sienna;
			this.Controls.Add(this.DammaCheckBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pieceBox);
			this.Controls.Add(this.SwipeSidesButton);
			this.Controls.Add(this.StartOverButton);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.ResultBox);
			this.Controls.Add(this.groupBox2);
			this.Name = "Board";
			this.Size = new System.Drawing.Size(600, 449);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		static public TPlayer Other( TPlayer Player )
		{
			return (TPlayer)(1-(int)Player);
		}
		static public bool IsPiece( int Value )
		{
			return Value < BLANK_FIELD;
		}
		static public bool BelongsTo( int Piece, TPlayer Player)
		{
			return Piece%2==(int)Player;
		}
		private void ResetBoard()
		{
			CheckersPiece piece;
			Random random = new Random();
			// ensure empty arraylist
			CheckersPieces = new ArrayList();
			int y = 0;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0 && i > 0)
					++y;
				if(GetFigurImage(i) != BLANK_FIELD)
				{
					piece = new CheckersPiece( GetFigurImage(i), 
						(i%8) * TILESIZE, y * TILESIZE-10, i,
						whitePieces,TILESIZE,TILESIZE );

					CheckersPieces.Add( piece );
				}
				board[i] = (i+y+1)%2;
			}
			FillStatusBar1();
		}
		
		

		

		private CheckersPiece GetPiece(int i)
		{
			return (CheckersPiece)CheckersPieces[i];
		}
		private CheckersPiece GetWResultPiece(int i)
		{
			return (CheckersPiece)WResultPieces[i];
		}
		private CheckersPiece GetBResultPiece(int i)
		{
			return (CheckersPiece)BResultPieces[i];
		}
		private int CheckBounds(Point point, int exclude)
		{
			Rectangle rectangle;
			for(int i = 0; i < CheckersPieces.Count; i++)
			{
				rectangle = GetPiece(i).GetBounds();
				if(rectangle.Contains(point) && i != exclude)
					return i;
			}
			return -1;
		}

		private void pieceBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			m_ImageBoard.CopyTo(m_OldImageBoard,0);
			Point pnt = new Point(e.X, e.Y);
			selectedIndex = CheckBounds(pnt, -1);
			if(selectedIndex>-1)
				m_Rect = GetPiece(selectedIndex).GetBounds();
			int selectedField = ((pnt.Y / TILESIZE) * 8) + (pnt.X / TILESIZE);
			//GetPiece(selectedIndex).m_spot = selectedField;

			if ( m_SideChanged )
				selectedField = BOARD_SIZE -1 - (((pnt.Y / TILESIZE) * 8) + (pnt.X / TILESIZE));
			if ((m_selectedImage = GetFigurImage(selectedField)) == BLANK_FIELD)
				return;
			OldImage = GetFigurImage(selectedField);

			SetFigurImage(selectedField, BLANK_FIELD);
			square1 = selectedField;
		}

		private void pieceBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(selectedIndex > -1)
			{
				Rectangle region = new Rectangle(
					e.X - TILESIZE *2, e.Y - TILESIZE * 2,
					TILESIZE * 4, TILESIZE *4);
				GetPiece(selectedIndex).SetLocation(
					e.X - TILESIZE/2,e.Y - TILESIZE/2, true);
				pieceBox.Invalidate(region);
			}
		}

		private void pieceBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( selectedIndex == -1 )
				return;
			int remove = -1;
			Point current = new Point( e.X, e.Y );
			if ( !m_SideChanged )
				square2 = ((current.Y / TILESIZE) * 8) + (current.X/ TILESIZE);
			else
				square2 = BOARD_SIZE - 1 - (((current.Y / TILESIZE) * 8) + (current.X / TILESIZE));

			if(IsInWSpot(square2, square1))
			{
				GetPiece( selectedIndex ).SetLocation( m_Rect.X,
					m_Rect.Y, false );
				// piece is in wrong spot, back track
				SetFigurImage(square1, m_selectedImage);
				pieceBox.Invalidate();
			}
			else
			{
				// move to new spot
				SetFigurImage(square2, m_selectedImage);
				int type =-1;
				if((OldImage == BLACK_FIELD) && (square2 < 8))
				{
					// we have a black king
					SetFigurImage(square2 , 1);
					type = Game.BKING;
					
				}
				if((OldImage == WHITE_FIELD) && (square2 > 55))
				{
					// we have a white king					
					SetFigurImage(square2 , WKING_FIELD);
					type = Game.WKING;
				}
				Point newPoint = new Point( 
					current.X - ( current.X % TILESIZE ),
					current.Y - ( current.Y % TILESIZE )-10 );
				// check bounds with point, exclude whitePieces piece
				remove = CheckBounds( current, selectedIndex );
				if(type > -1)
					GetPiece(selectedIndex).MorpheTo(type);
				// snap piece into center of closest square
				GetPiece( selectedIndex ).SetLocation( newPoint.X,
					newPoint.Y, true);
				int selectedField = ((current.Y / TILESIZE) * 8) + (current.X / TILESIZE);
				if(m_SideChanged)
					selectedField = BOARD_SIZE -1  - selectedField;

				GetPiece(selectedIndex).m_spot = selectedField;
				pieceBox.Invalidate();
				Update();
				
				//reset first and last pos in game
				m_Game.m_first = GetBoardElement(square1);
				m_Game.m_last = GetBoardElement(square2);
				// replace con pieces with blank
				EatCon(square2, square1, OldImage);

				if(!m_Game.GetMyMove())
				{
					MapBoardToImage(m_Game.GBoard, false);
				}
				else
				{

					if(!ComputerMove())
					{
						// Human won game
						Reset();
						m_selectedImage = BLANK_FIELD;
						pieceBox.Invalidate();
						return;
					}
					
					//Computer moves piece
					TranslateMove();
					
					if(m_Game.TMoveGenerator.NoMovesLeft(m_Game.GBoard,m_Game.Turn))
					{
						// Human lost game
						m_Game.SetEndGame(true);
						MessageBox.Show("You have lost. End of game","Checkers",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						// reset all doc variables
						Reset();
						
						if(m_blackwhite)
							OnBlack();
						else
							OnWhite();
					}
				}
			}

			ResetPieces();
			selectedIndex = -1;
			m_selectedImage = BLANK_FIELD;
			pieceBox.Invalidate();
		}

		static public int Evaluation(int[]Board, TPlayer Player)
		{
			int Score=0 ;
			int NWKings=0, NBKings=0, NWMen=0, NBMen=0;

			for( int i=10 ; i<45 ; i++ )
			{
				if(IsPiece(Board[i]))
				{
					int Piece= Board[i];

					switch(Piece)
					{
						case Game.WMAN:
							NWMen++;
							break;

						case Game.BMAN:
							NBMen++;
							break;

						case Game.WKING:
							NWKings++;
							break;

						case Game.BKING:
							NBKings++;
							break;
					}
				}
			}

			if(Board[10]==Game.WMAN&&Board[12]==Game.WMAN&&NBMen>1)
				Score-=BACKRANK;

			if(Board[44]==Game.BMAN&&Board[42]==Game.BMAN&&NWMen>1)
				Score+=BACKRANK;

			int MaterialBlack= NBKings*KING+NBMen*CHECKER;
			int MaterialWhite= NWKings*KING+NWMen*CHECKER;

			//hints to exchange pieces if ahead in material
			Score+= ((MaterialBlack-MaterialWhite)*200)
				/(MaterialBlack+MaterialWhite);

			Score+=MaterialBlack-MaterialWhite;
			return Player==TPlayer.BLACK?Score:-Score;
		}

		public void MapImageToBoard()
		{
			int y = 0;
			int w = 13;
			int temp = 3;
			int m = 7;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0  && i > 0)
				{
					++y;
					m -= temp;
					w +=  (m + 3);
					temp = -temp;
				}
		
				if(((i + y)%2) == 0)
				{
					if(i > 0)
						w -= 3;
					if((i + w) >= 0 && (i + w) < 45)
					{
						switch(m_ImageBoard[i])
						{
							case BLACK_FIELD:	m_Game.GBoard[i + w] = Game.BMAN;	break;
							case BKING_FIELD:	m_Game.GBoard[i + w] = Game.BKING;	break;
							case WHITE_FIELD:	m_Game.GBoard[i + w] = Game.WMAN;	break;
							case WKING_FIELD:	m_Game.GBoard[i + w] = Game.WKING;	break;
							case BLANK_FIELD:	m_Game.GBoard[i + w] = Game.EMPTY;	break;
						}
					}
				
				}
			}
		}

		public int GetBoardElement(int pos)
		{
			int y = 0;
			int w = 13;
			int temp = 3;
			int m = 7;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0  && i > 0)
				{
					++y;
					m -= temp;
					w +=  (m + 3);
					temp = -temp;
				}
				if(((i + y)%2)==0)
				{
					if(i > 0)
						w -= 3;
					if(i == pos)
						return i + w;
				}
			}
			return 0;
		}

		public int GetImageElement(int pos)
		{
			int y = 0;
			int w = 13;
			int temp = 3;
			int m = 7;						

			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0  && i > 0)
				{
					++y;
					m -= temp;
					w +=  (m + 3);
					temp = -temp;
				}
				if(((i + y)%2)==0)
				{
					if(i > 0)
						w -= 3;
					if(pos == i + w) 
						return i;
				}
			}
			return 0;
		}

		public void MapBoardToImage(int[] Board, bool state)
		{
			for(int j = 10; j < 45; j++)
			{
				int y = 0;
				int w = 13;
				int temp = 3;
				int m = 7;
				for(int i = 0; i < BOARD_SIZE; i++)
				{
					if((i % 8)==0  && i > 0)
					{
						++y;
						m -= temp;
						w +=  (m + 3);
						temp = -temp;
					}
					if(((i + y)%2)==0)
					{
						if(i > 0)
							w -= 3;
						if((j == i + w) && (j < 45)  && i  < BOARD_SIZE)
						{
							switch(Board[j])
							{
								case Game.BMAN:	m_ImageBoard[i]	= BLACK_FIELD; m_NumBlacks++; break;
								case Game.BKING: 
								{
									m_ImageBoard[i]	= BKING_FIELD; 
									m_NumBlacks++;
									int count = CheckersPieces.Count;
									for(int l = 0; l < count; l++)
									{
										int t = GetPiece(l).m_spot;
										if(m_SideChanged)
											t = BOARD_SIZE -1 - t;
										if(t == i && GetPiece(l).GetCurrentType() > Game.BKING)
										{
											GetPiece(l).MorpheTo(( int )CheckersPiece.Types.BKING);
											break;
										}
									}
									break;
								}
								case Game.WMAN:	m_ImageBoard[i]	= WHITE_FIELD; m_NumWhites++;break;
								case Game.WKING: 
								{
									m_ImageBoard[i]	= WKING_FIELD; 
									m_NumWhites++;
									int count = CheckersPieces.Count;
									for(int l = 0; l < count; l++)
									{
										int t = GetPiece(l).m_spot;
										if(m_SideChanged)
											t = BOARD_SIZE -1 - t;
										
										if(t == i && GetPiece(l).GetCurrentType() > Game.BKING)
										{
											GetPiece(l).MorpheTo(( int )CheckersPiece.Types.WKING);
											break;
										}
									}
									break;
								}
								case Game.EMPTY:	m_ImageBoard[i]	= BLANK_FIELD; break;
							}
						}
					 
					}
				}
			}
			if(!state)
			{
				m_NumBlacks = 0;
				m_NumWhites = 0;
			}
		}

		private void YouRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!m_blackwhite)
				m_Game.Turn = TPlayer.WHITE;
			else
				m_Game.Turn = TPlayer.BLACK;
		}

		private void CompRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!m_blackwhite)
				m_Game.Turn = TPlayer.BLACK ;
			else
				m_Game.Turn = TPlayer.WHITE;
		
			if(CompRadioButton.Checked && ComputerMove())//Computer moves piece
				TranslateMove();
		}

		public bool ComputerMove()
		{
			SwipeSidesButton.Enabled = false;
			BlackRadioButton.Enabled = false;
			WhiteRadioButton.Enabled = false;
			CompRadioButton.Enabled = false;
			YouRadioButton.Enabled = false;
			DammaCheckBox.Enabled = false;

			SaveBoard();
			MapImageToBoard();
			int[] Move = new int[Game.MAX_MOVE_LENGTH];
			int i  = 0, pos = 0;
			int turn = (int)m_Game.Turn;
			if(m_Game.TMoveGenerator.NoMovesLeft(m_Game.GBoard,m_Game.Turn))
			{
				m_Game.SetEndGame(true);
				if(DammaCheckBox.Checked)
					MessageBox.Show("Sahit sahbi!. Nestahref bik!","Checkers",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				else
					MessageBox.Show("Congratulations!. You have won","Checkers",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				
				return false;
			}
			m_Game.ComputerResponds(Move);
			CopyLastMove(Move);
			int j = 0;
			do
			{
				pos = GetImageElement(Move[j]);
			
				if(i == 0)
				{
					m_StartPos = pos;
					m_InitImage= GetFigurImage(pos);
					SetFigurImage(pos , BLANK_FIELD);
				}
				if(m_SideChanged)
					pos = BOARD_SIZE - 1 - pos;
				m_PosList.Add(pos);

				m_compos = pos;
				i++;
			}while(Move[++j]!= 0);
			m_EndPos = pos;
			FillStatusBar1();

			return true;
		}

		public void SaveBoard()
		{
			for(int i = 0; i < 55; i++)
				m_OldBoard[i] = m_Game.GBoard[i];
		}

		public void CopyLastMove(int[] Board)
		{
			for(int i = 0; i < Game.MAX_MOVE_LENGTH; i++)
				m_LastMove.Add(Board[i]);
		}

		private void SwipeSidesButton_Click(object sender, System.EventArgs e)
		{
			m_SideChanged = !m_SideChanged;
			for(int i = 0; i < CheckersPieces.Count; i++)
			{
				if(GetPiece(i).GetCurrentType() == ( int )CheckersPiece.Types.BMAN)
					GetPiece(i).MorpheTo(( int )CheckersPiece.Types.WMAN);
				else 
					GetPiece(i).MorpheTo(( int )CheckersPiece.Types.BMAN);
				GetPiece(i).m_spot = BOARD_SIZE -1 -GetPiece(i).m_spot;
			}
			Invalidate(); // refresh form

		}

		public void Reset()
		{
			if(m_blackwhite)
				m_Game.Turn = TPlayer.BLACK ;
			else
				m_Game.Turn = TPlayer.WHITE;
			
			
			CheckersPieces.Clear();
			WResultPieces.Clear();
			BResultPieces.Clear();
			ReSetFigures();
			ResetBoard();
			Invalidate(); // refresh form
			SwipeSidesButton.Enabled = true;
			BlackRadioButton.Enabled = true;
			WhiteRadioButton.Enabled = true;
			CompRadioButton.Enabled = true;
			YouRadioButton.Enabled = true;
			DammaCheckBox.Enabled = true;
			YouRadioButton.Checked = true;
		}

		public void ReSetFigures()
		{
			int y  = 0;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0 && i > 0)
					++y;
				if(y < 3)
				{
					if(((i+y) % 2)!=0)
						SetFigurImage(i, BLANK_FIELD);
					else
						SetFigurImage(i, WHITE_FIELD);
				}
				else if(y < 5)
					SetFigurImage(i, BLANK_FIELD);
				else
				{
					if(((i+y) % 2)!= 0)
						SetFigurImage(i, BLANK_FIELD);
					else
						SetFigurImage(i, BLACK_FIELD);
				}
			}
			MapImageToBoard();
		}

		public void SetFigurImage(int pos, int image)
		{
			m_ImageBoard[pos] = image;
		}

		public int GetFigurImage(int pos)
		{
			return m_ImageBoard[pos];
		}
		public int GetViewSelection(int pos0, int pos)
		{
			MoveList MveList = new MoveList();
			int firstpos = GetBoardElement(pos0);
			int lastpos = GetBoardElement(pos);
			
			m_Game.TMoveGenerator.MakeMoveList(m_Game.GBoard,m_Game.Turn,MveList);

			int selection =  m_Game.TMoveGenerator.Getselection(firstpos, lastpos);
			if(MveList.MustTake)
			{
				if(selection == -1)
				{
					MessageBox.Show("You have more than one piece\nto take!","Checkers",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return -1;
				}
				else if(selection == 0)
					MessageBox.Show("You must take your opponent first!","Checkers",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}

			return selection;
		}

		public bool IsInWSpot(int pos, int pos0)
		{
			int Selection = GetViewSelection(pos0, pos);
			if(Selection > 0)
				return false;
			if(((m_selectedImage < WHITE_FIELD ) && !m_blackwhite) ||
				((m_selectedImage > BKING_FIELD ) && m_blackwhite))
				MessageBox.Show("Not your color!","Checkers",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			
			m_OldImageBoard.CopyTo(m_ImageBoard,0);
			return true;
		}

		private void WhiteRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			OnWhite();
		}

		private void BlackRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			OnBlack();
		}

		public void OnWhite()
		{
			if(!CompRadioButton.Checked)
				m_Game.Turn= TPlayer.WHITE;
			PlayerType[(int)TPlayer.WHITE] = TPlayerType.HUMAN;
			PlayerType[(int)TPlayer.BLACK] = TPlayerType.COMPUTER;
		}
		public void OnBlack()
		{
			if(!CompRadioButton.Checked)
				m_Game.Turn= TPlayer.BLACK;
			PlayerType[(int)TPlayer.BLACK] = TPlayerType.HUMAN;
			PlayerType[(int)TPlayer.WHITE] = TPlayerType.COMPUTER;
		}

		public void EatCon(int pos, int pos0, int OldImage)
		{
			MoveList MvList = new MoveList();
			int cpos = 0;
			int[] Move = new int[Game.MAX_MOVE_LENGTH];
			
			m_Game.TMoveGenerator.MakeMoveList(m_Game.GBoard,m_Game.Turn,MvList);
			// get best move
			int Selection = GetViewSelection(pos0, pos);
			if(Selection >= 1)
			{
				int i = 0;
				do
				{
					Move[i]=MvList.List[Selection-1,i];
				}while(MvList.List[Selection-1,++i]!=0);
				Move[i]=0;
				//TSearcher.CopyMove(ref Move, MoveList.List[Selection-1]);
				i = 0;
				do
				{
					cpos = GetImageElement(Move[i]);
					if((OldImage  > BKING_FIELD &&  (GetFigurImage(cpos) == BLACK_FIELD
						|| GetFigurImage(cpos) == BKING_FIELD)) ||
						((OldImage < WHITE_FIELD) && (GetFigurImage(cpos) == WHITE_FIELD 
						||GetFigurImage(cpos) == WKING_FIELD)))
					{
						// this is a white piece
							
						// the next piece is black, replace it with blank
						SetFigurImage(cpos, BLANK_FIELD);
						int count = CheckersPieces.Count;
						int j = 0;
						for(j = 0; j < count; j++)
						{
							int temp = GetPiece(j).m_spot;
							if(m_SideChanged)
								temp = BOARD_SIZE -1 - temp;
							if(temp == cpos)
							{
								if(( int )GetPiece(j).GetCurrentType() >Game.BKING)
								{
									int cnt =  WResultPieces.Count;
									CheckersPiece piece = new CheckersPiece( ( int )GetPiece(j).GetCurrentType(), 
										cnt%5 * 35, cnt/5 * 46 , 0,
										RWhitePieces,35,46 );
									WResultPieces.Add(piece);
											
								}
								else
								{
									int cnt =  BResultPieces.Count + 15;
									CheckersPiece piece = new CheckersPiece( ( int )GetPiece(j).GetCurrentType(), 
										cnt%5 * 35, cnt/5 * 46 , 0,
										RWhitePieces,35,46 );
									BResultPieces.Add(piece);
								}
								FillStatusBar1();
										
								ResultBox.Invalidate();
								CheckersPieces.RemoveAt( j );
								pieceBox.Invalidate();
								Update();
								break;
							}
						}
					}
				}while(Move[++i]!=0);
			}

		}

		public void TranslateMove()
		{
			DoTheDrag();
			MapBoardToImage(m_Game.GBoard,false);
			m_PosList.Clear();
		}

		public void FillPointList()
		{
			m_PointList.Clear();
			for(int j = 0; j < m_PosList.Count -1; j++)
			{
				
				Point startpoint= new Point(0,0);
				Point endpoint = new Point(0,0);
				SetPoint(ref startpoint, (int)m_PosList[j]);
				SetPoint(ref endpoint, (int)m_PosList[j + 1]);
				if(j == 0)
					selectedIndex = CheckBounds(startpoint, -1);
				int start = startpoint.Y, end = endpoint.Y;
				float slope = 1;
				int intercept = 0;
				int factor = 0;
				if(endpoint.X == startpoint.X)
					factor =1;
				else
				{
					slope =  (endpoint.Y - startpoint.Y)/(endpoint.X - startpoint.X);
					intercept = (int)(endpoint.Y - slope* endpoint.X);
				}
				if(startpoint.Y > endpoint.Y)
				{
					for(int i = startpoint.Y ; i > endpoint.Y ; i--)
					{ 
						int x =  (int)((1-factor)*(i - intercept)/slope + factor*(startpoint.X));
						Point pnt = new Point(x,i);
						m_PointList.Add(pnt);
					}
				}
				else
				{
					for(int i = startpoint.Y ; i < endpoint.Y ; i++)
					{ 
						int x =  (int)((1-factor)*(i - intercept)/slope + factor*(startpoint.X));
						Point pnt = new Point(x,i);
						m_PointList.Add(pnt);
					}
				}
			}
			
		}

		public void SetPoint(ref Point point, int pos)
		{
			point.Offset( (TILESIZE * (pos%8) + TILESIZE/2),(TILESIZE * (pos/8))+ TILESIZE/2-10) ;
		}

		public void DoTheDrag()
		{
			FillPointList();
			for(int i =0; i < m_PointList.Count; i++)
			{
				DragOn((Point)m_PointList[i]);
				Thread.Sleep(10);
				Update();
			}
			selectedIndex =-1;
		}

		public void DragOn(Point pnt)
		{
			if(selectedIndex > -1)
			{
				Rectangle region = new Rectangle(
					pnt.X - TILESIZE *2, pnt.Y - TILESIZE * 2,
					TILESIZE * 4, TILESIZE *4);
				GetPiece(selectedIndex).SetLocation(
					pnt.X - TILESIZE/2,pnt.Y - TILESIZE/2, true);
				int selectedField = ((pnt.Y / TILESIZE) * 8) + (pnt.X / TILESIZE);
				if(m_SideChanged)
					selectedField = BOARD_SIZE -1 - selectedField;

				GetPiece(selectedIndex).m_spot = selectedField;
				pieceBox.Invalidate(region);
				
			}
		}

		public void ResetPieces()
		{
			int x = pieceBox.Bounds.Left;
			int  y = pieceBox.Bounds.Top;
			for(int i =0; i < BOARD_SIZE; i++)
			{
				int X = x + (i % 8)*TILESIZE;
				int Y = y + i/8 * TILESIZE;
				int count = CheckersPieces.Count;
				int j = 0;
				for(j = 0; j < count; j++)
				{
					int temp = GetPiece(j).m_spot;
					if(m_ImageBoard[i] == BLANK_FIELD)
					{
						if(temp == i)
						{
							if(( int )GetPiece(j).GetCurrentType() >Game.BKING)
							{
								int cnt =  WResultPieces.Count;
								CheckersPiece piece = new CheckersPiece( ( int )GetPiece(j).GetCurrentType(), 
									cnt%5 * 35, cnt/5 * 46 , 0,
									RWhitePieces,35,46 );
								WResultPieces.Add(piece);
											
							}
							else
							{
								int cnt =  BResultPieces.Count + 15;
								CheckersPiece piece = new CheckersPiece( ( int )GetPiece(j).GetCurrentType(), 
									cnt%5 * 35, cnt/5 * 46 , 0,
									RWhitePieces,35,46 );
								BResultPieces.Add(piece);
							}
										
							ResultBox.Invalidate();
							CheckersPieces.RemoveAt(j);
							Rectangle region = new Rectangle(
								X - TILESIZE *2, Y - TILESIZE * 2,
								TILESIZE * 4, TILESIZE *4);
							pieceBox.Invalidate(region);
							break;
						}
						FillStatusBar1();
					}
				}
			}
		}
		private void StartOverButton_Click(object sender, System.EventArgs e)
		{
			Reset();
		}

		private void ResultBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			for(int i = 0; i < WResultPieces.Count; i++)
				GetWResultPiece(i).Draw(e.Graphics);
			for(int i = 0; i < BResultPieces.Count; i++)
				GetBResultPiece(i).Draw(e.Graphics);
		}

		public string GetExeDir()
		{
			
			Process p = Process.GetCurrentProcess();
			string str = p.MainModule.FileName;
			int end = str.Length;
			int k = str.LastIndexOf('\\');
			string result = str.Substring(0,k+1);
			return result;
		}


		
		public void LoadBoard()
		{
			MapBoardToImage(m_Game.GBoard,false);
			ResetBoard(); // initialize board
			Invalidate(); 
		}

		public void FillStatusBar1()
		{
			statusBarPanel1.Text = String.Format("Black Player {0}, White Player {1}",
				12 - BResultPieces.Count,12 - WResultPieces.Count);
		}

		

		

		

		

		

		private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//Board f = ActiveMdiChild as Board;
			//if( f != null)
		{
			Graphics g = e.Graphics;
			Rectangle rect = e.MarginBounds;
			int x = rect.Left + rect.Width/2 - TILESIZE *4;
			int  y = rect.Top + rect.Height/2 - TILESIZE *4;;
			int xtilesize = rect.Width/8;
				
			int z = 0;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0 && i > 0)
					++z;
				// draw image specified in board array
				g.DrawImage(
					(Image)checkersTile[ board[ i ] ],
					new Point( TILESIZE * (i%8) + x,
					TILESIZE * z + y) );
			}
			for(int i = 0; i < CheckersPieces.Count; i++)
				GetPiece(i).PrevDraw(e.Graphics,x,y);
		}
		}

		

		private void pieceBox_Paint_1(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics graphicsObject = e.Graphics;
			int x = 0;//pieceBox.Bounds.Left;
			int  y = 0;//pieceBox.Bounds.Top;
			int z = 0;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0 && i > 0)
					++z;
				// draw image specified in board array
				graphicsObject.DrawImage(
					(Image)checkersTile[ board[ i ] ],
					new Point( TILESIZE * (i%8) + x,
					TILESIZE * z + y) );
			}
			for(int i = 0; i < CheckersPieces.Count; i++)
				GetPiece(i).Draw(e.Graphics);
		}

	
		protected override void OnLoad(EventArgs e)
		{
			Assembly asbly =  Assembly.GetExecutingAssembly();
			Stream stream = asbly.GetManifestResourceStream("Checkers.WTile.png");
			checkersTile.Add( new Bitmap(stream) );
			stream = asbly.GetManifestResourceStream("Checkers.BTile.png");
			checkersTile.Add( new Bitmap(stream) );

			//	base.OnLoad(e);
			ResetBoard(); // initialize board
			Invalidate(); // refresh form
			base.OnLoad (e);
		}

		private void statusBar1_PanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e)
		{
		
		}

		public void Print(System.Drawing.Printing.PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle rect = e.MarginBounds;
			int x = rect.Left + rect.Width/2 - TILESIZE *4;
			int  y = rect.Top + rect.Height/2 - TILESIZE *4;;
			int xtilesize = rect.Width/8;
				
			int z = 0;
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if( (i % 8)==0 && i > 0)
					++z;
				// draw image specified in board array
				g.DrawImage(
					(Image)checkersTile[ board[ i ] ],
					new Point( TILESIZE * (i%8) + x,
					TILESIZE * z + y) );
			}
			for(int i = 0; i < CheckersPieces.Count; i++)
				GetPiece(i).PrevDraw(e.Graphics,x,y);
		}

		public bool OpenFile()
		{
			bool result = false;
			OpenFileDialog fileChooser = new OpenFileDialog();
			fileChooser.AddExtension = true;

			fileChooser.DefaultExt = "dam";
			fileChooser.Filter = "Damma files (*.dam)|*.dam";
			fileChooser.CheckFileExists = false;
			if(fileChooser.ShowDialog() == DialogResult.Cancel)
				return false;
			string filename =  fileChooser.FileName;
			if(filename == "" || filename == null)
			{
				MessageBox.Show("Invalid File Name","Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = false;
			}
			try 
			{
				//formatter.Serialize(Output,m_Game.GBoard);
				Output =  new FileStream(filename,
					FileMode.Open, FileAccess.Read);
				m_LastMove = (ArrayList)formatter.Deserialize(Output);
				GB = (ArrayList)formatter.Deserialize(Output);
				for(int i = 0; i < 55; i++)
					m_Game.GBoard[i] = (int)(GB[i]);
				WResultPieces = (ArrayList)formatter.Deserialize(Output);
				BResultPieces = (ArrayList)formatter.Deserialize(Output);
				LoadBoard();
				result = true;
			}
			catch (SerializationException k) 
			{
				MessageBox.Show("Failed to serialize. Reason: " + k.Message, "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = false;
				throw;
			}
			finally 
			{
				Output.Close();
				fileChooser.Dispose();
			}
			return result;
	
		}

		public void SaveFile()
		{
			
			SaveFileDialog fileChooser = new SaveFileDialog();
			fileChooser.AddExtension = true;

			fileChooser.DefaultExt = "dam";
			fileChooser.Filter = "Damma files (*.dam)|*.dam";
			fileChooser.CheckFileExists = false;
			if(fileChooser.ShowDialog() == DialogResult.Cancel)
				return ;
			string filename =  fileChooser.FileName;
			if(filename == "" || filename == null)
			{
				MessageBox.Show("Invalid File Name","Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				
			}
			else
			{
				try 
				{
					Output =  new FileStream(filename,
						FileMode.OpenOrCreate, FileAccess.Write);
					formatter.Serialize(Output, m_LastMove);
					for(int i = 0; i < 55; i++)
						GB.Add(m_Game.GBoard[i]);
					formatter.Serialize(Output,GB);
					formatter.Serialize(Output,WResultPieces);
					formatter.Serialize(Output,BResultPieces);
					
				}
				catch (SerializationException k) 
				{
					MessageBox.Show("Failed to serialize. Reason: " + k.Message, "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					
					throw;
				}
				finally 
				{
					Output.Close();
					fileChooser.Dispose();
				}
			}
			
		}
	}

}
