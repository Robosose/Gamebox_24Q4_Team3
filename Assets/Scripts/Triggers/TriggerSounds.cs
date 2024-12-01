using UnityEngine;

public class TriggerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_audioSource.isPlaying) // ���������, ����� ���� �� ������������
        {
            _audioSource.Play(); // ����������� ����
        }
    }
}
