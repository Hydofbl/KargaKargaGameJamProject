using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform camTransform;
    private Vector3 previousCamPos;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = camTransform.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].transform.position.z * -1;
        }
    }

    private void Update()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 parallax = (previousCamPos - camTransform.position) * parallaxScales[i];
            Vector3 parallaxObjectPosition = backgrounds[i].position;

            float backgroundTargetPosX = parallaxObjectPosition.x + parallax.x;
            float backgroundTargetPosY = parallaxObjectPosition.y + parallax.y;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, parallaxObjectPosition.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = camTransform.position;
    }
}
