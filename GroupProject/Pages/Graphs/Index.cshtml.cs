using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupProject.Data;
using GroupProject.Entity;

namespace GroupProject.Pages.Graphs
{
    public class IndexModel : PageModel
    {
        private readonly GroupProject.Data.ISPContext _context;

        public IndexModel(GroupProject.Data.ISPContext context)
        {
            _context = context;
        }

        public IList<Subscription> Subscription { get;set; }

        public IList<Package> Package { get;set; }

        public IList<Connection> Connection { get;set; }

        public async Task OnGetAsync()
        {
            Package = await _context.Packages.ToListAsync();
            Connection = await _context.Connections.ToListAsync();

            Subscription = await _context.Subscriptions
                .Include(s => s.Client)       
                .Include(s => s.Package).ToListAsync();       
        
            var conn = from c in Connection select c;
            var pack = from p in Package select p;
        }
    }
}
