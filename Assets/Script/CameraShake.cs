using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;

    private CinemachineBasicMultiChannelPerlin perlin = null;
    private float shakeTime = 0f;
    private bool isShake = false;

    private void Awake()
    {
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        EventManager<float, float>.Instance.StartListening(EventEnum.ShakeCamera, Shake);
    }

    private void OnDestroy()
    {
        EventManager<float, float>.Instance.StopListening(EventEnum.ShakeCamera, Shake);
    }

    public void Shake(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity;
        shakeTime = time;
        isShake = true;
    }

    void Update()
    {
        if (isShake == true)
        {
            shakeTime -= Time.deltaTime;

            if(shakeTime <= 0)
            {
                isShake = false;
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
