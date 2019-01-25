using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace DevTipsAndresCartin
{
    public class WaitForTextSlide: Slide
    {
        private TextMeshProUGUI _textToSplit;
        private string[] _strings;
        private int _currentLine;

        private void Start()
        {
            _textToSplit = transform.Find("Description").GetComponent<TextMeshProUGUI>();
            if (_textToSplit.isActiveAndEnabled)
                _strings = _textToSplit.text.Split(new string[] { "\n" }, StringSplitOptions.None);
            else
                _strings = new string[];
            Debug.Log("STRING COUNT: " + _strings.Length);
        }

        private void OnEnable()
        {
            _currentLine = 1;
        }

        public override IEnumerator WaitForEnd()
        {
            if (_strings.Length > 1)
            {
                _textToSplit.maxVisibleLines = 1;
                do
                {
                    yield return WaitForInput();
                    yield return new WaitForEndOfFrame();

                    if (exitMode == ExitMode.Next)
                    {
                        _currentLine++;
                        while (_currentLine < _strings.Length && _strings[_currentLine].Length == 0)
                            _currentLine++;
                    }
                    else
                        _currentLine--;

                    Debug.Log("STRING " +_currentLine);

                    _textToSplit.maxVisibleLines = _currentLine;

                    if (_currentLine < 0 ||  _currentLine >= _strings.Length)
                    {
                        break;
                    }
                } while (true);
            }
            yield return WaitForInput();
        }
    }
}
