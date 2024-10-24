using System.Collections.Generic;
using UnityEngine;

public class TeethManager : MonoBehaviour
{
    [SerializeField] private GameEvent _gameFinished;
    [SerializeField] private AudioSource _audioSource;

    private List<Teeth> _allTeeth = new();

    public static TeethManager Instance;
    private int _cleanedTeeth;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Teeth.TeethCleaning > 0 && _audioSource.isPlaying == false) _audioSource.Play();
        else if (Teeth.TeethCleaning <= 0 && _audioSource.isPlaying == true) _audioSource.Stop();
    }

    public void AddTeeth(Teeth teeth)
    {
        if(_allTeeth.Contains(teeth)==false) _allTeeth.Add(teeth);
    }

    public void ToothCleaned()
    {
        _cleanedTeeth++;
        if (_cleanedTeeth == _allTeeth.Count)
            _gameFinished.Invoke();
    }

    public void ResetTeeth()
    {
        foreach (Teeth tooth in _allTeeth)
            tooth.ResetCleaningProgress();
        Teeth.TeethCleaning = 0;
        _cleanedTeeth = 0;
    }
}
