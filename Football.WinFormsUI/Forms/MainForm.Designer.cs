namespace Football.WinFormsUI {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.pnlFavPlayers = new System.Windows.Forms.FlowLayoutPanel();
      this.lblFavourtePlayers = new System.Windows.Forms.Label();
      this.pnlPlayers = new System.Windows.Forms.FlowLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.pnlRankedMatches = new System.Windows.Forms.FlowLayoutPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.pnlRankedPlayers = new System.Windows.Forms.FlowLayoutPanel();
      this.lblChooseGender = new System.Windows.Forms.Label();
      this.lblChooseCountry = new System.Windows.Forms.Label();
      this.lblGender = new System.Windows.Forms.Label();
      this.ddlCountry = new System.Windows.Forms.ComboBox();
      this.gbSimpleSettings = new System.Windows.Forms.GroupBox();
      this.bgWorker = new System.ComponentModel.BackgroundWorker();
      this.printDocument = new System.Drawing.Printing.PrintDocument();
      this.printDialog = new System.Windows.Forms.PrintDialog();
      this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.gbSimpleSettings.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings,
            this.tsmiPrint});
      resources.ApplyResources(this.menuStrip1, "menuStrip1");
      this.menuStrip1.Name = "menuStrip1";
      // 
      // tsmiSettings
      // 
      resources.ApplyResources(this.tsmiSettings, "tsmiSettings");
      this.tsmiSettings.Name = "tsmiSettings";
      this.tsmiSettings.Click += new System.EventHandler(this.OpenSettings);
      // 
      // tsmiPrint
      // 
      this.tsmiPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.previewToolStripMenuItem,
            this.printToolStripMenuItem});
      resources.ApplyResources(this.tsmiPrint, "tsmiPrint");
      this.tsmiPrint.Name = "tsmiPrint";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.lblStatus});
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Name = "statusStrip1";
      // 
      // progressBar
      // 
      resources.ApplyResources(this.progressBar, "progressBar");
      this.progressBar.Name = "progressBar";
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      resources.ApplyResources(this.lblStatus, "lblStatus");
      // 
      // pnlFavPlayers
      // 
      this.pnlFavPlayers.AllowDrop = true;
      this.pnlFavPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.pnlFavPlayers, "pnlFavPlayers");
      this.pnlFavPlayers.Name = "pnlFavPlayers";
      this.pnlFavPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragFavPlayerDrop);
      this.pnlFavPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragFavPlayerEnter);
      // 
      // lblFavourtePlayers
      // 
      resources.ApplyResources(this.lblFavourtePlayers, "lblFavourtePlayers");
      this.lblFavourtePlayers.Name = "lblFavourtePlayers";
      // 
      // pnlPlayers
      // 
      this.pnlPlayers.AllowDrop = true;
      resources.ApplyResources(this.pnlPlayers, "pnlPlayers");
      this.pnlPlayers.Name = "pnlPlayers";
      this.pnlPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragPlayerDrop);
      this.pnlPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragPlayerEnter);
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // pnlRankedMatches
      // 
      resources.ApplyResources(this.pnlRankedMatches, "pnlRankedMatches");
      this.pnlRankedMatches.Name = "pnlRankedMatches";
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.Name = "label2";
      // 
      // label3
      // 
      resources.ApplyResources(this.label3, "label3");
      this.label3.Name = "label3";
      // 
      // pnlRankedPlayers
      // 
      resources.ApplyResources(this.pnlRankedPlayers, "pnlRankedPlayers");
      this.pnlRankedPlayers.Name = "pnlRankedPlayers";
      // 
      // lblChooseGender
      // 
      resources.ApplyResources(this.lblChooseGender, "lblChooseGender");
      this.lblChooseGender.Name = "lblChooseGender";
      // 
      // lblChooseCountry
      // 
      resources.ApplyResources(this.lblChooseCountry, "lblChooseCountry");
      this.lblChooseCountry.Name = "lblChooseCountry";
      // 
      // lblGender
      // 
      resources.ApplyResources(this.lblGender, "lblGender");
      this.lblGender.Name = "lblGender";
      // 
      // ddlCountry
      // 
      this.ddlCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      resources.ApplyResources(this.ddlCountry, "ddlCountry");
      this.ddlCountry.FormattingEnabled = true;
      this.ddlCountry.Name = "ddlCountry";
      this.ddlCountry.SelectedIndexChanged += new System.EventHandler(this.CountrySelected);
      // 
      // gbSimpleSettings
      // 
      this.gbSimpleSettings.Controls.Add(this.ddlCountry);
      this.gbSimpleSettings.Controls.Add(this.lblGender);
      this.gbSimpleSettings.Controls.Add(this.lblChooseCountry);
      this.gbSimpleSettings.Controls.Add(this.lblChooseGender);
      resources.ApplyResources(this.gbSimpleSettings, "gbSimpleSettings");
      this.gbSimpleSettings.Name = "gbSimpleSettings";
      this.gbSimpleSettings.TabStop = false;
      // 
      // bgWorker
      // 
      this.bgWorker.WorkerReportsProgress = true;
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
      // 
      // printDocument
      // 
      this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintPlayersAsPage);
      // 
      // printDialog
      // 
      this.printDialog.UseEXDialog = true;
      // 
      // printPreviewDialog
      // 
      resources.ApplyResources(this.printPreviewDialog, "printPreviewDialog");
      this.printPreviewDialog.Document = this.printDocument;
      this.printPreviewDialog.Name = "printPreviewDialog";
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
      this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OpenPrintDialog);
      // 
      // previewToolStripMenuItem
      // 
      this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
      resources.ApplyResources(this.previewToolStripMenuItem, "previewToolStripMenuItem");
      this.previewToolStripMenuItem.Click += new System.EventHandler(this.OpenPrintPreview);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
      this.printToolStripMenuItem.Click += new System.EventHandler(this.PrintDocument);
      // 
      // MainForm
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlRankedPlayers);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.pnlRankedMatches);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pnlPlayers);
      this.Controls.Add(this.lblFavourtePlayers);
      this.Controls.Add(this.pnlFavPlayers);
      this.Controls.Add(this.gbSimpleSettings);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExitApplication);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.gbSimpleSettings.ResumeLayout(false);
      this.gbSimpleSettings.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
    private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripProgressBar progressBar;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.FlowLayoutPanel pnlFavPlayers;
    private System.Windows.Forms.Label lblFavourtePlayers;
    private System.Windows.Forms.FlowLayoutPanel pnlPlayers;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.FlowLayoutPanel pnlRankedMatches;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.FlowLayoutPanel pnlRankedPlayers;
    private System.Windows.Forms.Label lblChooseGender;
    private System.Windows.Forms.Label lblChooseCountry;
    private System.Windows.Forms.Label lblGender;
    private System.Windows.Forms.ComboBox ddlCountry;
    private System.Windows.Forms.GroupBox gbSimpleSettings;
    private System.ComponentModel.BackgroundWorker bgWorker;
    private System.Drawing.Printing.PrintDocument printDocument;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.PrintDialog printDialog;
    private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
  }
}