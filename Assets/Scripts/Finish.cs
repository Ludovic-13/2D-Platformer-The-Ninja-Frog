using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private Animator anim;
    private Animator playerAnim;
    private PlayerMovement playerMovement;
    private float nextLevelDelay = 4f;

    private void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            finishSoundEffect.Play();
            Destroy(playerMovement);
            playerAnim.SetInteger("state", 0);
            anim.SetBool("flag_out", true);
            Invoke("NextLevel", nextLevelDelay);
        }
    }

    private void UpdateAnimationState()
    {
        anim.SetBool("flag_out", false);
        anim.SetBool("flag_idle", true);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
