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

        public IndexModel(IFactRepository repository)
        {
            this.repository = repository;
        }

        public string Title { get; private set; } = $"Today is {DateTime.UtcNow.ToShortDateString()}";

        public IEnumerable<FactReadModel> Facts { get; private set; }

        public void OnGet()
        {
            Facts = repository.Get().OrderByDescending(f => f.Date);
        }
    }
}
