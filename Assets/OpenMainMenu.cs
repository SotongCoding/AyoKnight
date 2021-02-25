using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMainMenu : MonoBehaviour {
    public GameObject startMenu;
    public void Open () {
        SceneLoader.LoadScene (1);
        startMenu.SetActive (false);
        ResourcesUIControl.TurnUION (true);
    }
}