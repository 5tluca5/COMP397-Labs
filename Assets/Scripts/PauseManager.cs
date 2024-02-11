using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePage;

    COMP397Labs inputs;
    bool isPausing = false;
    void Awake()
    {
        inputs = new COMP397Labs();
        inputs.Player.Pause.performed += context => SetPause(!isPausing);
    }

    private void OnEnable() => inputs.Enable();

    private void OnDisable() => inputs.Disable();

    public void SetPause(bool pause)
    {
        isPausing = pause;
        pausePage.SetActive(isPausing);
        //if(isPausing)
        //{
        //    inputs.Disable();
        //}
        //else
        //{
        //    inputs.Enable();
        //}
        Time.timeScale = isPausing ? 0 : 1;
    }
}
