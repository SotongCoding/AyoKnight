using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLosePU : PopUpAction_Base {

    public UI_DropItemControl itemTemplate;
    public Transform lootPlace;

    public void GenerateItem (List<LootDropData> dropsItem) {
        foreach (var item in dropsItem) {
            Debug.Log("Generated");
            UI_DropItemControl obj = Instantiate (itemTemplate, lootPlace);
            obj.Initial (item);
        }
    }
    void ClearItem(){
        foreach (Transform item in lootPlace)
        {
            Destroy(item.gameObject);
        }
    }
    public override void SetData () {
        //Btn A = Next Btn B = Retry
        events = new PopUpEvent (NextEvent, RetryEvent, null);
    }

    void NextEvent () {
        SetEneble (false);
        SceneLoader.LoadScene (1);
        ClearItem();
        ResourcesUIControl.TurnUION(true);
    }
    void RetryEvent () {
        SetEneble (false);
        LevelLoader.ReLoadLevel ();
        ClearItem();
    }
}