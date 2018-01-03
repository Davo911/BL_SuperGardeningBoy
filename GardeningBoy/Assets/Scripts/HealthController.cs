using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {
    public float startHealth=1;
    public int startLifePoints = 1;
    float health = 1;
    int lifePoints = 1;
    Animator anim;
    PlayerController playerController;
    bool isDead = false;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        //erste Szene für den Ctrinoller-Einsatz(Anpassen falls menü oder so)
        if (Application.loadedLevel == 0)
        {
            health = startHealth;
            lifePoints = startLifePoints;
        }
        else
        {

            health = PlayerPrefs.GetFloat("Health");
            lifePoints = PlayerPrefs.GetInt("LifePoints");
        }
    }


    void ApplyDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(0, health);
        if (!isDead) { 
            if (health == 0)
            {
                isDead = true;
                Dying();

            }
    }
    }

    void Dying()
    {
        anim.SetBool("dying", true);
        playerController.enabled = false;
        lifePoints--;
        //if (lifePoints <= 0)
        //{
            Invoke("StartGame", 3);

        //}
        //else
        //{
        //   // restart Level
        //    Invoke("RestartLevel", 3);
        //}n
    }

    void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    void RestartLevel()
    {
        health = startHealth;
        isDead = false;
        anim.SetBool("dying", false);
        playerController.enabled = true;

        if (!playerController.lookingRight)
        {
            playerController.Flip();
        }
       // Level neugenerieren und Spieler zurücksetzten
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetInt("LifePoints", lifePoints );
    }
    // Update is called once per frame
    void Update () {
		
	}
}
