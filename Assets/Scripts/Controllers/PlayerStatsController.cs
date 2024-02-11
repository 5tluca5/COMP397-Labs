using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsController : MonoBehaviour, IObserver<PlayerEnum>
{
    [SerializeField] Subject<PlayerEnum> playerSubject;
    [SerializeField] int health = 3;
    private void Awake()
    {
        playerSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Subject<PlayerEnum>>();
    }

    private void OnEnable()
    {
        playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        playerSubject.RemoveObserver(this);
    }

    public void OnNotify(PlayerEnum playerEnum)
    {
        Debug.Log(playerEnum.ToString());
        if (playerEnum == PlayerEnum.Die)
        {

            health -= 1;

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
