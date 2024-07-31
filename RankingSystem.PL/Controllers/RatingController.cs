using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.BLL.Interfaces;
using RankingSystem.DAL.Models;
using RankingSystem.PL.ViewModels;

namespace RankingSystem.PL.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IGenericRepository<Item> _itemRepo;
        private readonly IGenericRepository<Rating> _RatingRepo;
        private readonly IMapper _mapper;

        public RatingController(IGenericRepository<Item> ItemRepo, IGenericRepository<Rating> RatingRepo,IMapper mapper)
        {
            _itemRepo = ItemRepo;
            _RatingRepo = RatingRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var AllRating = await _RatingRepo.GetAllAsync();
            var MappedRating = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingViewModel>>(AllRating);
            return View(MappedRating);
        }

        [HttpGet]
        public async Task<IActionResult> Rate(int id)
        {
            var item = await _itemRepo.GetByIdAsync(id);
            var mappedItem = new RatingViewModel()
            {
                ItemId = item.id,
                Item = item,
            };
            return View(mappedItem);
        }

        [HttpPost]
        public async Task<IActionResult> Rate(RatingViewModel RatingVM)
        {
            if (ModelState.IsValid)
            {
                var mappedItem = _mapper.Map<RatingViewModel, Rating>(RatingVM);
                int Result = _RatingRepo.AddAsync(mappedItem);
                if (Result > 0)
                    return RedirectToAction(nameof(Index),nameof(Item));
            }
            return View(RatingVM);
        }



    }
}
