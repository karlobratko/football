namespace Football.WinFormsUI.Controls {
  partial class PlayerControl {
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControl));
      this.pbPlayer = new System.Windows.Forms.PictureBox();
      this.lblShirtNumber = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.lblPosition = new System.Windows.Forms.Label();
      this.lblCaptain = new System.Windows.Forms.Label();
      this.pbFavourite = new System.Windows.Forms.PictureBox();
      this.cmsPlayerControl = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.changeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tsmiRemoveImage = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbFavourite)).BeginInit();
      this.cmsPlayerControl.SuspendLayout();
      this.SuspendLayout();
      // 
      // pbPlayer
      // 
      this.pbPlayer.Image = global::Football.WinFormsUI.Properties.Resources.user;
      this.pbPlayer.InitialImage = global::Football.WinFormsUI.Properties.Resources.user;
      resources.ApplyResources(this.pbPlayer, "pbPlayer");
      this.pbPlayer.Name = "pbPlayer";
      this.pbPlayer.TabStop = false;
      // 
      // lblShirtNumber
      // 
      resources.ApplyResources(this.lblShirtNumber, "lblShirtNumber");
      this.lblShirtNumber.Name = "lblShirtNumber";
      // 
      // lblName
      // 
      resources.ApplyResources(this.lblName, "lblName");
      this.lblName.Name = "lblName";
      // 
      // lblPosition
      // 
      resources.ApplyResources(this.lblPosition, "lblPosition");
      this.lblPosition.Name = "lblPosition";
      // 
      // lblCaptain
      // 
      resources.ApplyResources(this.lblCaptain, "lblCaptain");
      this.lblCaptain.Name = "lblCaptain";
      // 
      // pbFavourite
      // 
      this.pbFavourite.Image = global::Football.WinFormsUI.Properties.Resources.star;
      this.pbFavourite.InitialImage = global::Football.WinFormsUI.Properties.Resources.star;
      resources.ApplyResources(this.pbFavourite, "pbFavourite");
      this.pbFavourite.Name = "pbFavourite";
      this.pbFavourite.TabStop = false;
      // 
      // cmsPlayerControl
      // 
      this.cmsPlayerControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeImageToolStripMenuItem,
            this.tsmiRemoveImage});
      this.cmsPlayerControl.Name = "cmsPlayerControl";
      resources.ApplyResources(this.cmsPlayerControl, "cmsPlayerControl");
      // 
      // changeImageToolStripMenuItem
      // 
      this.changeImageToolStripMenuItem.Name = "changeImageToolStripMenuItem";
      resources.ApplyResources(this.changeImageToolStripMenuItem, "changeImageToolStripMenuItem");
      this.changeImageToolStripMenuItem.Click += new System.EventHandler(this.ChangeImage);
      // 
      // tsmiRemoveImage
      // 
      resources.ApplyResources(this.tsmiRemoveImage, "tsmiRemoveImage");
      this.tsmiRemoveImage.Name = "tsmiRemoveImage";
      this.tsmiRemoveImage.Click += new System.EventHandler(this.RemoveImage);
      // 
      // PlayerControl
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.ContextMenuStrip = this.cmsPlayerControl;
      this.Controls.Add(this.pbFavourite);
      this.Controls.Add(this.lblCaptain);
      this.Controls.Add(this.lblPosition);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.lblShirtNumber);
      this.Controls.Add(this.pbPlayer);
      this.Name = "PlayerControl";
      this.Load += new System.EventHandler(this.PlayerControl_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbFavourite)).EndInit();
      this.cmsPlayerControl.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbPlayer;
    private System.Windows.Forms.Label lblShirtNumber;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblPosition;
    private System.Windows.Forms.Label lblCaptain;
    private System.Windows.Forms.PictureBox pbFavourite;
    private System.Windows.Forms.ContextMenuStrip cmsPlayerControl;
    private System.Windows.Forms.ToolStripMenuItem changeImageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tsmiRemoveImage;
  }
}
