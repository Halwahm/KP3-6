using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundMusic; // Звуковой файл для фоновой музыки
    private AudioSource audioSource; // Ссылка на аудиоисточник

    void Start()
    {
        // Получаем ссылку на аудиоисточник, если он уже присоединен к объекту
        audioSource = GetComponent<AudioSource>();

        // Если аудиоисточник отсутствует, создаем новый и присоединяем его к объекту
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Настройка параметров звука
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.3f; // Устанавливаем громкость на 50%
        audioSource.pitch = 0.65f; // Начальная высота тона
        audioSource.spatialBlend = 0.0f; // Устанавливаем нулевой пространственный бленд для фонового звука
        audioSource.playOnAwake = true; // Включаем воспроизведение при старте

        // Воспроизводим звук
        audioSource.Play();
    }
}
