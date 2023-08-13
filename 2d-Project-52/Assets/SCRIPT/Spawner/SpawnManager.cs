using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    public Transform EnemyTarget;
    public static SpawnManager Instance;

    public List<Enemy> enemies;
    public Dictionary<Enemy, ObjectPool<Enemy>> enemyDictionary = new();
    private List<EnemyStateMachine> stateMachines = new();
    [SerializeField] private EnemyStateMachine grimReaper;
    private void Awake()
    {
        Instance = this;
        AddToPoolList();
    }

    void AddToPoolList()
    {
        foreach(var _prefab in enemies)
        {
            if(!enemyDictionary.TryGetValue(_prefab, out ObjectPool<Enemy> _pool))
            {
                enemyDictionary.Add(_prefab, new ObjectPool<Enemy>(() =>
                {
                    var _enemy = Instantiate(_prefab, transform);
                    _enemy.Init(KillEnemy);
                    _enemy.name = _prefab.name;
                    _enemy.Target = EnemyTarget;

                    return _enemy;
                },  _enemy =>
                {
                    _enemy.gameObject.SetActive(true);
                    stateMachines.Add(_enemy.GetComponent<EnemyStateMachine>());
                },  _enemy =>
                {
                    _enemy.gameObject.SetActive(false);
                    stateMachines.Remove(_enemy.GetComponent<EnemyStateMachine>());
                },  _enemy =>
                {
                    Destroy(_enemy.gameObject);
                }, true, 20, 100));
            }
        }
    }

    private void FixedUpdate()
    {
        grimReaper.Tick();

        if (stateMachines.Count == 0)
            return;

        foreach (EnemyStateMachine _state in stateMachines)
            _state.Tick();
    }

    private void KillEnemy(Enemy _prefab, Enemy _enemy)
    {
        foreach(ObjectPool<Enemy> _e in enemyDictionary.Values)
        {
            Debug.Log(_e);
        }
        enemyDictionary[_prefab].Release(_enemy);
    }
}
