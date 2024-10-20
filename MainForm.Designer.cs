using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WinFormsApp1
{
    partial class MainForm
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
            panel2 = new Panel();
            ExitButton = new Button();
            sidebar = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(ExitButton);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(76, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1186, 53);
            panel2.TabIndex = 1;
            // 
            // ExitButton
            // 
            ExitButton.BackColor = Color.Transparent;
            ExitButton.BackgroundImageLayout = ImageLayout.None;
            ExitButton.FlatStyle = FlatStyle.Popup;
            ExitButton.ForeColor = SystemColors.ControlText;
            ExitButton.Location = new Point(1136, 0);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(50, 53);
            ExitButton.TabIndex = 0;
            ExitButton.Text = "X";
            ExitButton.UseVisualStyleBackColor = false;
            ExitButton.Click += ExitButton_Click;
            // 
            // sidebar
            // 
            sidebar.BackColor = SystemColors.Desktop;
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(76, 673);
            sidebar.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(sidebar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "MainForm";
            Load += Form1_Load;
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Button ExitButton;
        private Panel sidebar;
    }
}
