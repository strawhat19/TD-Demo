using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour {
    [HideInInspector]
    public Transform finishLine;

    void Start() {
        SetFinishLine();
    }

    void SetFinishLine() {
        GameObject finishLineObject = GameObject.FindGameObjectWithTag("Finish");
        if (finishLineObject != null) {
            finishLine = finishLineObject.GetComponent<Transform>();
        }
    }

    public string RemoveDotZero(string input) {
        if (decimal.TryParse(input, out decimal number)) {
            return number.ToString("0.##");
        }
        return input;
    }
}