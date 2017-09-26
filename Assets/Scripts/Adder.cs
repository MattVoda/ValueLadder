using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK.UnityEventHelper;

public class Adder : MonoBehaviour {

    public Text mainDisplay;
    private float currentNumber = 0;

    private void Awake() {
        if (mainDisplay == null) {
            print("missing display reference");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Add(float additionalNumber) {
        float tempNumber = currentNumber + additionalNumber;
        currentNumber = tempNumber;
        Display(currentNumber);
    }

    void Display(float numberToDisplay) {
        mainDisplay.text = numberToDisplay.ToString();
    }
}
