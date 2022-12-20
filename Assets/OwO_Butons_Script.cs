using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;
using Rnd = UnityEngine.Random;
using System;

public class OwO_Butons_Script : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;

    public KMSelectable[] Buttons;
    public Material[] LEDColors;
    public Transform[] ButtonsTransforms;
    public TextMesh[] ButtonsText;
    public MeshRenderer[] ButtonsLED;
    public MeshRenderer LED;

    private int _morse;
    private int _owo;
    private string _morseWord;
    private int _pressButton;
    private int[] _buttonOrder = new int[3];
    private bool _F;
    private int _stage;
    private bool _Solve;

    static int _moduleIdCounter = 1;
    int _moduleID = 0;

    void Awake()
    {
        _moduleID = _moduleIdCounter++;
        for (int i = 0; i < Buttons.Length; i++)
        {
            int x = i;
            Buttons[x].OnInteract += delegate
            {
                Press(x);
                return false;
            };
        }
    }

    // Use this for initialization
    void Start()
    {
        GetEdgework();
        Initialize();
    }

    private void Initialize()
    {
        _Solve = false;
        _morse = Rnd.Range(0, 5);
        int Helpy = Rnd.Range(0, 11);
        int[] CaseYesF = new int[] { 0, 1, 2, 4, 6, 8, 9, 10, 11, 13, 15, 17 };
        int[] CaseNoF = new int[] { 0, 1, 2, 3, 5, 7, 9, 10, 11, 12, 14, 16 };
        if (_F)
        {
            _owo = CaseYesF[Helpy];
        }

        if (!_F)
        {
            _owo = CaseNoF[Helpy];
        }

        _morseWord = Dictionary.MorseCodeWords[_morse];
        for (int i = 0; i < _buttonOrder.Length; i++)
        {
            _buttonOrder[i] = int.Parse(Dictionary.MorseToOwO[_owo][_morse][i].ToString());
            ButtonsText[_buttonOrder[i] - 1].text = (Dictionary.OwOToButtons[_owo][i]).ToString();
        }

        _stage = 1;
        StartCoroutine(LightFlash());
        
        Log("The flashing word is {0}", _morseWord);
        Log("The button labels are {0}, {1} and {2}", Dictionary.OwOToButtons[_owo][0], Dictionary.OwOToButtons[_owo][1], Dictionary.OwOToButtons[_owo][2]);
        Log("Serial Number {0} contain an F", _F ? "does" : "does not");
        Log("The correct order of button presses is {0}", Dictionary.MorseToOwO[_owo][_morse]);
    }

    private void GetEdgework()
    {
        _F = Bomb.GetSerialNumber().Contains("F");
    }

    private void Press(int index)
    {
        if (!_Solve)
        {
            Buttons[index].AddInteractionPunch();
            StartCoroutine(AnimateButton(ButtonsTransforms[index], _Solve));
            _pressButton = index;
            Verify();
        }
    }

    private void Log(string message, params object[] args)
    {
        Debug.LogFormat("[OwO Buttons #{0}] {1}", _moduleID, string.Format(message, args));
    }

    private void Verify()
    {
        switch (_stage)
        {
            case 1:
                if (_pressButton == (_buttonOrder[0] - 1))
                {
                    Log("Pressed Button {0}. Correct!", _pressButton + 1);
                    StartCoroutine(AnimateLED(ButtonsLED[_pressButton], true));
                    _stage++;
                    break;
                }
                else
                {
                    Log("Pressed Button {0}. Wrong, striking. Expected {1}", _pressButton + 1, _buttonOrder[0]);
                    StartCoroutine(Incorect());
                }
                break;

            case 2:
                if (_pressButton == (_buttonOrder[1] - 1))
                {
                    Log("Pressed Button {0}. Correct!", _pressButton + 1);
                    StartCoroutine(AnimateLED(ButtonsLED[_pressButton], true));
                    _stage++;
                    break;
                }
                else
                {
                    Log("Pressed Button {0}. Wrong, striking. Expected {1}", _pressButton + 1, _buttonOrder[1]);
                    StartCoroutine(Incorect());
                }
                break;

            case 3:
                if (_pressButton == (_buttonOrder[2] - 1))
                {
                    Log("Pressed Button {0}. Correct! Module solved!", _pressButton + 1);
                    StartCoroutine(AnimateLED(ButtonsLED[_pressButton], true));
                    StartCoroutine(Solved());
                    break;
                }
                break;
        }
    }

    private IEnumerator Incorect()
    {
        Module.HandleStrike();
        StartCoroutine(AnimateLED(ButtonsLED[_pressButton], false));
        yield return null;
    }

    private IEnumerator Solved()
    {
        _Solve = true;
        Module.HandlePass();
        Audio.PlaySoundAtTransform("owo_oj0BqGJ", transform);
        for (int i = 0; i < ButtonsTransforms.Length; i++)
        {
            StartCoroutine(AnimateButton(ButtonsTransforms[i], _Solve));
        }
        yield return null;
    }

    private IEnumerator AnimateLED(Renderer LED, bool Correct)
    {
        const float duration = .5f;
        var elapsed = 0f;
        if (!Correct)
        {
            while (elapsed < duration)
            {
                yield return null;
                elapsed += Time.deltaTime;
                LED.material = LEDColors[1];
            }
            LED.material = LEDColors[0];
        }
        else
        {
            LED.material = LEDColors[2];
        }
    }

    private IEnumerator LightFlash()
    {
        string word = _morseWord;
        while (!_Solve)
        {
            foreach (char c in word)
            {
                string morse = Dictionary.MorseCode[Array.IndexOf(Dictionary.Alphabet, c)];
                foreach (char symbol in morse)
                {
                    LED.material = LEDColors[3];
                    yield return new WaitForSeconds(symbol == '.' ? 0.25f : 0.75f);
                    LED.material = LEDColors[0];
                    yield return new WaitForSeconds(0.25f);
                }
                yield return new WaitForSeconds(0.75f);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator AnimateButton(Transform btn, bool S)
    {
        const float duration = .35f;
        var elapsed = 0f;
        const float depressed = 0f;
        const float undepressed = 0;
        var originalPosition = btn.localPosition;
        var startValue = undepressed;
        var endValue = depressed;
        while (elapsed < duration)
        {
            yield return null;
            elapsed += Time.deltaTime;
            btn.localPosition = new Vector3(originalPosition.x, (endValue - startValue) * elapsed / duration + startValue, originalPosition.z);
        }
        btn.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
        if (S)
        {
            elapsed = 0f;
            while (elapsed < duration)
            {
                yield return null;
                elapsed += Time.deltaTime;
                btn.localPosition = new Vector3(originalPosition.x, (-0.05f - startValue) * elapsed / duration + startValue, originalPosition.z);
            }
            ButtonsText[3].text = Dictionary.OwO[_owo];
            LED.material = LEDColors[0];
            StopAllCoroutines();
        }
    }
}
