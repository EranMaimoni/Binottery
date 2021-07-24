using Binottery.Model;

namespace Binottery.Utilities.Serializer
{
    public interface IGameSerializer
    {
        State Load();

        void Save(State state);

        bool HasGameSaved();
    }
}