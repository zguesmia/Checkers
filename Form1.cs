using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Checkers
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Damma : System.Windows.Forms.Form
	{
		private Checkers.Board board1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Damma()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Damma));
			this.board1 = new Checkers.Board();
			this.SuspendLayout();
			// 
			// board1
			// 
			this.board1.BackColor = System.Drawing.Color.Sienna;
			this.board1.Location = new System.Drawing.Point(0, -8);
			this.board1.Name = "board1";
			this.board1.Size = new System.Drawing.Size(608, 472);
			this.board1.TabIndex = 0;
			// 
			// Damma
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 453);
			this.Controls.Add(this.board1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Damma";
			this.Text = "Damma";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ParentForm());
		}

		public void PrintDamma(System.Drawing.Printing.PrintPageEventArgs e)
		{
			board1.Print(e);
		}

		public bool Open()
		{
			return board1.OpenFile();
		
		}

		public void Save()
		{
			board1.SaveFile();
		}

	}
}
