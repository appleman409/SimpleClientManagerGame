using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        public GameDataManager gameDataManager = new GameDataManager();
        private List<Image> ListGameBackground = new List<Image>();
        private List<Button> gameButtons = new List<Button>(); // List to store game buttons
        public int curindex = -1;
        private Button? allAppsButton; // Reference to the All Apps button
        public PrivateFontCollection pfc = new PrivateFontCollection();

        public MainForm()
        {
            InitializeComponent();
            (new Core.DropShadow()).ApplyShadows(this);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameDataManager.LoadGameData();
            if (gameDataManager.GameDataList.Count <= 0)
            {
                MessageBox.Show("No game data found");
                NoGameForm noGameForm = new NoGameForm(this);
                noGameForm.Setup(this.Controls);
                return;
            }
            StartScene();
        }

       

        public void StartScene()
        {
            if (gameDataManager.GameDataList.Count > 0)
            {
                LoadImageBackground();
                curindex = 0;
                SetBackground();
                LoadButton();
                LoadGameIcons();
            }
        }

        public void StartScene(int index)
        {
            if (gameDataManager.GameDataList.Count > index)
            {
                LoadImageBackground();
                curindex = index;
                SetBackground();
                LoadButton();
                LoadGameIcons();
            }
        }

        private void LoadButton()
        {
            Button startButton = new Button();
            startButton.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            startButton.Location = new Point(670, 532);
            startButton.Name = "startButton";
            startButton.Size = new Size(360, 80);
            startButton.TabIndex = 0;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += new EventHandler(StartButton_Click);
            //Setting
            Button settingButton = new Button();
            settingButton.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            settingButton.Location = new Point(1090, 532);
            settingButton.Name = "settingButton";
            settingButton.Size = new Size(80, 80);
            settingButton.TabIndex = 0;
            settingButton.Text = "-";
            settingButton.UseVisualStyleBackColor = true;
            settingButton.Click += new EventHandler(SettingButton_Click);
            Controls.Add(settingButton);
            Controls.Add(startButton);

            allAppsButton = new Button();
            allAppsButton.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            allAppsButton.Size = new Size(65, 65);
            int xPosition = (sidebar.Width - allAppsButton.Width) / 2; // Center horizontally
            allAppsButton.Location = new Point(xPosition, sidebar.Height - 75); // Position at the bottom
            allAppsButton.Name = "allAppsButton";
            allAppsButton.TabIndex = 0;
            allAppsButton.Text = "All Apps";
            allAppsButton.UseVisualStyleBackColor = true;
            allAppsButton.Click += new EventHandler(AllAppsButton_Click);
            sidebar.Controls.Add(allAppsButton);
        }

        private void AllAppsButton_Click(object? sender, EventArgs e)
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
                    Gameid = gameDataManager.GameDataList.Count,
                    Gamepath = path,
                    Gameargs = args,
                    Gamebackground = ".//404.jpg",
                    Gameicon = iconPath
                };

                AddNewGame(game);
                MessageBox.Show("Game added successfully!");
                sidebar.Controls.Clear();
                StartScene(game.Gameid);
            }
        }

        
        private void SettingButton_Click(object? sender, EventArgs e)
        {
            int gameId = curindex; // Assuming curindex is the ID of the current game
            SettingsForm settingsForm = new SettingsForm(gameDataManager, gameId);
            settingsForm.FormClosed += (s, args) =>
            {
                // Reload the game data and update the UI after the settings form is closed
                gameDataManager.LoadGameData();
                ReloadIconsAndBackground();
            };
            settingsForm.ShowDialog();
        }

        private void ReloadIconsAndBackground()
        {
            // Update the background image
            if (curindex >= 0 && curindex < gameDataManager.GameDataList.Count)
            {
                GameData gameData = gameDataManager.GameDataList[curindex];
                if (File.Exists(gameData.Gamebackground))
                {
                    this.BackgroundImage = Image.FromFile(gameData.Gamebackground);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                sidebar.Controls.Clear();
                StartScene();
            }
            if( curindex == gameDataManager.GameDataList.Count)
            {
                curindex--;
                ReloadIconsAndBackground();
            }
            
        }
        private void StartButton_Click(object? sender, EventArgs e)
        {
            if (curindex >= 0 && curindex < gameDataManager.GameDataList.Count)
            {
                GameData gameData = gameDataManager.GameDataList[curindex];
                if (File.Exists(gameData.Gamepath))
                {
                    try
                    {
                        // Start the game executable
                        System.Diagnostics.Process.Start(gameData.Gamepath);

                        // Minimize the application to the system tray
                        this.WindowState = FormWindowState.Minimized;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to start the game: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Game executable not found.");
                }
            }
            else
            {
                MessageBox.Show("No game selected.");
            }
        }


        private void SetBackground()
        {
            if (ListGameBackground.Count < curindex + 1) return;
            if (curindex >= 0 && ListGameBackground[curindex] == null) return;
            this.BackgroundImage = curindex >= 0 ? ListGameBackground[curindex] : null;
            BackgroundImageLayout = ImageLayout.Stretch;

            // Highlight the current game button
            for (int i = 0; i < gameButtons.Count; i++)
            {
                if (i == curindex)
                {
                    gameButtons[i].BackColor = Color.Yellow; // Highlight color
                    gameButtons[i].Size = new Size(75, 75);
                }
                else
                {
                    gameButtons[i].BackColor = SystemColors.Control; // Default color
                    gameButtons[i].Size = new Size(65, 65);
                }
            }

            // Highlight the All Apps button if the current index is -1
            if (allAppsButton != null)
            {
                allAppsButton.BackColor = curindex == -1 ? Color.Yellow : SystemColors.Control;
            }
        }

        private void LoadImageBackground()
        {
            ListGameBackground.Clear();
            foreach (var item in gameDataManager.GameDataList)
            {
                if (item.Gamebackground != null)
                {
                    if (IsValidImg(item.Gamebackground))
                    {
                        ListGameBackground.Add(Image.FromFile(item.Gamebackground, true));
                    }
                }
            }
        }

        bool IsValidImg(string filename)
        {
            if (filename == String.Empty) return false;
            try
            {
                using (Image newImg = Image.FromFile(filename))
                { }
            }
            catch (OutOfMemoryException)
            {
                return false;
            }
            return true;
        }

        public void AddNewGame(GameData newGame)
        {
            gameDataManager.AddNewGame(newGame);
            AddGameIconButton(newGame);
        }

        private void AddGameIconButton(GameData game)
        {
            Button gameButton = new Button();
            gameButton.Size = new Size(65, 65);

            // Calculate the position from bottom to top
            int buttonIndex = gameDataManager.GameDataList.Count - game.Gameid - 1;
            int xPosition = (sidebar.Width - gameButton.Width) / 2; // Center horizontally
            gameButton.Location = new Point(xPosition, sidebar.Height - (75 * (buttonIndex + 2)) - 10);

            if (File.Exists(game.Gameicon))
            {
                gameButton.BackgroundImage = Image.FromFile(game.Gameicon);
            }
            else
            {
                // Handle the case where the file does not exist
                MessageBox.Show($"Icon file not found: {game.Gameicon}");
                // Optionally, set a default image
                // gameButton.BackgroundImage = Properties.Resources.DefaultIcon;
            }

            gameButton.BackgroundImageLayout = ImageLayout.Stretch;
            gameButton.Click += (sender, e) => GameIconButton_Click(sender, e, game.Gameid);
            sidebar.Controls.Add(gameButton);
            gameButtons.Add(gameButton); // Add the button to the list
        }

        private void GameIconButton_Click(object? sender, EventArgs e, int gameId)
        {
            curindex = gameId;
            SetBackground();
        }

        private void LoadGameIcons()
        {
            foreach (var game in gameDataManager.GameDataList)
            {
               
                AddGameIconButton(game);
            }
        }

        private void ExitButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void AppButton_Click(object? sender, EventArgs e)
        {
            // Logic for App button click
        }
    }
}

