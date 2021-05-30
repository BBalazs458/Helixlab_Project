using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField]
    private int _neddedStuff = 0;
    private bool _nextObjectives = false;

    public string firstObjective = "Reach the Light tower!";
    public string secondObjective = "Onboard to the Ship!";

    public GameObject lightTower;
    public GameObject endGame;


    public bool SetNextObjectives { set { _nextObjectives = value; } }

    private void Start()
    {
        if (lightTower == null) return;
        lightTower.SetActive(false);
        if (endGame == null) return;
        endGame.SetActive(false);
    }


    public void AddStuff(int s)
    {
        _neddedStuff += s;
    }

    private void OnGUI()
    {
        if (_neddedStuff == 3)
        {
            lightTower.SetActive(true);
            StartCoroutine(DisableGUI(firstObjective));
        }
        if (_nextObjectives)
        {
            endGame.SetActive(true);
            StartCoroutine(DisableGUI(secondObjective));
            _nextObjectives = false;
        }
    }


    IEnumerator DisableGUI(string objective)
    {

        GUI.Label(new Rect(0, 450, 300, 200), objective);
        yield return new WaitForSeconds(10f);
        GUI.enabled = false;

    }

}//
