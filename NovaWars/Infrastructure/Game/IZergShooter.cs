using NovaWars.Model.Zergs;

namespace NovaWars.Infrastructure.Game
{
    public interface IZergShooter
    {
        void SplitString(string text);

        IZerg CreateNewZerg();
    }
}
