namespace Football.WinFormsUI.Controls {
  partial class RankedMatchControl {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankedMatchControl));
      this.lblHomeTeamDesc = new System.Windows.Forms.Label();
      this.lblAwayTeamDesc = new System.Windows.Forms.Label();
      this.lblHomeTeam = new System.Windows.Forms.Label();
      this.lblAwayTeam = new System.Windows.Forms.Label();
      this.lblLocationDesc = new System.Windows.Forms.Label();
      this.lblAttendanceDesc = new System.Windows.Forms.Label();
      this.lblLocation = new System.Windows.Forms.Label();
      this.lblAttendance = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblHomeTeamDesc
      // 
      resources.ApplyResources(this.lblHomeTeamDesc, "lblHomeTeamDesc");
      this.lblHomeTeamDesc.Name = "lblHomeTeamDesc";
      // 
      // lblAwayTeamDesc
      // 
      resources.ApplyResources(this.lblAwayTeamDesc, "lblAwayTeamDesc");
      this.lblAwayTeamDesc.Name = "lblAwayTeamDesc";
      // 
      // lblHomeTeam
      // 
      resources.ApplyResources(this.lblHomeTeam, "lblHomeTeam");
      this.lblHomeTeam.Name = "lblHomeTeam";
      // 
      // lblAwayTeam
      // 
      resources.ApplyResources(this.lblAwayTeam, "lblAwayTeam");
      this.lblAwayTeam.Name = "lblAwayTeam";
      // 
      // lblLocationDesc
      // 
      resources.ApplyResources(this.lblLocationDesc, "lblLocationDesc");
      this.lblLocationDesc.Name = "lblLocationDesc";
      // 
      // lblAttendanceDesc
      // 
      resources.ApplyResources(this.lblAttendanceDesc, "lblAttendanceDesc");
      this.lblAttendanceDesc.Name = "lblAttendanceDesc";
      // 
      // lblLocation
      // 
      resources.ApplyResources(this.lblLocation, "lblLocation");
      this.lblLocation.Name = "lblLocation";
      // 
      // lblAttendance
      // 
      resources.ApplyResources(this.lblAttendance, "lblAttendance");
      this.lblAttendance.Name = "lblAttendance";
      // 
      // RankedMatchControl
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.lblAttendance);
      this.Controls.Add(this.lblLocation);
      this.Controls.Add(this.lblAttendanceDesc);
      this.Controls.Add(this.lblLocationDesc);
      this.Controls.Add(this.lblAwayTeam);
      this.Controls.Add(this.lblHomeTeam);
      this.Controls.Add(this.lblAwayTeamDesc);
      this.Controls.Add(this.lblHomeTeamDesc);
      this.Name = "RankedMatchControl";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblHomeTeamDesc;
    private System.Windows.Forms.Label lblAwayTeamDesc;
    private System.Windows.Forms.Label lblHomeTeam;
    private System.Windows.Forms.Label lblAwayTeam;
    private System.Windows.Forms.Label lblLocationDesc;
    private System.Windows.Forms.Label lblAttendanceDesc;
    private System.Windows.Forms.Label lblLocation;
    private System.Windows.Forms.Label lblAttendance;
  }
}
