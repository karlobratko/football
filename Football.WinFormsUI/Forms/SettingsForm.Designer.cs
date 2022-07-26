namespace Football.WinFormsUI.Forms {
  partial class SettingsForm {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
      this.lblLanguage = new System.Windows.Forms.Label();
      this.lblGender = new System.Windows.Forms.Label();
      this.ddlLanguage = new System.Windows.Forms.ComboBox();
      this.rbMale = new System.Windows.Forms.RadioButton();
      this.rbFemale = new System.Windows.Forms.RadioButton();
      this.btnSubmit = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblLanguage
      // 
      resources.ApplyResources(this.lblLanguage, "lblLanguage");
      this.lblLanguage.Name = "lblLanguage";
      // 
      // lblGender
      // 
      resources.ApplyResources(this.lblGender, "lblGender");
      this.lblGender.Name = "lblGender";
      // 
      // ddlLanguage
      // 
      this.ddlLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      resources.ApplyResources(this.ddlLanguage, "ddlLanguage");
      this.ddlLanguage.FormattingEnabled = true;
      this.ddlLanguage.Name = "ddlLanguage";
      // 
      // rbMale
      // 
      resources.ApplyResources(this.rbMale, "rbMale");
      this.rbMale.Name = "rbMale";
      this.rbMale.TabStop = true;
      this.rbMale.UseVisualStyleBackColor = true;
      // 
      // rbFemale
      // 
      resources.ApplyResources(this.rbFemale, "rbFemale");
      this.rbFemale.Name = "rbFemale";
      this.rbFemale.TabStop = true;
      this.rbFemale.UseVisualStyleBackColor = true;
      // 
      // btnSubmit
      // 
      resources.ApplyResources(this.btnSubmit, "btnSubmit");
      this.btnSubmit.Name = "btnSubmit";
      this.btnSubmit.UseVisualStyleBackColor = true;
      this.btnSubmit.Click += new System.EventHandler(this.SubmitSettings);
      // 
      // btnCancel
      // 
      resources.ApplyResources(this.btnCancel, "btnCancel");
      this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.CancelSettings);
      // 
      // SettingsForm
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSubmit);
      this.Controls.Add(this.rbFemale);
      this.Controls.Add(this.rbMale);
      this.Controls.Add(this.ddlLanguage);
      this.Controls.Add(this.lblGender);
      this.Controls.Add(this.lblLanguage);
      this.KeyPreview = true;
      this.Name = "SettingsForm";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserPressedKey);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblLanguage;
    private System.Windows.Forms.Label lblGender;
    private System.Windows.Forms.ComboBox ddlLanguage;
    private System.Windows.Forms.RadioButton rbMale;
    private System.Windows.Forms.RadioButton rbFemale;
    private System.Windows.Forms.Button btnSubmit;
    private System.Windows.Forms.Button btnCancel;
  }
}