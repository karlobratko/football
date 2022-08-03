namespace Football.WinFormsUI.Controls {
  partial class RankedPlayerControl {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankedPlayerControl));
      this.pbPlayer = new System.Windows.Forms.PictureBox();
      this.lblGoalsDesc = new System.Windows.Forms.Label();
      this.lblPosition = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.lblShirtNumber = new System.Windows.Forms.Label();
      this.lblYellowCardsDesc = new System.Windows.Forms.Label();
      this.lblGoals = new System.Windows.Forms.Label();
      this.lblYellowCards = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).BeginInit();
      this.SuspendLayout();
      // 
      // pbPlayer
      // 
      resources.ApplyResources(this.pbPlayer, "pbPlayer");
      this.pbPlayer.Image = global::Football.WinFormsUI.Properties.Resources.user;
      this.pbPlayer.InitialImage = global::Football.WinFormsUI.Properties.Resources.user;
      this.pbPlayer.Name = "pbPlayer";
      this.pbPlayer.TabStop = false;
      // 
      // lblGoalsDesc
      // 
      resources.ApplyResources(this.lblGoalsDesc, "lblGoalsDesc");
      this.lblGoalsDesc.Name = "lblGoalsDesc";
      // 
      // lblPosition
      // 
      resources.ApplyResources(this.lblPosition, "lblPosition");
      this.lblPosition.Name = "lblPosition";
      // 
      // lblName
      // 
      resources.ApplyResources(this.lblName, "lblName");
      this.lblName.Name = "lblName";
      // 
      // lblShirtNumber
      // 
      resources.ApplyResources(this.lblShirtNumber, "lblShirtNumber");
      this.lblShirtNumber.Name = "lblShirtNumber";
      // 
      // lblYellowCardsDesc
      // 
      resources.ApplyResources(this.lblYellowCardsDesc, "lblYellowCardsDesc");
      this.lblYellowCardsDesc.Name = "lblYellowCardsDesc";
      // 
      // lblGoals
      // 
      resources.ApplyResources(this.lblGoals, "lblGoals");
      this.lblGoals.Name = "lblGoals";
      // 
      // lblYellowCards
      // 
      resources.ApplyResources(this.lblYellowCards, "lblYellowCards");
      this.lblYellowCards.Name = "lblYellowCards";
      // 
      // RankedPlayerControl
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.lblYellowCards);
      this.Controls.Add(this.lblGoals);
      this.Controls.Add(this.lblYellowCardsDesc);
      this.Controls.Add(this.lblGoalsDesc);
      this.Controls.Add(this.lblPosition);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.lblShirtNumber);
      this.Controls.Add(this.pbPlayer);
      this.Name = "RankedPlayerControl";
      ((System.ComponentModel.ISupportInitialize)(this.pbPlayer)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbPlayer;
    private System.Windows.Forms.Label lblGoalsDesc;
    private System.Windows.Forms.Label lblPosition;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblShirtNumber;
    private System.Windows.Forms.Label lblYellowCardsDesc;
    private System.Windows.Forms.Label lblGoals;
    private System.Windows.Forms.Label lblYellowCards;
  }
}
