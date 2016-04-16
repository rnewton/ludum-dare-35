using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// How long the object should shake for.
	private float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount = 0.7f;
	private float decreaseFactor = 1.0f;

	private Transform camTransform;
	private Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null){
			camTransform = Camera.main.transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		} else {
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}

	public void Shake(float duration, float amount, float decFactor)
	{
		this.shakeDuration = duration;
		this.shakeAmount = amount;
		this.decreaseFactor = decFactor;
	}
}