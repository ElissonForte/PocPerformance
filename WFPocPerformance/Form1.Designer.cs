namespace WFPocPerformance
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
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
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_MultiplesQuery = new System.Windows.Forms.Button();
            this.btn_OneQuery = new System.Windows.Forms.Button();
            this.numRowCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRowCount)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-1, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(803, 378);
            this.dataGridView1.TabIndex = 0;
            // 
            // btn_MultiplesQuery
            // 
            this.btn_MultiplesQuery.Location = new System.Drawing.Point(13, 13);
            this.btn_MultiplesQuery.Name = "btn_MultiplesQuery";
            this.btn_MultiplesQuery.Size = new System.Drawing.Size(134, 23);
            this.btn_MultiplesQuery.TabIndex = 1;
            this.btn_MultiplesQuery.Text = "Consulta Repetitiva";
            this.btn_MultiplesQuery.UseVisualStyleBackColor = true;
            this.btn_MultiplesQuery.Click += new System.EventHandler(this.btn_MultiplesQuery_Click);
            // 
            // btn_OneQuery
            // 
            this.btn_OneQuery.Location = new System.Drawing.Point(248, 13);
            this.btn_OneQuery.Name = "btn_OneQuery";
            this.btn_OneQuery.Size = new System.Drawing.Size(134, 23);
            this.btn_OneQuery.TabIndex = 2;
            this.btn_OneQuery.Text = "Consulta Única";
            this.btn_OneQuery.UseVisualStyleBackColor = true;
            this.btn_OneQuery.Click += new System.EventHandler(this.btn_OneQuery_Click);
            // 
            // numRowCount
            // 
            this.numRowCount.Location = new System.Drawing.Point(482, 15);
            this.numRowCount.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numRowCount.Name = "numRowCount";
            this.numRowCount.Size = new System.Drawing.Size(120, 20);
            this.numRowCount.TabIndex = 3;
            this.numRowCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numRowCount);
            this.Controls.Add(this.btn_OneQuery);
            this.Controls.Add(this.btn_MultiplesQuery);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poc Performance";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRowCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_MultiplesQuery;
        private System.Windows.Forms.Button btn_OneQuery;
        private System.Windows.Forms.NumericUpDown numRowCount;
    }
}

