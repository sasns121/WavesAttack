using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveIndex=0;
    private int _aliveEnemiesCount;
    private float _deltaTimeBetweenWaves;

    private bool _isSkip = false;

    public bool IsSkip { get => _isSkip; set => _isSkip = value; }

    public event UnityAction<int, int> EnemyCountChanged;
    public event UnityAction WaveEnd;
    public event UnityAction WaveStart;

    private void Start()
    {
        if (_currentWaveIndex < _waves.Count)
        {
            SetWave(_currentWaveIndex);
            StartCoroutine(SpawnWaveCycle());
            EnemyCountChanged?.Invoke(0, 1);
        }
    }

    private void SetWave(int index) {
        _currentWave = _waves[index];
        
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _currentWave.spawnPoint.position, Quaternion.identity,_currentWave.spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;

    }
    private void OnEnemyDying(Enemy enemy) {
        _player.AddMoney(enemy.Reward);
        _aliveEnemiesCount--;
        EnemyCountChanged?.Invoke(_aliveEnemiesCount, _currentWave.WaveEnemyCount);
        enemy.Dying -= OnEnemyDying;
      
       
    }

    private IEnumerator SpawnWaveCycle() {
        WaveStart?.Invoke();
        for(int i = 0; i < _currentWave.WaveEnemyCount; i++) { 
            InstantiateEnemy();
            _aliveEnemiesCount++;
            EnemyCountChanged?.Invoke(_aliveEnemiesCount, _currentWave.WaveEnemyCount);
            yield return new WaitForSeconds(_currentWave.SpawnDalay);
        }

        yield return new WaitUntil(() => _aliveEnemiesCount <= 0);
        if (_currentWaveIndex+1 < _waves.Count)
        {
            WaveEnd?.Invoke();
        }
        while (_deltaTimeBetweenWaves < _currentWave.NextWaveDalay)
        {
            _deltaTimeBetweenWaves += Time.deltaTime;
            if (IsSkip)
            {
                IsSkip = false;
                break;
            }
            else
                yield return null;
        }

        _deltaTimeBetweenWaves = 0;
        _currentWaveIndex++;
        if (_currentWaveIndex < _waves.Count)
        {
            SetWave(_currentWaveIndex);
            StartCoroutine(SpawnWaveCycle());
        }
        else
            _currentWave = null;
    }
    public string TimeBeforeNextWave()
    {
        return (_currentWave.NextWaveDalay-_deltaTimeBetweenWaves).ToString("0");
    }
}

[System.Serializable]
public class Wave {
    public Transform spawnPoint;
    public GameObject Template;
    public float SpawnDalay;
    public int WaveEnemyCount;
    public int NextWaveDalay;

}