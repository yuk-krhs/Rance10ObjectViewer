namespace Rance10ObjectViewer
{
    partial class CheatControl
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
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudTurn = new System.Windows.Forms.NumericUpDown();
            this.bTurn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUltimate = new System.Windows.Forms.CheckBox();
            this.cbTicket = new System.Windows.Forms.CheckBox();
            this.cbTotalExp = new System.Windows.Forms.CheckBox();
            this.cbFriendship = new System.Windows.Forms.CheckBox();
            this.cbMedal = new System.Windows.Forms.CheckBox();
            this.cbIngot = new System.Windows.Forms.CheckBox();
            this.bMaxExpFriend = new System.Windows.Forms.Button();
            this.cbTurn = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTurn)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInfo
            // 
            this.tbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbInfo.Location = new System.Drawing.Point(0, 119);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(821, 399);
            this.tbInfo.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 119);
            this.panel1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTurn);
            this.groupBox2.Controls.Add(this.nudTurn);
            this.groupBox2.Controls.Add(this.bTurn);
            this.groupBox2.Location = new System.Drawing.Point(3, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(629, 48);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "進行情報";
            // 
            // nudTurn
            // 
            this.nudTurn.Location = new System.Drawing.Point(175, 23);
            this.nudTurn.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudTurn.Name = "nudTurn";
            this.nudTurn.Size = new System.Drawing.Size(68, 19);
            this.nudTurn.TabIndex = 6;
            this.nudTurn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bTurn
            // 
            this.bTurn.Location = new System.Drawing.Point(6, 20);
            this.bTurn.Name = "bTurn";
            this.bTurn.Size = new System.Drawing.Size(105, 23);
            this.bTurn.TabIndex = 5;
            this.bTurn.Text = "書き換え";
            this.bTurn.UseVisualStyleBackColor = true;
            this.bTurn.Click += new System.EventHandler(this.bTurn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUltimate);
            this.groupBox1.Controls.Add(this.cbTicket);
            this.groupBox1.Controls.Add(this.cbTotalExp);
            this.groupBox1.Controls.Add(this.cbFriendship);
            this.groupBox1.Controls.Add(this.cbMedal);
            this.groupBox1.Controls.Add(this.cbIngot);
            this.groupBox1.Controls.Add(this.bMaxExpFriend);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "パーティ情報";
            // 
            // cbUltimate
            // 
            this.cbUltimate.AutoSize = true;
            this.cbUltimate.Location = new System.Drawing.Point(510, 24);
            this.cbUltimate.Name = "cbUltimate";
            this.cbUltimate.Size = new System.Drawing.Size(96, 16);
            this.cbUltimate.TabIndex = 12;
            this.cbUltimate.Text = "AT&HP=999999";
            this.cbUltimate.UseVisualStyleBackColor = true;
            // 
            // cbTicket
            // 
            this.cbTicket.AutoSize = true;
            this.cbTicket.Location = new System.Drawing.Point(117, 24);
            this.cbTicket.Name = "cbTicket";
            this.cbTicket.Size = new System.Drawing.Size(60, 16);
            this.cbTicket.TabIndex = 10;
            this.cbTicket.Text = "食券=3";
            this.cbTicket.UseVisualStyleBackColor = true;
            // 
            // cbTotalExp
            // 
            this.cbTotalExp.AutoSize = true;
            this.cbTotalExp.Location = new System.Drawing.Point(381, 24);
            this.cbTotalExp.Name = "cbTotalExp";
            this.cbTotalExp.Size = new System.Drawing.Size(123, 16);
            this.cbTotalExp.TabIndex = 9;
            this.cbTotalExp.Text = "獲得EXP=99999999";
            this.cbTotalExp.UseVisualStyleBackColor = true;
            // 
            // cbFriendship
            // 
            this.cbFriendship.AutoSize = true;
            this.cbFriendship.Location = new System.Drawing.Point(315, 24);
            this.cbFriendship.Name = "cbFriendship";
            this.cbFriendship.Size = new System.Drawing.Size(60, 16);
            this.cbFriendship.TabIndex = 8;
            this.cbFriendship.Text = "友情=3";
            this.cbFriendship.UseVisualStyleBackColor = true;
            // 
            // cbMedal
            // 
            this.cbMedal.AutoSize = true;
            this.cbMedal.Location = new System.Drawing.Point(183, 24);
            this.cbMedal.Name = "cbMedal";
            this.cbMedal.Size = new System.Drawing.Size(60, 16);
            this.cbMedal.TabIndex = 7;
            this.cbMedal.Text = "勲章=2";
            this.cbMedal.UseVisualStyleBackColor = true;
            // 
            // cbIngot
            // 
            this.cbIngot.AutoSize = true;
            this.cbIngot.Location = new System.Drawing.Point(249, 24);
            this.cbIngot.Name = "cbIngot";
            this.cbIngot.Size = new System.Drawing.Size(60, 16);
            this.cbIngot.TabIndex = 6;
            this.cbIngot.Text = "金塊=3";
            this.cbIngot.UseVisualStyleBackColor = true;
            // 
            // bMaxExpFriend
            // 
            this.bMaxExpFriend.Location = new System.Drawing.Point(6, 20);
            this.bMaxExpFriend.Name = "bMaxExpFriend";
            this.bMaxExpFriend.Size = new System.Drawing.Size(105, 23);
            this.bMaxExpFriend.TabIndex = 5;
            this.bMaxExpFriend.Text = "書き換え";
            this.bMaxExpFriend.UseVisualStyleBackColor = true;
            this.bMaxExpFriend.Click += new System.EventHandler(this.bMaxExpFriend_Click);
            // 
            // cbTurn
            // 
            this.cbTurn.AutoSize = true;
            this.cbTurn.Checked = true;
            this.cbTurn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTurn.Location = new System.Drawing.Point(117, 24);
            this.cbTurn.Name = "cbTurn";
            this.cbTurn.Size = new System.Drawing.Size(53, 16);
            this.cbTurn.TabIndex = 7;
            this.cbTurn.Text = "ターン:";
            this.cbTurn.UseVisualStyleBackColor = true;
            // 
            // CheatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.panel1);
            this.Name = "CheatControl";
            this.Size = new System.Drawing.Size(821, 518);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTurn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bMaxExpFriend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbTotalExp;
        private System.Windows.Forms.CheckBox cbFriendship;
        private System.Windows.Forms.CheckBox cbMedal;
        private System.Windows.Forms.CheckBox cbIngot;
        private System.Windows.Forms.CheckBox cbTicket;
        private System.Windows.Forms.CheckBox cbUltimate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudTurn;
        private System.Windows.Forms.Button bTurn;
        private System.Windows.Forms.CheckBox cbTurn;
    }
}
