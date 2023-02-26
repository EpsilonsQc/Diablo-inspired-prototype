using UnityEngine;

namespace HeroicFantasy.Algorithm
{
    public class EasingFunctions : MonoBehaviour
    {
        [SerializeField] private enum InterpolationType
        {
            Linear,
            EaseIn,
            EaseOut,
            EaseInOut,
            EaseOutBounce
        }

        [SerializeField] private enum MethodType
        {
            Default,
            Loop,
            PingPong
        }

        [Header("TYPE")]
        [SerializeField] private InterpolationType interpolationType = InterpolationType.Linear;
        [SerializeField] private MethodType methodType = MethodType.Loop;
        [Space(10)]

        [Header("OFFSET POSITION")]
        [SerializeField] private Vector3 offsetPosition;
        [Space(10)]

        [Header("MAX DURATION")]
        [SerializeField] private float maxDuration;
        [Space(10)]

        private Vector3 startPosition;
        private Vector3 endPosition;

        private float currentDuration = 0f;

        private void Awake()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            endPosition = startPosition + offsetPosition;

            UpdateInterpolation(interpolationType, methodType);
        }

        private void UpdateInterpolation(InterpolationType interpolationType, MethodType methodType)
        {
            Vector3 position;

            currentDuration += Time.deltaTime;
            float currentTime = UpdateTime();

            switch (interpolationType)
            {
                case InterpolationType.Linear:
                    position = Lerp(startPosition, endPosition, currentTime);
                    break;
                case InterpolationType.EaseIn:
                    position = EaseIn(startPosition, endPosition, currentTime);
                    break;
                case InterpolationType.EaseOut:
                    position = EaseOut(startPosition, endPosition, currentTime);
                    break;
                case InterpolationType.EaseInOut:
                    position = EaseInOut(startPosition, endPosition, currentTime);
                    break;
                case InterpolationType.EaseOutBounce:
                    position = EaseOutBounce(startPosition, endPosition, currentTime);
                    break;
                default:
                    position = Vector3.zero;
                    break;
            }

            if (currentTime <= 1f) 
            {
                transform.position = position;
            }
        }

        private float UpdateTime()
        {
            float time = currentDuration / maxDuration;

            switch (methodType)
            {
                case MethodType.Loop:
                    if (time >= 1f)
                    {
                        currentDuration = 0f;
                        time = 0f;
                    }
                    break;
                case MethodType.PingPong:
                    time = Mathf.Sin(time * Mathf.PI) / 2 + 0.5f;
                    break;
                default:
                    break;
            }
            return time;
        }

        #region INTERPOLATION Functions
        // Lerp (Float)
        private float Lerp(float start, float end, float t)
        {
            float result = start + (end - start) * t;
            return result;
        }

        // Lerp (Vector 2)
        private Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            Vector2 result = start + (end - start) * t;
            return result;
        }

        // Lerp (Vector 3)
        private Vector3 Lerp(Vector3 start, Vector3 end, float t)
        {
            Vector3 result = start + (end - start) * t;
            return result;
        }

        // Ease In (float)
        private float EaseIn(float start, float end, float t)
        {
            float result = start + (end - start) * t * t;
            return result;
        }

        // Ease In (Vector 3)
        private Vector3 EaseIn(Vector3 start, Vector3 end, float t)
        {
            Vector3 result = start + (end - start) * t * t;
            return result;
        }

        // Ease Out (float)
        private float EaseOut(float start, float end, float t)
        {
            float result = start - (end - start) * t * (t - 2);
            return result;
        }
        
        // East Out (Vector 3)
        private Vector3 EaseOut(Vector3 start, Vector3 end, float t)
        {
            Vector3 result = start - (end - start) * t * (t - 2);
            return result;
        }

        // Ease In Out (Float)
        private float EaseInOut(float start, float end, float t)
        {
            float result = start - (end - start) * (0.5f * Mathf.Cos(Mathf.PI * t) - 0.5f);
            return result;
        }

        // Ease In Out (Vector 3)
        private Vector3 EaseInOut(Vector3 start, Vector3 end, float t)
        {
            Vector3 result = start - (end - start) * (0.5f * Mathf.Cos(Mathf.PI * t) - 0.5f);
            return result;
        }

        // Ease Out Bounce
        private Vector3 EaseOutBounce(Vector3 start, Vector3 end, float x)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;
            float t;

            if (x < 1 / d1)
            {
                t = n1 * x * x;
            }
            else if (x < 2 / d1)
            {
                t = n1 * (x -= 1.5f / d1) * x + 0.75f;
            }
            else if (x < 2.5 / d1)
            {
                t = n1 * (x -= 2.25f / d1) * x + 0.9375f;
            }
            else
            {
                t = n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
            return start + (end - start) * t;
        }
        #endregion
    }
}