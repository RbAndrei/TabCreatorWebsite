using TabCreator.Models;

namespace TabCreator.Services.Interfaces
{
    public interface ITablatureService
    {
        void CreateTablature(Tablature tablature);

        void DeleteTablature(Tablature tablature);

        void UpdateTablature(Tablature tablature);

        Tablature GetTablatureById(int id);

        List<Tablature> GetTablatures();
    }
}

