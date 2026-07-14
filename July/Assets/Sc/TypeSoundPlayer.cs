using UnityEngine;

/// <summary>
/// タイプライター表示中に文字が表示されるたびに効果音を鳴らすクラス。
/// </summary>
public class TypeSoundPlayer : MonoBehaviour
{
    [SerializeField] private TypewriterText typewriter;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typeSound;

    private void OnEnable()
    {
        typewriter.OnCharTyped += HandleCharTyped;
    }

    private void OnDisable()
    {
        typewriter.OnCharTyped -= HandleCharTyped;
    }

    private void HandleCharTyped(char c)
    {
        if (c == ' ' || c == '\n') return;
        if (audioSource != null && typeSound != null)
        {
            audioSource.PlayOneShot(typeSound);
        }
    }
}
