using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace enigma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Dictionary<Key, Button> _LampButtons = new Dictionary<Key, Button>(26);
        Dictionary<Key, char> _KeyCodeToChar = new Dictionary<Key, char>(26);
        Rotor rotor1 = new Rotor(ROTOR1_INIT_PERM, ROTOR1_INIT_SETTING);
        Rotor rotor2 = new Rotor(ROTOR2_INIT_PERM, ROTOR2_INIT_SETTING);
        Rotor rotor3 = new Rotor(ROTOR2_INIT_PERM, ROTOR2_INIT_SETTING);

        Rotor reflector = new Rotor("IXUHFEZDAOMTKQJWNSRLCYPBVG", ' ');
        Button _LastActiveLampButton = null;

        private const char ROTOR1_INIT_SETTING = 'A';
        private const char ROTOR2_INIT_SETTING = 'A';
        private const char ROTOR3_INIT_SETTING = 'A';

        private const string ROTOR1_INIT_PERM = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        private const string ROTOR2_INIT_PERM = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        private const string ROTOR3_INIT_PERM = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";

        public MainWindow()
        {
            InitializeComponent();
            InitLampButtons();
            InitMachine();
        }

        private void InitLampButtons()
        {
            _LampButtons[Key.A] = btnLampA;
            _LampButtons[Key.B] = btnLampB;
            _LampButtons[Key.C] = btnLampC;
            _LampButtons[Key.D] = btnLampD;
            _LampButtons[Key.E] = btnLampE;
            _LampButtons[Key.F] = btnLampF;
            _LampButtons[Key.G] = btnLampG;
            _LampButtons[Key.H] = btnLampH;
            _LampButtons[Key.I] = btnLampI;
            _LampButtons[Key.J] = btnLampJ;
            _LampButtons[Key.K] = btnLampK;
            _LampButtons[Key.L] = btnLampL;
            _LampButtons[Key.M] = btnLampM;
            _LampButtons[Key.N] = btnLampN;
            _LampButtons[Key.O] = btnLampO;
            _LampButtons[Key.P] = btnLampP;
            _LampButtons[Key.Q] = btnLampQ;
            _LampButtons[Key.R] = btnLampR;
            _LampButtons[Key.S] = btnLampS;
            _LampButtons[Key.T] = btnLampT;
            _LampButtons[Key.U] = btnLampU;
            _LampButtons[Key.V] = btnLampV;
            _LampButtons[Key.W] = btnLampW;
            _LampButtons[Key.X] = btnLampX;
            _LampButtons[Key.Y] = btnLampY;
            _LampButtons[Key.Z] = btnLampZ;

            foreach (KeyValuePair<Key, Button> kv in _LampButtons)
            {
                kv.Value.Background = Brushes.Gray;
            }

            _KeyCodeToChar[Key.A] = 'A';
            _KeyCodeToChar[Key.B] = 'B';
            _KeyCodeToChar[Key.C] = 'C';
            _KeyCodeToChar[Key.D] = 'D';
            _KeyCodeToChar[Key.E] = 'E';
            _KeyCodeToChar[Key.F] = 'F';
            _KeyCodeToChar[Key.G] = 'G';
            _KeyCodeToChar[Key.H] = 'H';
            _KeyCodeToChar[Key.I] = 'I';
            _KeyCodeToChar[Key.J] = 'J';
            _KeyCodeToChar[Key.K] = 'K';
            _KeyCodeToChar[Key.L] = 'L';
            _KeyCodeToChar[Key.M] = 'M';
            _KeyCodeToChar[Key.N] = 'N';
            _KeyCodeToChar[Key.O] = 'O';
            _KeyCodeToChar[Key.P] = 'P';
            _KeyCodeToChar[Key.Q] = 'Q';
            _KeyCodeToChar[Key.R] = 'R';
            _KeyCodeToChar[Key.S] = 'S';
            _KeyCodeToChar[Key.T] = 'T';
            _KeyCodeToChar[Key.U] = 'U';
            _KeyCodeToChar[Key.V] = 'V';
            _KeyCodeToChar[Key.W] = 'W';
            _KeyCodeToChar[Key.X] = 'X';
            _KeyCodeToChar[Key.Y] = 'Y';
            _KeyCodeToChar[Key.Z] = 'Z';
        }

        private void InitMachine()
        {
            txtRotor1.Content = rotor1.GetCurrLetter();
            txtRotor2.Content = rotor2.GetCurrLetter();
            txtRotor3.Content = rotor3.GetCurrLetter();

            txtRotor1.Background = Brushes.LightBlue;
            txtRotor2.Background = Brushes.LightGoldenrodYellow;
            txtRotor3.Background = Brushes.LightPink;

            btnReset.Background = Brushes.LightSeaGreen;

            this.Background = Brushes.Lavender;

            txtPlain.Background = Brushes.Gold;
            txtEncrypted.Background = Brushes.Fuchsia;
        }

        private void TxtRotor1_Click(object sender, RoutedEventArgs e)
        {
            rotor1.Advance();
            txtRotor1.Content = rotor1.GetCurrLetter();
        }

        private void TxtRotor2_Click(object sender, RoutedEventArgs e)
        {
            rotor2.Advance();
            txtRotor2.Content = rotor2.GetCurrLetter();
        }

        private void TxtRotor3_Click(object sender, RoutedEventArgs e)
        {
            rotor3.Advance();
            txtRotor3.Content = rotor3.GetCurrLetter();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_LastActiveLampButton != null)
            {
                _LastActiveLampButton.Background = Brushes.Gray;
            }
            ProcessTheCharacter(e.Key);
        }

        private void ProcessTheCharacter(Key _key)
        {
            try
            {
                //if (_key == Key.Space)
                //{
                //    txtPlain.Text += ' ';
                //    txtEncrypted.Text += ' ';
                //    return;
                //}

                Button lampBtn = null;

                if (_LampButtons.TryGetValue(_key, out lampBtn) == false)
                    return;

                lampBtn.Background = Brushes.DarkTurquoise;
                _LastActiveLampButton = lampBtn;

                char c = _KeyCodeToChar[_key];

                txtPlain.Text += c;

                c = rotor1.EncryptLetterLeft(c);
                c = rotor2.EncryptLetterLeft(c);
                c = rotor3.EncryptLetterLeft(c);

                c = reflector.EncryptLetterLeft(c);

                c = rotor3.EncryptLetterRight(c);
                c = rotor2.EncryptLetterRight(c);
                c = rotor1.EncryptLetterRight(c);

                txtEncrypted.Text += c;

                if (AdvanceRotor1() == 26)
                {
                    if (AdvanceRotor2() == 26)
                    {
                        AdvanceRotor3();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        int AdvanceRotor1()
        {
            int rotations = rotor1.Advance();
            txtRotor1.Content = rotor1.GetCurrLetter();
            return rotations;
        }

        int AdvanceRotor2()
        {
            int rotations = rotor2.Advance();
            txtRotor2.Content = rotor2.GetCurrLetter();
            return rotations;
        }

        int AdvanceRotor3()
        {
            int rotations = rotor3.Advance();
            txtRotor3.Content = rotor3.GetCurrLetter();
            return rotations;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rotor1 = new Rotor(ROTOR1_INIT_PERM, ROTOR1_INIT_SETTING);
            rotor2 = new Rotor(ROTOR2_INIT_PERM, ROTOR2_INIT_SETTING);
            rotor3 = new Rotor(ROTOR3_INIT_PERM, ROTOR2_INIT_SETTING);

            txtRotor1.Content = rotor1.GetCurrLetter();
            txtRotor2.Content = rotor2.GetCurrLetter();
            txtRotor3.Content = rotor3.GetCurrLetter();

            txtPlain.Text = "";
            txtEncrypted.Text = "";
        }
    }
}