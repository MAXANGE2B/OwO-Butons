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
    private string _morseword;
    private int _pressButton;
    private int _buttonOrder1;
    private int _buttonOrder2;
    private int _buttonOrder3;
    private bool _F;
    private int _stage;
    private bool _solve;

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
        _solve = false;
        _morse = Rnd.Range(0, 5);
        _owo = Rnd.Range(0, 17);
        if (_F)
        {
            while (_owo == 3 || _owo == 5 || _owo == 7 || _owo == 12 || _owo == 14 || _owo == 16)
            {
                _owo = Rnd.Range(0, 17);
            }
        }
        if (!_F)
        {
            while (_owo == 4 || _owo == 6 || _owo == 8 || _owo == 13 || _owo == 15 || _owo == 17)
            {
                _owo = Rnd.Range(0, 17);
            }
        }
        _morseword = dictionnary.MorseCodeWords[_morse];
        _buttonOrder1 = int.Parse(dictionnary.MorseToOwO[_owo][_morse][0].ToString());
        _buttonOrder2 = int.Parse(dictionnary.MorseToOwO[_owo][_morse][1].ToString());
        _buttonOrder3 = int.Parse(dictionnary.MorseToOwO[_owo][_morse][2].ToString());
        Log("{0}", _buttonOrder1);
        Log("{0}", _buttonOrder2);
        Log("{0}", _buttonOrder3);
        Log("{0}", _morseword);
        ButtonsText[_buttonOrder1 - 1].text = (dictionnary.OwOToButtons[_owo][0]).ToString();
        ButtonsText[_buttonOrder2 - 1].text = (dictionnary.OwOToButtons[_owo][1]).ToString();
        ButtonsText[_buttonOrder3 - 1].text = (dictionnary.OwOToButtons[_owo][2]).ToString();
        _stage = 1;
        StartCoroutine(LightFlash());
    }

    private void GetEdgework()
    {

        _F = Bomb.GetSerialNumber().Contains("F");

        if (_F)
        {
            Debug.Log("F");
        }
    }

    private void Press(int index) // detect if a press as occurre and start a play set
    {
        Buttons[index].AddInteractionPunch();
        StartCoroutine(animateButton(ButtonsTransforms[index], false));
        _pressButton = index;
        Debug.Log(index);
        Log("stage {0}", _stage);
        StartCoroutine(GameLoop());
    }
    private void Log(string message, params object[] args)
    {
        Debug.LogFormat("[OwO Buttons #{0}] {1}", _moduleID, string.Format(message, args));
    }

    private IEnumerator GameLoop()
    {
        if (_stage == 1)
        {
            if (_pressButton == (_buttonOrder1 - 1))
            {
                StartCoroutine(animateLED(ButtonsLED[_pressButton], true));
                _stage++;
                yield break;
            }
            else
            {
                StartCoroutine(Incorect());
            }

        }

        if (_stage == 2)
        {
            if (_pressButton == (_buttonOrder2 - 1))
            {
                StartCoroutine(animateLED(ButtonsLED[_pressButton], true));
                _stage++;
                yield break;
            }
            else 
            {
                StartCoroutine(Incorect());
            }

        }

        if (_stage == 3)
        {
            if (_pressButton == (_buttonOrder3 - 1))
            {
                StartCoroutine(animateLED(ButtonsLED[_pressButton], true));
                StartCoroutine(Solved());
                yield break;
            }


        }
        yield return null;
    }

    private IEnumerator Incorect()
    {
        Module.HandleStrike();
        StartCoroutine(animateLED(ButtonsLED[_pressButton], false));
        yield return null;
    }

    private IEnumerator Solved()
    {
        Module.HandlePass();
        for (int i = 0; i < ButtonsTransforms.Length; i++)
        {
            StartCoroutine(animateButton(ButtonsTransforms[i], true));
        }
        yield return null;
    }

    private IEnumerator animateLED(Renderer LED, bool correct)
    {
        const float duration = .5f;
        var elapsed = 0f;
        if (!correct)
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
            LED.material = LEDColors[2];
    }

    IEnumerator LightFlash()
    {
        string word = _morseword;
        while (true)
        {
            foreach (char c in word)
            {
                string morse = dictionnary.MorseCode[Array.IndexOf(dictionnary.Alphabet, c)];
                foreach (char symbol in morse)
                {
                    LED.material = LEDColors[3];
                    //morseLight.SetActive(true);
                    yield return new WaitForSeconds(symbol == '.' ? 0.25f : 0.75f);
                    LED.material = LEDColors[0];
                    //morseLight.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                }
                yield return new WaitForSeconds(0.75f);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator animateButton(Transform btn, bool _s)
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
        if (_s)
        {

            elapsed = 0f;
            while (elapsed < duration)
            {
                yield return null;
                elapsed += Time.deltaTime;


                btn.localPosition = new Vector3(originalPosition.x, (-0.05f - startValue) * elapsed / duration + startValue, originalPosition.z);

            }
            ButtonsText[3].text = dictionnary.OwO[_owo];        
        }
    }
}
