namespace Rance10ObjectViewer
{
    partial class Rance10AnalyzeView
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rance10AnalyzeView));
            this.tvObject = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefreshObjectTree = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvObject
            // 
            this.tvObject.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvObject.Location = new System.Drawing.Point(0, 25);
            this.tvObject.Name = "tvObject";
            this.tvObject.Size = new System.Drawing.Size(395, 510);
            this.tvObject.TabIndex = 0;
            this.tvObject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObject_AfterSelect);
            this.tvObject.DoubleClick += new System.EventHandler(this.tvObject_DoubleClick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(395, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 510);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // tbInfo
            // 
            this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInfo.Location = new System.Drawing.Point(3, 3);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(531, 504);
            this.tbInfo.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(398, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(537, 510);
            this.panel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefreshObjectTree});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(935, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRefreshObjectTree
            // 
            this.tsbRefreshObjectTree.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshObjectTree.Image")));
            this.tsbRefreshObjectTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshObjectTree.Name = "tsbRefreshObjectTree";
            this.tsbRefreshObjectTree.Size = new System.Drawing.Size(86, 22);
            this.tsbRefreshObjectTree.Text = "ツリーの更新";
            this.tsbRefreshObjectTree.Click += new System.EventHandler(this.tsbRefreshObjectTree_Click);
            // 
            // Rance10AnalyzeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tvObject);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Rance10AnalyzeView";
            this.Size = new System.Drawing.Size(935, 535);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvObject;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefreshObjectTree;
    }
}
