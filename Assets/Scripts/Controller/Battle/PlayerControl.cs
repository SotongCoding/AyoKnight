﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    NoteControl noteControl;
    Phase curentPhase;

    private void Awake () {
        noteControl = FindObjectOfType<NoteControl> ();
    }

    private void Start () {
        BattleController._instance.OnGetPhase += SetCurentPhase;
    }

    void SetCurentPhase (Phase phase) {
        curentPhase = phase;
    }
    public void CallCode (int code) {
        noteControl.CheckNote (code);
    }
    public void Submit (bool isRunOut) {
        BattleData_Enemy enemy = BattleController.getEnemyData ();
        BattleData_Player player = BattleController.getPlayerData ();
        noteControl.SubmitResult (out int correctNote);

        if (curentPhase == Phase.player) {
            if (!isRunOut) enemy.TakeDamage (player.CalculateDamage (correctNote));
        }
        else {
            if (!isRunOut) player.TakeDamage (enemy.CalculateDamage (player.defense_fix, correctNote));
            else player.TakeDamage (enemy.CalculateDamage (0, player.defense_fix));
        }

        StartCoroutine (GetNextPhase ());
        BattleController.isBattlePause = true;
    }

    IEnumerator GetNextPhase () {
        yield return new WaitForSeconds (1.3f); // animation duration
        BattleController._instance.CallOnGetPhase ();
        BattleController.isBattlePause = false;
    }
}