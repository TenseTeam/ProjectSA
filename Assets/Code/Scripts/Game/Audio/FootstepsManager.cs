using ProjectSA.Managers.GameManager;
using ProjectSA.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VUDK.Features.Main.CharacterController.Movements;
using VUDK.Generic.Managers.Main;
using VUDK.Generic.Managers.Main.Interfaces.Casts;
using VUDK.Generic.Serializable;
using Unity.Mathematics;

public class FootstepsManager : MonoBehaviour, ICastGameManager<PSAGameManager>
{
    public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
    public CharacterMovement characterMovement => GameManager.PlayerManager.CharacterMovement;

    public AudioSource footstepSource; 
    public AudioClip[] footstepConcreteClips; 
    public AudioClip[] footstepCarpetClips;

    private DelayTask delayTask;
    public float stepTimer = 0.5f;

    public LayerMask layerMask;

    private void Awake()
    {
        delayTask = new DelayTask();
        delayTask.Start();  
        delayTask.Pause();
    }

    private void OnEnable()
    {
        delayTask.OnTaskCompleted += PlayFootstepsSound;
    }

    private void OnDisable()
    {
        delayTask.OnTaskCompleted -= PlayFootstepsSound;
    }

    private void Update()
    {
        delayTask.Process();
        if (characterMovement.IsMoving)
        {
            if (characterMovement.IsRunning)
            {
                delayTask.ChangeDuration(stepTimer /2f);
            }
            else
            { 
                delayTask.ChangeDuration(stepTimer);
            }
            delayTask.Resume();
        }
        else
        {
            delayTask.Pause();
        }
    }

    private void PlayFootstepsSound()
    {
        delayTask.Start();
        if (Physics.Raycast(characterMovement.transform.position, -characterMovement.transform.up, out RaycastHit hit, 2, layerMask))
        {
            if (hit.transform.gameObject.layer == 9)
            {
                PlayCarpetSound();
            }
            else
            {
                PlayConcreteSound();
            }
        }
    }

    private void PlayCarpetSound()
    {
        int randomIndex = UnityEngine.Random.Range(0, footstepCarpetClips.Length); 
        footstepSource.clip = footstepCarpetClips[randomIndex];
        footstepSource.Play();
    }

    private void PlayConcreteSound()
    {
        int randomIndex = UnityEngine.Random.Range(0, footstepConcreteClips.Length);
        footstepSource.clip = footstepConcreteClips[randomIndex];
        footstepSource.Play();
    }
}
