namespace Rance10ObjectViewer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Rance10AnalyzeView1 = new Rance10ObjectViewer.Rance10AnalyzeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.partyControl1 = new Rance10ObjectViewer.CheatControl();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1264, 961);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Rance10AnalyzeView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1256, 935);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Object Tree";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Rance10AnalyzeView1
            // 
            this.Rance10AnalyzeView1.Analyzer = null;
            this.Rance10AnalyzeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rance10AnalyzeView1.Location = new System.Drawing.Point(3, 3);
            this.Rance10AnalyzeView1.Name = "Rance10AnalyzeView1";
            this.Rance10AnalyzeView1.Size = new System.Drawing.Size(1250, 929);
            this.Rance10AnalyzeView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.partyControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1256, 935);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cheat";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // partyControl1
            // 
            this.partyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partyControl1.Location = new System.Drawing.Point(3, 3);
            this.partyControl1.Name = "partyControl1";
            this.partyControl1.Size = new System.Drawing.Size(1250, 929);
            this.partyControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 961);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Rance10 Object Viewer";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private Rance10AnalyzeView Rance10AnalyzeView1;
        private System.Windows.Forms.TabPage tabPage3;
        private CheatControl partyControl1;
    }
}

