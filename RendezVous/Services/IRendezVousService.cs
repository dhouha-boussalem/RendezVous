using System.Collections.Generic;
using RendezVous;

namespace RendezVous.Services
{
    public interface IRendezVousService
    {
        IEnumerable<RendezVous> GetAll();
        void Add(RendezVous rendezVous);
    }
}
