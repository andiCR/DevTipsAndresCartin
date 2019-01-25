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
        public bool waitForText = true;
        private TextMeshProUGUI _textToSplit;
        //private string[] _strings;
        private int _currentLine = 1;
        private int _lineCount;

        private void Start()
        {
            _textToSplit = transform.Find("Description").GetComponent<TextMeshProUGUI>();
            //if (_textToSplit.isActiveAndEnabled && waitForText)
            //    _strings = _textToSplit.text.Split(new string[] { "\n" }, StringSplitOptions.None);
            //else
                //_strings = new string[0];

            _lineCount = _textToSplit.textInfo.lineCount;
        }

        private void OnEnable()
        {
            _currentLine = Mathf.Clamp(_currentLine, 1, _lineCount);
        }

        public override IEnumerator WaitForEnd()
        {
            if (_lineCount > 1 && waitForText && _textToSplit.isActiveAndEnabled)
            {
                var lines = _textToSplit.textInfo.lineInfo;
                _textToSplit.maxVisibleLines = _currentLine;
                do
                {
                    yield return WaitForInput();
                    yield return new WaitForEndOfFrame();

                    if (exitMode == ExitMode.Next)
                    {
                        _currentLine++;
                        Debug.Log(_currentLine + " of " + _lineCount);
                        while (_currentLine < _lineCount && lines[_currentLine].visibleCharacterCount == 0)
                            _currentLine++;
                    }
                    else
                        _currentLine--;

                    _textToSplit.maxVisibleLines = _currentLine;

                    if (_currentLine < 0 ||  _currentLine >= _lineCount)
                    {
                        break;
                    }
                } while (true);
            }
            yield return WaitForInput();
        }
    }
}
