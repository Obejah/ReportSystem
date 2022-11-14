using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReportSystem.DB;
using ReportSystem.Models;
using ReportSystem.Repositories;
using System;

namespace ReportSystem.Pages
{
    public class MeldingenOverview : PageModel
    {
        private readonly CrudRepository _crudRepository;
        private readonly UserRepository _userRepository;
        public MeldingenOverview(AuthDbContext context, CrudRepository crudRepository, UserRepository userRepository)
        {
            _crudRepository = crudRepository;
            _userRepository = userRepository;
            _Options = crudRepository.ReadAllRows<Option>().Result;
            _Points = crudRepository.ReadAllRows<Point>().Result;
            _Notices = crudRepository.ReadAllRows<Notice>().Result;
        }
       

        [BindProperty] public Notice? Notice { get; set; }

        public List<Option>? _Options { get;set; }
        public List<Notice>? _Notices { get;set; }
        public List<Point>? _Points { get; set; }

        public async Task<IActionResult> OnPostDelteMelding()
        {
            if(_Notices != null)
            {
                var notice = _Notices.FirstOrDefault(x => Notice != null && x.NoticeId == Notice.NoticeId);
                await _crudRepository.RemoveRow(notice);
            }
            return RedirectToPage("/Melding/MeldingenOverview");
        }
        
    }
}
