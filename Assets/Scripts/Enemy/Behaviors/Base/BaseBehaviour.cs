using System;
using System.Threading.Tasks;
using TopDown.Scripts.Extensions;

namespace TopDown.Scripts.Enemy.Behaviors.Base
{
    [Serializable]
    public abstract class BaseBehaviour : IBehaviour
    {
        public bool running { get; private set; }

        public async Task Run()
        {
            if (running)
                throw new InvalidOperationException();

            running = true;
            await OnRun();
            await Wait();
        }

        protected abstract Task OnRun();

        public abstract void Update(float deltaTime);

        public void Stop()
        {
            running = false;
            OnStop();
        }

        protected abstract void OnStop();

        public Task Wait()
            => TaskEx.WaitPredicate(() => running);
    }
}
