using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;
using Rnd = UnityEngine.Random;

public class OwO_Butons_Script : MonoBehaviour 
{

    public class ModSettingsJSON
    {
        public float TimeLimit;
        public int _stageCount;
    }


    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;
    public KMColorblindMode Colorblind;
    public KMModSettings modSettings;

    public KMSelectable[] Buttons;
    public TextMesh[] ButtonsText;
    public MeshRenderer[] ButtonsLED;
    public MeshRenderer LED;


























    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
