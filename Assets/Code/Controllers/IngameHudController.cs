﻿using UnityEngine;
using UnityEngine.UI;


public class IngameHudController : MonoBehaviour
{
    public Button pauseButton;
    public TMPro.TextMeshProUGUI leftScoreLabel;
    public TMPro.TextMeshProUGUI rightScoreLabel;

    private RecordedScore lastRecordedScore;

    void OnEnable()
    {
        GameEventCenter.scoreChange.AddListener(UpdateScore);
        pauseButton.onClick.AddListener(TriggerPauseGameEvent);
    }
    void OnDisable()
    {
        GameEventCenter.scoreChange.RemoveListener(UpdateScore);
        pauseButton.onClick.RemoveListener(TriggerPauseGameEvent);
    }

    private void UpdateScore(RecordedScore recordedScore)
    {
        lastRecordedScore    = recordedScore;
        leftScoreLabel.text  = recordedScore.LeftPlayerScore.ToString();
        rightScoreLabel.text = recordedScore.RightPlayerScore.ToString();
    }
    private void TriggerPauseGameEvent()
    {
        if (lastRecordedScore == null)
        {
            Debug.LogError("LastRecordedScore received by HudController is null");
        }
        GameEventCenter.pauseGame.Trigger(lastRecordedScore);
    }
}
