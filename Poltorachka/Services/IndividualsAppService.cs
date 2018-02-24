using System.Collections.Generic;
using System.Linq;
using Poltorachka.Domain;
using Poltorachka.Domain.Individuals;
using Poltorachka.Models;

namespace Poltorachka.Services
{
    public interface IIndividualsAppService
    {
        ICollection<IndividualViewModel> Get();
    }

    public class IndividualsAppService : IIndividualsAppService
    {
        private readonly IIndividualsQuery _individualsQuery;

        public IndividualsAppService(IIndividualsQuery individualsQuery)
        {
            _individualsQuery = individualsQuery;
        }

        public ICollection<IndividualViewModel> Get()
        {
            return _individualsQuery.Execute()
                .Select(i => new IndividualViewModel { IndId = i.IndId, Name = i.Name })
                .ToList();
        }
    }
}
