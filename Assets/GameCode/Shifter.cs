using UnityEngine;

namespace GameCode
{
    public class Shifter : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private float time;

        private float min;
        private float max;

        private void Awake()
        {
            min = transform.localPosition.x - range;
            max = transform.localPosition.x + range;
            transform.localPosition = new Vector3(
                range,
                transform.localPosition.y,
                transform.localPosition.z
            );
        }

        private void Update()
        {
            var timer = Time.time;
            var remain = timer % (time / 2);
            var distance = remain / (time / 2) * (range * 2);
            var moveRight = timer % time < time / 2;
            var x = moveRight ? min + distance : max - distance;

            transform.localPosition = new Vector3(
                x,
                transform.localPosition.y,
                transform.localPosition.z
            );
        }
    }
}
