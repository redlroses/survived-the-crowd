using Sources.Data;

namespace Sources.Saves
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}