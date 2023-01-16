using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enigma
{
    class Rotor
    {
        public string _Perm;
        public char _CurrentLetter;
        public Dictionary<char, char> _Index = new Dictionary<char, char>(26);
        public Dictionary<char, char> _ReverseIndex = new Dictionary<char, char>(26);
        public int _Rotations = 0;

        public Rotor(string perm,char initLetter)
        {
            _Perm = perm;
            _CurrentLetter = initLetter;
            _Rotations = 0;

            UpdateIndex();
        }

        void UpdateIndex()
        {
            string sLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < sLetters.Length; ++i)
            {
                _Index[sLetters[i]] = _Perm[i];
                _ReverseIndex[_Perm[i]] = sLetters[i];
            }
        }

        void Rotate()
        {
            string newPerm = "";
            string sLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < sLetters.Length; ++i)
            {
                char c = sLetters[i];
                char nextC;
                if (c == 'Z')
                    nextC = 'A';
                else
                {
                    nextC = Convert.ToChar(c + 1);
                }

                char nextWasPointingTo = _Index[nextC];

                char prevCh;
                if (nextWasPointingTo == 'A')
                    prevCh = 'Z';
                else
                    prevCh = Convert.ToChar(nextWasPointingTo - 1);

                newPerm += prevCh;
            }
            _Perm = newPerm;
            UpdateIndex();
        }

        public int Advance()
        {
            if (_CurrentLetter == 'Z')
            {
                _CurrentLetter = 'A';
            }
            else
            {
                ++_CurrentLetter;
            }
            Rotate();
            int _TempRotations = ++_Rotations;
            if (_Rotations == 26)
                _Rotations = 1;
            return +_TempRotations;
        }

        public char GetCurrLetter()
        {
            return _CurrentLetter;
        }

        public char EncryptLetterLeft(char c)
        {
            char encC = _Index[c];
            return encC;
        }

        public char EncryptLetterRight(char c)
        {
            char encC = _ReverseIndex[c];
            return encC;
        }
    }
}
