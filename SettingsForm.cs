using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WinFormsApp1
{
    public partial class SettingsForm : Form
    {
        private TextBox exePathTextBox;
        private TextBox argumentsTextBox;
        private TextBox backgroundImagePathTextBox;
        private TextBox gameIconPathTextBox;
        private GameDataManager gameDataManager;
        private int gameId;

        public SettingsForm(GameDataManager gameDataManager, int gameId)
        {
            this.gameDataManager = gameDataManager;
            this.gameId = gameId;
            InitializeComponent();
            LoadGameData();
        }

        private void InitializeComponent()
        {
            this.Text = "Settings";
            this.Size = new Size(500, 300);

            // Executable Path
            Label exePathLabel = new Label() { Text = "Executable Path:", Location = new Point(20, 20) };
            exePathTextBox = new TextBox() { Location = new Point(150, 20), Width = 250 };
            Button exePathButton = new Button() { Text = "Browse", Location = new Point(410, 20) };
            exePathButton.Click += (sender, e) => OpenFileDialog(exePathTextBox);

            // Arguments
            Label argumentsLabel = new Label() { Text = "Arguments:", Location = new Point(20, 60) };
            argumentsTextBox = new TextBox() { Location = new Point(150, 60), Width = 250 };

            // Background Image Path
            Label backgroundImagePathLabel = new Label() { Text = "Background Image:", Location = new Point(20, 100) };
            backgroundImagePathTextBox = new TextBox() { Location = new Point(150, 100), Width = 250 };
            Button backgroundImagePathButton = new Button() { Text = "Browse", Location = new Point(410, 100) };
            backgroundImagePathButton.Click += (sender, e) => OpenFileDialog(backgroundImagePathTextBox);

            // Game Icon Path
            Label gameIconPathLabel = new Label() { Text = "Game Icon:", Location = new Point(20, 140) };
            gameIconPathTextBox = new TextBox() { Location = new Point(150, 140), Width = 250 };
            Button gameIconPathButton = new Button() { Text = "Browse", Location = new Point(410, 140) };
            gameIconPathButton.Click += (sender, e) => OpenFileDialog(gameIconPathTextBox);

            // Save Button
            Button saveButton = new Button() { Text = "Save", Location = new Point(20, 180) };
            saveButton.Click += SaveButton_Click;

            Button delButton = new Button() { Text = "Delete", Location = new Point(100, 180) };
            delButton.Click += (sender, e) =>
            {
                var gameData = gameDataManager.GameDataList[gameId];
                if (gameData != null)
                {
                    gameDataManager.RemoveGame(gameData);
                    MessageBox.Show("Game deleted!");
                    this.Close();
                }
            };

            // Add controls to the form
            this.Controls.Add(exePathLabel);
            this.Controls.Add(exePathTextBox);
            this.Controls.Add(exePathButton);
            this.Controls.Add(argumentsLabel);
            this.Controls.Add(argumentsTextBox);
            this.Controls.Add(backgroundImagePathLabel);
            this.Controls.Add(backgroundImagePathTextBox);
            this.Controls.Add(backgroundImagePathButton);
            this.Controls.Add(gameIconPathLabel);
            this.Controls.Add(gameIconPathTextBox);
            this.Controls.Add(gameIconPathButton);
            this.Controls.Add(saveButton);
            this.Controls.Add(delButton);
        }

        private void LoadGameData()
        {
            var gameData = gameDataManager.GameDataList.Find(g => g.Gameid == gameId);
            if (gameData != null)
            {
                exePathTextBox.Text = gameData.Gamepath;
                argumentsTextBox.Text = gameData.Gameargs;
                backgroundImagePathTextBox.Text = gameData.Gamebackground;
                gameIconPathTextBox.Text = gameData.Gameicon;
            }
        }

        private void OpenFileDialog(TextBox textBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var gameData = gameDataManager.GameDataList[gameId];
            if (gameData != null)
            {
                gameData.Gamepath = exePathTextBox.Text;
                gameData.Gameargs = argumentsTextBox.Text;
                gameData.Gamebackground = backgroundImagePathTextBox.Text;
                gameData.Gameicon = gameIconPathTextBox.Text;

                // Save the updated GameDataManager list
                gameDataManager.SaveGameData();

                MessageBox.Show("Settings saved!");
                

                this.Close();

                // Reload icons and background
                ReloadIconsAndBackground(gameData);
                
            }
        }

        private void ReloadIconsAndBackground(GameData gameData)
        {
            // Implement the logic to reload icons and background based on the saved settings
            // This might involve updating the MainForm or other parts of your application
        }
    }
}

