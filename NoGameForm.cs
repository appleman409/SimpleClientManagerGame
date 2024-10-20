using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal class NoGameForm
    {
        private MainForm mainForm;

        public NoGameForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void Setup(Control.ControlCollection Controls)
        {
            // This is a placeholder for the game form
            TextBox textBox = new TextBox();
            textBox.Text = "No game data found! You should install game to application";
            textBox.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            textBox.Location = new Point(100, 100);
            textBox.Size = new Size(1000, 100);
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.ForeColor = Color.Black;
            textBox.BorderStyle = BorderStyle.None;
            textBox.TextAlign = HorizontalAlignment.Center;
            Controls.Add(textBox);

            Button GetGame = new Button();
            GetGame.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            GetGame.Location = new Point(420, 250); 
            GetGame.Name = "getButton";
            GetGame.Size = new Size(260, 80);
            GetGame.TabIndex = 0;
            GetGame.Text = "Get Game";
            GetGame.UseVisualStyleBackColor = true;
            Controls.Add(GetGame);
            GetGame.Click += new EventHandler(GetButton_Click);
        }

        private void GetButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileexe = new OpenFileDialog();
            fileexe.Filter = "Executable files (*.exe)|*.exe";
            fileexe.FilterIndex = 1;
            fileexe.RestoreDirectory = true;
            if (fileexe.ShowDialog() == DialogResult.OK)
            {
                string path = fileexe.FileName;
                string args = "";
                Icon icon = Icon.ExtractAssociatedIcon(path);
                string iconPath = Path.Combine(Path.GetTempPath(), $"{Path.GetFileNameWithoutExtension(path)}.ico");
                using (FileStream fs = new FileStream(iconPath, FileMode.Create))
                {
                    icon.Save(fs);
                }
                GameData game = new GameData
                {
                    Gameid = mainForm.gameDataManager.GameDataList.Count,
                    Gamepath = path,
                    Gameargs = args,
                    Gamebackground = ".//404.jpg",
                    Gameicon = iconPath
                };

                mainForm.AddNewGame(game);
                MessageBox.Show("Game added successfully!");
                mainForm.StartScene();
            }
        }
    }
}

