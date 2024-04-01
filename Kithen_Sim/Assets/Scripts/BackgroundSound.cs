using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundMusic; // �������� ���� ��� ������� ������
    private AudioSource audioSource; // ������ �� �������������

    void Start()
    {
        // �������� ������ �� �������������, ���� �� ��� ����������� � �������
        audioSource = GetComponent<AudioSource>();

        // ���� ������������� �����������, ������� ����� � ������������ ��� � �������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ��������� ���������� �����
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.3f; // ������������� ��������� �� 50%
        audioSource.pitch = 0.65f; // ��������� ������ ����
        audioSource.spatialBlend = 0.0f; // ������������� ������� ���������������� ����� ��� �������� �����
        audioSource.playOnAwake = true; // �������� ��������������� ��� ������

        // ������������� ����
        audioSource.Play();
    }
}
