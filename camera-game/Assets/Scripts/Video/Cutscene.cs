using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    public string nextScene;
    public GameObject skipButton;
    public float skipbuttonShowDuration = 5f;

    private VideoPlayer video;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        playerInput.SwitchCurrentActionMap("UI");
        playerInput.actions["Submit"].performed += ShowSkipButton;

        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += OnCutsceneEnd;

    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
    public void ShowSkipButton(InputAction.CallbackContext context)
    {
        if (this == null) return;
        skipButton.SetActive(true);
        playerInput.actions["Submit"].performed -= ShowSkipButton;
        playerInput.actions["Submit"].performed += OnSkip;
        StartCoroutine(HideSkipButton());
    }

    public IEnumerator HideSkipButton()
    {
        yield return new WaitForSeconds(skipbuttonShowDuration);

        skipButton.SetActive(false);
        playerInput.actions["Submit"].performed -= OnSkip;
        playerInput.actions["Submit"].performed += ShowSkipButton;
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        if (this == null) return;
        playerInput.actions["Submit"].performed -= OnSkip;
        OnCutsceneEnd(video);
    }
}