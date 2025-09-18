using System.Collections.Generic;
using RendezVous;

namespace RendezVous.Services
{
    public class RendezVousService : IRendezVousService
    {
        private static readonly List<RendezVous> _rendezVousList = new();
        public IEnumerable<RendezVous> GetAll()
        {
            return _rendezVousList;
        }

        public void Add(RendezVous rendezVous)
        {
            _rendezVousList.Add(rendezVous);
        }
    }
}
