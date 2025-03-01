namespace Code.Gameplay.Features.Statuses.Factory
{
    public interface IStatusFactory
    {
        GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId);
        GameEntity CreateFreezeStatus(StatusSetup setup, int producerId, int targetId);
    }
}