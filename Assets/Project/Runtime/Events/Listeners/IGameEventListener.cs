namespace Events
{
    public interface IGameEventListener<in T>
    {
        void OnEventRaised(T item);
    }
}
