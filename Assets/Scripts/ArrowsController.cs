using UnityEngine;
using DG.Tweening;
using System;

public class ArrowsController : MonoBehaviour
{
    [SerializeField] private RectTransform _hourArrow;
    [SerializeField] private RectTransform _minArrow;
    [SerializeField] private RectTransform _secArrow;

    private const int SEC_MIN_CLOCK_TICK = 60;
    private const int HOUR_CLOCK_TICK = 12;
    private const int MAX_ANGLE = 360;

    public void StartAnimation(DateTime time)
    {
        _secArrow.rotation = Quaternion.Euler(0, 0, -(float)time.Second/ SEC_MIN_CLOCK_TICK * MAX_ANGLE);
        _minArrow.rotation = Quaternion.Euler(0, 0, -(float)time.Minute/ SEC_MIN_CLOCK_TICK * MAX_ANGLE);
        _hourArrow.rotation = Quaternion.Euler(0, 0, -(float)time.Hour/ HOUR_CLOCK_TICK * MAX_ANGLE);
        _secArrow.DORotate(new Vector3(0, 0, -MAX_ANGLE), SEC_MIN_CLOCK_TICK, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
        _minArrow.DORotate(new Vector3(0, 0, -MAX_ANGLE), SEC_MIN_CLOCK_TICK*SEC_MIN_CLOCK_TICK, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
        _hourArrow.DORotate(new Vector3(0, 0, -MAX_ANGLE), SEC_MIN_CLOCK_TICK*SEC_MIN_CLOCK_TICK*HOUR_CLOCK_TICK, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void StopAndClearAnimation()
    {
        _secArrow.DOKill();
        _minArrow.DOKill();
        _hourArrow.DOKill();
    }
}
