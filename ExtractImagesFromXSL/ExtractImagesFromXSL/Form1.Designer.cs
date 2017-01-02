namespace ExtractImagesFromXSL
    {
    partial class SampleTool
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
            {
            if ( disposing && (components != null) )
                {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
            {
            this.lblSelectFolderPath = new System.Windows.Forms.Label();
            this.txtDisplayFolderPath = new System.Windows.Forms.TextBox();
            this.browseResultsFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnResultsFolder = new System.Windows.Forms.Button();
            this.btnExtractImages = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSelectFolderPath
            // 
            this.lblSelectFolderPath.AutoSize = true;
            this.lblSelectFolderPath.Location = new System.Drawing.Point(30, 122);
            this.lblSelectFolderPath.Name = "lblSelectFolderPath";
            this.lblSelectFolderPath.Size = new System.Drawing.Size(102, 16);
            this.lblSelectFolderPath.TabIndex = 0;
            this.lblSelectFolderPath.Text = "Select xsl folder";
            this.lblSelectFolderPath.Click += new System.EventHandler(this.lblSelectFolderPath_Click);
            // 
            // txtDisplayFolderPath
            // 
            this.txtDisplayFolderPath.Enabled = false;
            this.txtDisplayFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayFolderPath.Location = new System.Drawing.Point(167, 116);
            this.txtDisplayFolderPath.Name = "txtDisplayFolderPath";
            this.txtDisplayFolderPath.Size = new System.Drawing.Size(553, 26);
            this.txtDisplayFolderPath.TabIndex = 1;
            // 
            // browseResultsFolderDialog
            // 
            this.browseResultsFolderDialog.ShowNewFolderButton = false;
            // 
            // btnResultsFolder
            // 
            this.btnResultsFolder.Location = new System.Drawing.Point(746, 113);
            this.btnResultsFolder.Name = "btnResultsFolder";
            this.btnResultsFolder.Size = new System.Drawing.Size(85, 29);
            this.btnResultsFolder.TabIndex = 2;
            this.btnResultsFolder.Text = "Browse";
            this.btnResultsFolder.UseVisualStyleBackColor = true;
            this.btnResultsFolder.Click += new System.EventHandler(this.btnResultsFolder_Click);
            // 
            // btnExtractImages
            // 
            this.btnExtractImages.Location = new System.Drawing.Point(345, 179);
            this.btnExtractImages.Name = "btnExtractImages";
            this.btnExtractImages.Size = new System.Drawing.Size(112, 35);
            this.btnExtractImages.TabIndex = 3;
            this.btnExtractImages.Text = "Run";
            this.btnExtractImages.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(256, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Extract images from xsl";
            // 
            // SampleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExtractImages);
            this.Controls.Add(this.btnResultsFolder);
            this.Controls.Add(this.txtDisplayFolderPath);
            this.Controls.Add(this.lblSelectFolderPath);
            this.MaximumSize = new System.Drawing.Size(900, 400);
            this.MinimumSize = new System.Drawing.Size(900, 400);
            this.Name = "SampleTool";
            this.ShowIcon = false;
            this.Text = "Extract Images from xsl files";
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.Label lblSelectFolderPath;
        private System.Windows.Forms.TextBox txtDisplayFolderPath;
        private System.Windows.Forms.FolderBrowserDialog browseResultsFolderDialog;
        private System.Windows.Forms.Button btnResultsFolder;
        private System.Windows.Forms.Button btnExtractImages;
        private System.Windows.Forms.Label label1;
        }
    }

