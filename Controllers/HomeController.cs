﻿using BlogMVC.Data;
using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        // injections
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
                                ApplicationDbContext context)
        {
            // assign injected values
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // to read in blog posts from db
            List<BlogPost> model = await _context.BlogPosts
                                                 .Include(b => b.Comments)
                                                 .Include (b => b.Category)
                                                 .OrderByDescending(b => b.DateCreated)
                                                 .ToListAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}