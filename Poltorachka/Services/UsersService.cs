using System.Collections.Generic;
using System.Linq;
using Poltorachka.Domain;
using Poltorachka.Models;

namespace Poltorachka.Services
{
    public interface IIndividualsService
    {
        ICollection<IndividualViewModel> Get();
    }

    public class UsersService : IIndividualsService
    {
        private readonly IIndividualsQuery _individualsQuery;

        public UsersService(IIndividualsQuery individualsQuery)
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
