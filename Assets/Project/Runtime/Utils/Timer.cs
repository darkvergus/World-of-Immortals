using System;
using Managers;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        private readonly Action onComplete;
        private readonly Action<float> onUpdate;
        private float startTime;
        private float lastUpdateTime;

        private float? timeElapsedBeforeCancel;
        private float? timeElapsedBeforePause;

        private readonly MonoBehaviour autoDestroyOwner;
        private readonly bool hasAutoDestroyOwner;

        public float Duration { get; private set; }

        public bool IsLooped { get; set; }
        public bool IsCompleted { get; private set; }
        public bool UsesRealTime { get; private set; }
        public bool IsPaused => timeElapsedBeforePause.HasValue;
        public bool IsCancelled => timeElapsedBeforeCancel.HasValue;

        private bool IsOwnerDestroyed => hasAutoDestroyOwner && autoDestroyOwner == null;
        public bool IsDone => IsCompleted || IsCancelled || IsOwnerDestroyed;

        public static Timer Register(float duration, Action onComplete, Action<float> onUpdate, bool isLooped, bool useRealTime, MonoBehaviour autoDestroyOwner)
        {
            Timer timer = new Timer(duration, onComplete, onUpdate, isLooped, useRealTime, autoDestroyOwner);
            TimerManager.Instance.RegisterTimer(timer);
            return timer;
        }

        public static Timer Register(float duration, Action onComplete, Action<float> onUpdate, bool isLooped, bool useRealTime) => Register(duration, onComplete, onUpdate, isLooped, useRealTime, null);
        public static Timer Register(float duration, Action onComplete, Action<float> onUpdate, bool isLooped) => Register(duration, onComplete, onUpdate, isLooped, false, null);
        public static Timer Register(float duration, Action onComplete, bool isLooped) => Register(duration, onComplete, null, isLooped, false, null);
        public static Timer Register(float duration, Action onComplete) => Register(duration, onComplete, null, false, false, null);

        public static void Cancel(Timer timer) => timer?.Cancel();

        public void Cancel()
        {
            if (IsDone)
            {
                return;
            }

            timeElapsedBeforeCancel = GetTimeElapsed();
            timeElapsedBeforePause = null;
        }

        public static void Pause(Timer timer) => timer?.Pause();

        public void Pause()
        {
            if (IsPaused || IsDone)
            {
                return;
            }

            timeElapsedBeforePause = GetTimeElapsed();
        }

        public static void Resume(Timer timer) => timer?.Resume();

        public void Resume()
        {
            if (!IsPaused || IsDone)
            {
                return;
            }

            timeElapsedBeforePause = null;
        }

        public static void CancelAllRegisteredTimers()
        {
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.CancelAllTimers();
            }
        }

        public static void PauseAllRegisteredTimers()
        {
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.PauseAllTimers();
            }
        }

        public static void ResumeAllRegisteredTimers()
        {
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.ResumeAllTimers();
            }
        }

        public float GetTimeElapsed() => IsCompleted || GetWorldTime() >= GetFireTime()
                ? Duration
                : timeElapsedBeforeCancel ?? timeElapsedBeforePause ?? GetWorldTime() - startTime;

        public float GetTimeRemaining() => Duration - GetTimeElapsed();
        public float GetRatioComplete() => GetTimeElapsed() / Duration;
        public float GetRatioRemaining() => GetTimeRemaining() / Duration;

        private Timer(float duration, Action onComplete, Action<float> onUpdate, bool isLooped, bool usesRealTime, MonoBehaviour autoDestroyOwner)
        {
            Duration = duration;
            this.onComplete = onComplete;
            this.onUpdate = onUpdate;

            IsLooped = isLooped;
            UsesRealTime = usesRealTime;

            this.autoDestroyOwner = autoDestroyOwner;
            hasAutoDestroyOwner = autoDestroyOwner != null;

            startTime = GetWorldTime();
            lastUpdateTime = startTime;
        }

        private float GetWorldTime() => UsesRealTime ? Time.realtimeSinceStartup : Time.time;

        private float GetFireTime() => startTime + Duration;

        private float GetTimeDelta() => GetWorldTime() - lastUpdateTime;

        public void Update()
        {
            if (IsDone)
            {
                return;
            }

            if (IsPaused)
            {
                startTime += GetTimeDelta();
                lastUpdateTime = GetWorldTime();
                return;
            }

            lastUpdateTime = GetWorldTime();

            onUpdate?.Invoke(GetTimeElapsed());

            if (GetWorldTime() >= GetFireTime())
            {
                onComplete?.Invoke();

                if (IsLooped)
                {
                    startTime = GetWorldTime();
                }
                else
                {
                    IsCompleted = true;
                }
            }
        }
    }
}