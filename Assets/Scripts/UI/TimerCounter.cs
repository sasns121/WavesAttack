using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class TimerCounter : MonoBehaviour
{
    private Spawner _spawner;
    private TMP_Text _text;



    public void Init(Spawner spawner) {
        _spawner = spawner;
    }
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }
    private void OnGUI()
    {
        if (_spawner)
            _text.text=_spawner.TimeBeforeNextWave();
    }
    
}
