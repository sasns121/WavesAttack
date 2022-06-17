using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Button _nexWaveButton;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Spawner _spawner;

    private void Awake()
    {
        _timerText.GetComponent<TimerCounter>().Init(_spawner);
    }

    private void OnEnable()
    {
        _spawner.WaveEnd += OnWaveEnd;
        _spawner.WaveStart += OnWaveStart;
        _nexWaveButton.onClick.AddListener(OnNextWaveButtonClick);
    }
    private void OnDisable()
    {
        _spawner.WaveEnd -= OnWaveEnd;
        _spawner.WaveStart -= OnWaveStart;
        _nexWaveButton.onClick.RemoveListener(OnNextWaveButtonClick);
    }
    private void OnWaveEnd() {
        _nexWaveButton.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(true);
    }
    private void OnWaveStart()
    {
        _nexWaveButton.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(false);
    }

    public void OnNextWaveButtonClick() {
        _spawner.IsSkip = true;
    }
}
