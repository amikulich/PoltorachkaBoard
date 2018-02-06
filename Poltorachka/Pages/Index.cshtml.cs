using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poltorachka.Domain;

namespace Poltorachka.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFactRepository repository;

        private readonly IIndividualsQuery individualsQuery;

        public IndexModel(IFactRepository repository, IIndividualsQuery individualsQuery)
        {
            this.repository = repository;
            this.individualsQuery = individualsQuery;
        }

        public string Title { get; private set; } = $"Today is {DateTime.UtcNow.ToShortDateString()}";

        public IEnumerable<Fact> Facts { get; private set; }

        public void OnGet()
        {
            Facts = repository.Get().OrderByDescending(f => f.Date);
        }

        public PageResult OnPostApproveFact(int factId)
        {
            var fact = repository.GetById(factId);

            var userName = individualsQuery.Execute().Single(u => User.Claims.Single(c => c.Type == "name").Value == u.Email).Name;

            fact.Approve(userName);

            repository.Save(fact);

            return Page(); 
        }
    }
}
