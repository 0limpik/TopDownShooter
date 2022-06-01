using System.Threading.Tasks;

namespace TopDown.Scripts.Enemy.Behaviors.Base
{
    internal interface IBehaviour
    {
        Task Run();
        void Update(float deltaTime);
        void Stop();
    }
}
