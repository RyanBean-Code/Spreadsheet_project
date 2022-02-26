
namespace Spreadsheet_Ryan_Bean
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Demo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 60;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(800, 450);
            this.dataGridView1.TabIndex = 0;
            // 
            // Demo
            // 
            this.Demo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Demo.Location = new System.Drawing.Point(0, 421);
            this.Demo.Name = "Demo";
            this.Demo.Size = new System.Drawing.Size(800, 29);
            this.Demo.TabIndex = 1;
            this.Demo.Text = "Demo";
            this.Demo.UseVisualStyleBackColor = true;
            this.Demo.MouseClick += Demo_MouseClick;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Demo);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Spreadsheet Ryan Bean";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Demo for HW1
        /// </summary>
        /// <param name="sender"> The button object. </param>
        /// <param name="e"> I dunno what this is actually set to. </param>
        private void Demo_MouseClick(object sender, MouseEventArgs e)
        {
            System.Random random = new System.Random();
            for (int i = 0; i < 50; i++)
            {
                int col = random.Next(0, 25);
                int row = random.Next(0, 49);
                this.spreadsheet.GetCell(col, row).Text = "Hello World!";
            }
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(1, i).Text = "This is cell B" + (i + 1).ToString();
            }
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(0, i).Text = "=B" + (i + 1).ToString();
            }
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Demo;
        //private System.Windows.Forms.DataGridViewColumn[] columnArray;
    }
}