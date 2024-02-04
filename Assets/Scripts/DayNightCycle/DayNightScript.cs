using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace DayNightScript
{
    [RequireComponent(typeof(Light2D))]
    public class DayNightScript : MonoBehaviour
    {
        public float duration = 5f;

        [SerializeField] private Gradient time;
        private Light2D light;
        private float startTime;
        // Start is called before the first frame update
        private void Start()
        {
            light = GetComponent<Light2D>();
            startTime = Time.time;
        }

        // Update is called once per frame
        private void Update()
        {
            float timeElapsed = Time.time - startTime;
            float percent = Mathf.Sin(f: timeElapsed / duration * Mathf.PI * 2) * 0.5f + 0.5f;
            percent = Mathf.Clamp01(percent);

            light.color = time.Evaluate(percent);

        }
    }
}

