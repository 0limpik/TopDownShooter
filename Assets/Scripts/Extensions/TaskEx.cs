using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TopDown.Scripts.Extensions
{
    internal static class TaskEx
    {
        public static async Task WaitPredicate(Func<bool> predicate)
        {
            predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

            var isPlaying = Application.isPlaying;

            while (predicate())
            {
                if (isPlaying != Application.isPlaying)
                    return;

                await Task.Yield();
            }
        }

        public static void Forget(this Task task)
        {
            task.ContinueWith(
                t => { Debug.LogException(t.Exception); },
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
