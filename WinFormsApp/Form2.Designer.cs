
namespace WinFormsApp
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.seedTextBox = new System.Windows.Forms.TextBox();
            this.capacityTextBox = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.allItemsListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chosenItemsListBox = new System.Windows.Forms.ListBox();
            this.elementstextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ziarno";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pojemność";
            // 
            // seedTextBox
            // 
            this.seedTextBox.Location = new System.Drawing.Point(137, 43);
            this.seedTextBox.Name = "seedTextBox";
            this.seedTextBox.Size = new System.Drawing.Size(125, 27);
            this.seedTextBox.TabIndex = 2;
            this.seedTextBox.TextChanged += new System.EventHandler(this.seedTextBox_TextChanged);
            // 
            // capacityTextBox
            // 
            this.capacityTextBox.Location = new System.Drawing.Point(137, 89);
            this.capacityTextBox.Name = "capacityTextBox";
            this.capacityTextBox.Size = new System.Drawing.Size(125, 27);
            this.capacityTextBox.TabIndex = 3;
            this.capacityTextBox.Text = "50";
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(450, 494);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(94, 29);
            this.calculateButton.TabIndex = 4;
            this.calculateButton.Text = "Przelicz";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // allItemsListBox
            // 
            this.allItemsListBox.FormattingEnabled = true;
            this.allItemsListBox.ItemHeight = 20;
            this.allItemsListBox.Location = new System.Drawing.Point(25, 161);
            this.allItemsListBox.Name = "allItemsListBox";
            this.allItemsListBox.Size = new System.Drawing.Size(237, 324);
            this.allItemsListBox.TabIndex = 5;
            this.allItemsListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Elementy wygenerowane";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Elementy wybrane";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // chosenItemsListBox
            // 
            this.chosenItemsListBox.FormattingEnabled = true;
            this.chosenItemsListBox.ItemHeight = 20;
            this.chosenItemsListBox.Location = new System.Drawing.Point(320, 161);
            this.chosenItemsListBox.Name = "chosenItemsListBox";
            this.chosenItemsListBox.Size = new System.Drawing.Size(224, 324);
            this.chosenItemsListBox.TabIndex = 7;
            this.chosenItemsListBox.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // elementstextbox
            // 
            this.elementstextbox.Location = new System.Drawing.Point(435, 43);
            this.elementstextbox.Name = "elementstextbox";
            this.elementstextbox.Size = new System.Drawing.Size(125, 27);
            this.elementstextbox.TabIndex = 10;
            this.elementstextbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Liczba elementów";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 535);
            this.Controls.Add(this.elementstextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chosenItemsListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.allItemsListBox);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.capacityTextBox);
            this.Controls.Add(this.seedTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox seedTextBox;
        private System.Windows.Forms.TextBox capacityTextBox;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.ListBox allItemsListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox chosenItemsListBox;
        private System.Windows.Forms.TextBox elementstextbox;
        private System.Windows.Forms.Label label5;
    }
}