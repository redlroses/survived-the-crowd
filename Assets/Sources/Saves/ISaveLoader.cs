using Sources.Data;

namespace Sources.Saves
{
    public interface ISaveLoader
    {
        public void Save(PlayerProgress progress);
        public PlayerProgress Load();
    }
}