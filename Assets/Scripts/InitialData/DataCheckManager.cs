using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCheckManager : MonoBehaviour {

    public event Action finishInitialize;
    public static DataCheckManager _instance;
    int doneAmout = 0;
    int targetDone = 0;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy (gameObject);
        }
    }

    static public void StartInitializeData (params IDataChecker[] checkers) {
        _instance.targetDone = checkers.Length;
        _instance.StartCoroutine (_instance.CheckProcess ());

        foreach (var item in checkers) {
            item.InitializeData (_instance);
        }
    }
    static public void StartInitializeData_Battle (params IDataChecker[] checkers) {
        _instance.targetDone = checkers.Length;
        _instance.StartCoroutine (_instance.CheckProcess ());

        foreach (var item in checkers) {
            item.InitializeData_Battle (_instance);
        }
    }

    void CallDoneInitialize () {
        if (finishInitialize != null) {
            finishInitialize ();
        } else {
            Debug.LogWarning ("Initialize Complate but there are no event when complete");
        }
        doneAmout = targetDone = 0;
    }
    public void CallDone () {
        doneAmout++;
    }

    IEnumerator CheckProcess () {
        while (doneAmout != targetDone) {
            yield return null;
        }
        CallDoneInitialize ();
    }

}