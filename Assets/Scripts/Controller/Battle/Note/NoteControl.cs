using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteControl : MonoBehaviour {
    #region other
    [SerializeField] Button submitBtn;
    #endregion
    #region Generate Note
    [SerializeField] int noteAmount;
    [SerializeField] NoteChecker notePerf;
    [SerializeField] int[] noteGenerateCode_weap;
    [SerializeField] int[] noteGenerateCode_armor;

    [SerializeField] Transform notePlace;
    #endregion

    #region Note Check
    List<NoteChecker> generatedNote = new List<NoteChecker> ();
    int cur_IndexNote = 0;
    int trueNote;
    #endregion

    private void Start () {
        submitBtn.interactable = false;
        BattleController._instance.OnGetPhase += GenerateNote;
    }

    public void setNoteAmout (int noteAmount) {
        this.noteAmount = noteAmount;
    }
    public void setNoteCode (int[] noteCode_weap,int[] noteCode_armor) {
        noteGenerateCode_weap = noteCode_weap;
        noteGenerateCode_armor = noteCode_armor;
    }

    public void GenerateNote (Phase pase) {
        int amount = Mathf.Clamp (noteAmount - cur_IndexNote, 1, 6);

        if (pase == Phase.player) {
            for (int i = 0; i < amount; i++) {
                int ran_note = Random.Range (0, noteGenerateCode_weap.Length);
                NoteChecker note = Instantiate (notePerf, notePlace);
                note.InitialNote (noteGenerateCode_weap[ran_note]);
                generatedNote.Add (note);
            }
        } else {
            for (int i = 0; i < amount; i++) {
                int ran_note = Random.Range (0, noteGenerateCode_armor.Length);
                NoteChecker note = Instantiate (notePerf, notePlace);
                note.InitialNote (noteGenerateCode_armor[ran_note]);
                generatedNote.Add (note);
            }
        }

    }

    public void CheckNote (int sendCode) {
        if (cur_IndexNote < noteAmount) {
            generatedNote[cur_IndexNote].Check (sendCode, out bool isCorrect);
            cur_IndexNote++;
            if (isCorrect) {
                trueNote++;
            }
        }
        if (cur_IndexNote % 6 == 0 && cur_IndexNote < noteAmount) {
            DestroyAllNote ();
            GenerateNote (default);
        }
        if (cur_IndexNote == noteAmount) {
            submitBtn.interactable = true;
        }
    }
    public void SubmitResult (out int trueNote) {
        trueNote = this.trueNote;
        cur_IndexNote = this.trueNote = 0;
        generatedNote.Clear ();
        submitBtn.interactable = false;
        DestroyAllNote ();
    }

    public void DestroyAllNote () {
        foreach (Transform item in notePlace) {
            Destroy (item.gameObject);
        }
    }

}