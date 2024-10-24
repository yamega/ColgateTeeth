using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private float _targetTime;
    [SerializeField] private TMP_Text _text;

    [Header("Events")]
    [SerializeField] private GameEvent _onGameFinished;

    private float _currentTime;
    private bool _hasStarted;

    private void Update()
    {
        if( _hasStarted == true)
        {
            _currentTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_currentTime / 60);
            int seconds = Mathf.FloorToInt(_currentTime % 60);
            _text.SetText("{0:00} : {1:00}",minutes,seconds);
            if (_currentTime <= 0.0f) StopTimer();
        }
    }

    public void StartTimer()
    {
        _currentTime = _targetTime;
        _hasStarted = true;
    }

    public void StopTimer()
    {
        _hasStarted = false;
        _onGameFinished.Invoke();
    }
}
