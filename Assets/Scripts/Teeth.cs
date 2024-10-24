using UnityEngine;

public class Teeth : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayerMask _brushLayer;

    [Header("Values")]
    [SerializeField] private FloatValue _cleaningTime;

    private Material _material;
    private ParticleSystem _bubbleParticles;

    private float _cleaningTimer;
    private bool _isCleaning;
    private bool _isCleaned;

    public static int TeethCleaning;

    private void Start()
    {
        TeethManager.Instance?.AddTeeth(this);

        _material = GetComponent<Renderer>().material;
        _bubbleParticles = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.ShapeModule particleShape = _bubbleParticles.shape;
        particleShape.meshRenderer = GetComponent<MeshRenderer>();
        _material.SetFloat("_Range", 0.0f);
    }

    private void Update()
    {
        if( _isCleaning == true && _isCleaned == false) UpdateCleaningPorgress();
        else if(_bubbleParticles.isPlaying == true) _bubbleParticles.Stop();
    }

    public void UpdateCleaningPorgress()
    {
        _cleaningTimer += Time.deltaTime;
        _material.SetFloat("_Range", _cleaningTimer / _cleaningTime.Value);
        if(_cleaningTimer >= _cleaningTime.Value)
        {
            _isCleaned = true;
            _isCleaning = false;
            TeethCleaning--;
            _cleaningTimer = 0.0f;
            TeethManager.Instance.ToothCleaned();
        }
    }

    public void ResetCleaningProgress()
    {
        _isCleaned = false;
        _isCleaning = false;
        _cleaningTimer = 0.0f;
        _material.SetFloat("_Range", 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(_brushLayer, 2) && _isCleaned == false)
        {
            TeethCleaning++;
            _isCleaning = true;
            _bubbleParticles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(_brushLayer, 2) && _isCleaned == false)
        {
            TeethCleaning--;
            _isCleaning = false;
            _bubbleParticles.Stop();
        }
    }
}
