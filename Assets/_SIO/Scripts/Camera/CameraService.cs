using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpSpeed;

    private void LateUpdate()
    {
        Character player = GameManager.Instance.CharacterFactory.Player;
        if (player == null || Camera.main == null)
            return;

        Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position,
            player.transform.position + offset,
            lerpSpeed * Time.deltaTime);
    }
}