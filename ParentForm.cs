using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Checkers
{
	/// <summary>
	/// Summary description for ParentForm.
	/// </summary>
	public class ParentForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.ImageList imageListToolbar;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton tbbNew;
		private System.Windows.Forms.ToolBarButton tbbOpen;
		private System.Windows.Forms.ToolBarButton tbbSave;
		private System.Windows.Forms.ToolBarButton tbbPrint;
		private System.Windows.Forms.ToolBarButton tbbPreview;
		private System.ComponentModel.IContainer components;

		public ParentForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitToolbarButtons();


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
				if(components != null)
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ParentForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.imageListToolbar = new System.Windows.Forms.ImageList(this.components);
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.tbbNew = new System.Windows.Forms.ToolBarButton();
			this.tbbOpen = new System.Windows.Forms.ToolBarButton();
			this.tbbSave = new System.Windows.Forms.ToolBarButton();
			this.tbbPrint = new System.Windows.Forms.ToolBarButton();
			this.tbbPreview = new System.Windows.Forms.ToolBarButton();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem5,
																					  this.menuItem7,
																					  this.menuItem6,
																					  this.menuItem8,
																					  this.menuItem10,
																					  this.menuItem9,
																					  this.menuItem4});
			this.menuItem1.Text = "&File";
			this.menuItem1.Popup += new System.EventHandler(this.menuItem1_Popup);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&New";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "&Print";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 3;
			this.menuItem7.Text = "&Print Preview";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "-";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "Open";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 6;
			this.menuItem10.Text = "&Save";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 7;
			this.menuItem9.Text = "-";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 8;
			this.menuItem4.Text = "&Exit";
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// imageListToolbar
			// 
			this.imageListToolbar.ImageSize = new System.Drawing.Size(16, 16);
			this.imageListToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolbar.ImageStream")));
			this.imageListToolbar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.tbbNew,
																						this.tbbOpen,
																						this.tbbSave,
																						this.tbbPrint,
																						this.tbbPreview});
			this.toolBar1.ButtonSize = new System.Drawing.Size(16, 16);
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageListToolbar;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(736, 28);
			this.toolBar1.TabIndex = 1;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// tbbNew
			// 
			this.tbbNew.ImageIndex = 0;
			this.tbbNew.ToolTipText = "New Game";
			// 
			// tbbOpen
			// 
			this.tbbOpen.ImageIndex = 1;
			this.tbbOpen.ToolTipText = "Open Existing Game";
			// 
			// tbbSave
			// 
			this.tbbSave.Enabled = false;
			this.tbbSave.ImageIndex = 2;
			this.tbbSave.ToolTipText = "Save Game";
			// 
			// tbbPrint
			// 
			this.tbbPrint.Enabled = false;
			this.tbbPrint.ImageIndex = 4;
			this.tbbPrint.ToolTipText = "Print Board";
			// 
			// tbbPreview
			// 
			this.tbbPreview.Enabled = false;
			this.tbbPreview.ImageIndex = 3;
			this.tbbPreview.ToolTipText = "Preview Print";
			// 
			// ParentForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 561);
			this.Controls.Add(this.toolBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "ParentForm";
			this.Text = "Damma";
			this.MdiChildActivate += new System.EventHandler(this.ParentForm_MdiChildActivate);
			this.ResumeLayout(false);

		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ParentForm());
		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Damma dame =  new Damma();
			dame.MdiParent = this;
			dame.Show();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			PrintDialog dlg = new PrintDialog();
			dlg.Document = printDocument1;
			if(dlg.ShowDialog()== DialogResult.OK)
				printDocument1.Print();
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Damma f = ActiveMdiChild as Damma;
			if( f != null)
				f.PrintDamma(e);
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			PrintPreviewDialog dlg = 
				new PrintPreviewDialog();
			dlg.Document = printDocument1;
			dlg.ShowDialog();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			Damma dame =  new Damma();
			dame.MdiParent = this;
			if(dame.Open())
				dame.Show();
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			Damma f = ActiveMdiChild as Damma;
			if( f != null)
				f.Save();

		}

		private void menuItem1_Popup(object sender, System.EventArgs e)
		{
			Damma f = ActiveMdiChild as Damma;
			bool state = (f != null);
			this.menuItem5.Enabled = state;
			this.menuItem7.Enabled = state;
			this.menuItem10.Enabled = state;

		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			MenuItem mi = e.Button.Tag as MenuItem;
			if(mi != null)
				mi.PerformClick();

		}

		internal void InitToolbarButtons()
		{
			tbbNew.Tag = menuItem2;
			tbbOpen.Tag = menuItem8;
			tbbSave.Tag = menuItem10;
			tbbPreview.Tag = menuItem7;
			tbbPrint.Tag = menuItem5;
		}

		private void ParentForm_MdiChildActivate(object sender, System.EventArgs e)
		{
			Damma f = ActiveMdiChild as Damma;
			bool state = (f != null);
			this.tbbSave.Enabled = state ;
			this.tbbPreview.Enabled = state ;
			this.tbbPrint.Enabled = state;
		}
	}
}
