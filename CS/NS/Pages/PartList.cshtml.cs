using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NS.Data;
using NS.Models;

namespace NS.Pages
{
    public class PartListModel : PageModel
    {
        private readonly NS.Data.ApplicationDbContext _context;

        public PartListModel(NS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Part> Part { get;set; }

        public async Task OnGetAsync()
        {
            Part = await _context.Part.ToListAsync();
        }
    }
}
