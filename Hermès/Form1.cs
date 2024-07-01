using System;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;

namespace Hermès
{
    public partial class Form1 : Form
    {
        private readonly System.Timers.Timer _timer;
        private readonly System.Timers.Timer _displayTimer;
        private int _timeLeft;
        private bool _isScrutating;
        private int _filesMovedCount;
        private ResourceManager resManager;
        private CultureInfo cultureInfo;

        public Form1()
        {
            // Initialisation du gestionnaire de ressources
            resManager = new ResourceManager("Hermès.Properties.Resources", typeof(Form1).Assembly);
            cultureInfo = CultureInfo.CurrentCulture;

            InitializeComponent();

            LoadSettings();
            InitializeDarkModeToggle();

            // Charger l'état du mode sombre depuis les paramètres de l'application
            if (Properties.Settings.Default.IsDarkModeEnabled)
            {
                SetDarkMode();
                checkBox1.Checked = true;
            }
            else
            {
                SetClassicMode();
                checkBox1.Checked = false;
            }

            // Initialisation des timers
            _timer = new System.Timers.Timer();
            _displayTimer = new System.Timers.Timer();

            // Remplir le ComboBox avec les options de langue
            cbLG.Items.Add("English");
            cbLG.Items.Add("Français");
            cbLG.SelectedIndexChanged += cbLG_SelectedIndexChanged;

            // Charger et définir la langue
            string savedLanguage = Properties.Settings.Default.Language;
            SetLanguage(savedLanguage); // Définir la langue après avoir rempli le ComboBox

            // Vérifier si les répertoires existent et lancer la scrutation si c'est le cas
            if (Directory.Exists(tbSrc.Text) && Directory.Exists(tbDst.Text))
            {
                StartScrutation();
            }
            else
            {
                lbTmr.Text = "Scrutation not launched";
            }

            UpdateLanguage();
        }

        private void InitializeDarkModeToggle()
        {
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            this.Controls.Add(checkBox1);
        }

        private void LoadSettings()
        {
            tbSrc.Text = Properties.Settings.Default.SourcePath;
            tbDst.Text = Properties.Settings.Default.DestinationPath;
            string savedLanguage = Properties.Settings.Default.Language;
            SetLanguage(savedLanguage);
        }


        private void SaveSettings()
        {
            Properties.Settings.Default.SourcePath = tbSrc.Text;
            Properties.Settings.Default.DestinationPath = tbDst.Text;
            Properties.Settings.Default.Language = cultureInfo.Name;
            Properties.Settings.Default.Save();
        }

        private void btnSrcBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbSrc.Text = fbd.SelectedPath;
                    SaveSettings();
                }
            }
        }

        private void btnDstBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbDst.Text = fbd.SelectedPath;
                    SaveSettings();
                }
            }
        }

        private void btnScr_Click(object sender, EventArgs e)
        {
            UpdateLanguage();
            if (_isScrutating)
            {
                StopScrutation();
            }
            else if (Directory.Exists(tbSrc.Text) && Directory.Exists(tbDst.Text))
            {
                StartScrutation();
            }
            else
            {
                MessageBox.Show(resManager.GetString("ErrorInvalidDirectories", cultureInfo), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartScrutation()
        {
            _isScrutating = true;
            _timeLeft = 60;
            lbTmr.Text = _timeLeft.ToString();
            btnScr.Text = "Stop scrutation";
            _timer.Interval = 1000; // 1 second
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private void StopScrutation()
        {
            _isScrutating = false;
            _timer.Stop();
            _timer.Elapsed -= OnTimedEvent; // Désabonner l'événement
            lbTmr.Text = "Scrutation not launched";
            btnScr.Text = "Start scrutation";
        }

        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            if (_timeLeft > 0)
            {
                _timeLeft--;
                lbTmr.Invoke((MethodInvoker)delegate
                {
                    lbTmr.Text = _timeLeft.ToString();
                });
            }
            else
            {
                _timer.Stop(); // Arrêter le timer principal
                _timer.Elapsed -= OnTimedEvent; // Désabonner l'événement pour éviter des appels multiples
                MoveFiles();
                _displayTimer.Interval = 3000; // 3 seconds
                _displayTimer.Elapsed += DisplayFilesMoved;
                _displayTimer.Start();
            }
        }

        private void MoveFiles()
        {
            _filesMovedCount = 0;
            try
            {
                foreach (var file in Directory.GetFiles(tbSrc.Text))
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(tbDst.Text, fileName);
                    File.Move(file, destFile, true); // Écraser les fichiers existants
                    _filesMovedCount++;
                }
                lbTmr.Invoke((MethodInvoker)delegate
                {
                    lbTmr.Text = $"{_filesMovedCount} {resManager.GetString("FilesMoved", cultureInfo)}";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{resManager.GetString("ErrorMovingFiles", cultureInfo)}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFilesMoved(object? source, ElapsedEventArgs e)
        {
            _displayTimer.Stop();
            _displayTimer.Elapsed -= DisplayFilesMoved; // Désabonner l'événement pour éviter des appels multiples
            _timeLeft = 57; // Réinitialiser le timer
            lbTmr.Invoke((MethodInvoker)delegate
            {
                lbTmr.Text = _timeLeft.ToString();
            });
            _timer.Elapsed += OnTimedEvent; // Réabonner l'événement pour la nouvelle scrutation
            _timer.Start(); // Redémarrer le timer principal
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Ne pas sélectionner le texte par défaut dans tbSrc
            tbSrc.Select(0, 0); // Place le curseur au début sans sélectionner
        }

        private void btnPTT_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                btnPTT.Text = "Pin to Top";
            }
            else
            {
                this.TopMost = true;
                btnPTT.Text = "Unpin from Top";
            }
            UpdateLanguage();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SetDarkMode();
            }
            else
            {
                SetClassicMode();
            }
        }

        private void SetDarkMode()
        {
            this.BackColor = Color.FromArgb(27, 27, 27);
            SetControlColors(this.Controls, Color.FromArgb(27, 27, 27), Color.White);
            // Sauvegarder l'état du mode sombre dans les paramètres de l'application
            Properties.Settings.Default.IsDarkModeEnabled = true;
            Properties.Settings.Default.Save();
        }

        private void SetClassicMode()
        {
            this.BackColor = Color.White;
            SetControlColors(this.Controls, Color.White, Color.Black);
            // Sauvegarder l'état du mode sombre dans les paramètres de l'application
            Properties.Settings.Default.IsDarkModeEnabled = false;
            Properties.Settings.Default.Save();
        }

        private void SetControlColors(Control.ControlCollection controls, Color backColor, Color foreColor)
        {
            foreach (Control control in controls)
            {
                if (control.Name == "btnPTT")
                {
                    continue;
                }

                if (control is Label || control is Button || control is TextBox || control is CheckBox || control is ComboBox)
                {
                    control.BackColor = backColor;
                    control.ForeColor = foreColor;
                }

                if (control.HasChildren)
                {
                    SetControlColors(control.Controls, backColor, foreColor);
                }
            }
        }

        private void cbLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLG.SelectedItem.ToString())
            {
                case "English":
                    cultureInfo = new CultureInfo("en");
                    break;
                case "Français":
                    cultureInfo = new CultureInfo("fr");
                    break;
            }
            UpdateLanguage();
            SaveSettings(); // Sauvegarder la langue après chaque changement
        }

        private void UpdateLanguage()
        {
            if (resManager == null || cultureInfo == null)
            {
                MessageBox.Show("Resource manager or culture info is not initialized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbTmr.Text = _isScrutating ? _timeLeft.ToString() : resManager.GetString("ScrutationNotLaunched", cultureInfo);
            btnScr.Text = _isScrutating ? resManager.GetString("StopScrutation", cultureInfo) : resManager.GetString("StartScrutation", cultureInfo);
            btnSrcBrowse.Text = resManager.GetString("Browse", cultureInfo);
            btnDstBrowse.Text = resManager.GetString("Browse", cultureInfo);
            btnPTT.Text = this.TopMost ? resManager.GetString("UnpinFromTop", cultureInfo) : resManager.GetString("PinToTop", cultureInfo);
            checkBox1.Text = resManager.GetString("DarkMode", cultureInfo);
            tbSrc.PlaceholderText = resManager.GetString("SourcePathPlaceholder", cultureInfo);
            tbDst.PlaceholderText = resManager.GetString("DestinationPathPlaceholder", cultureInfo);
        }


        private void SetLanguage(string language)
        {
            switch (language)
            {
                case "en":
                    if (cbLG.Items.Count > 0) cbLG.SelectedIndex = 0;
                    cultureInfo = new CultureInfo("en");
                    break;
                case "fr":
                    if (cbLG.Items.Count > 1) cbLG.SelectedIndex = 1;
                    cultureInfo = new CultureInfo("fr");
                    break;
                default:
                    if (cbLG.Items.Count > 0) cbLG.SelectedIndex = 0;
                    cultureInfo = new CultureInfo("en");
                    break;
            }
            // Mettre à jour le ResourceManager pour la culture courante
            resManager = new ResourceManager("Hermès.Properties.Resources", typeof(Form1).Assembly);
            UpdateLanguage();
        }

    }
}
