﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eazorCRUD.Data;
using eazorCRUD.Model;

namespace eazorCRUD.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly eazorCRUD.Data.eazorCRUDContext _context;

        public IndexModel(eazorCRUD.Data.eazorCRUDContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
    }
}
