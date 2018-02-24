using System.Collections.Generic;
using System.Linq;
using Poltorachka.Domain;
using Poltorachka.Models;

namespace Poltorachka.Services
{
    public interface IIndividualsService
    {
        ICollection<IndividualModel> Get();
    }

    public class UsersService : IIndividualsService
    {
        private readonly IIndividualsQuery _individualsQuery;

        public UsersService(IIndividualsQuery individualsQuery)
        {
            _individualsQuery = individualsQuery;
        }

        public ICollection<IndividualModel> Get()
        {
            return _individualsQuery.Execute()
                .Select(i => new IndividualModel { IndId = i.IndId, Name = i.Name })
                .ToList();
        }
    }
}
