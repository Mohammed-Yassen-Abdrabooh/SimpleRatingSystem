using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.BLL.Interfaces;
using RankingSystem.BLL.Repositories;
using RankingSystem.DAL.Models;
using RankingSystem.PL.ViewModels;

namespace RankingSystem.PL.Controllers
{
    [Authorize]
    //[AllowAnonymous] ==> ByDefault
    public class ItemController : Controller
    {
        private readonly IGenericRepository<Item> _itemRepo;
        private readonly IMapper _mapper;

        public ItemController(IGenericRepository<Item> ItemRepo , IMapper mapper)
        {
            _itemRepo = ItemRepo;
            _mapper = mapper;
        }

        //BaseUrl/Item/Index
        //Get All Items
        public async Task<IActionResult> Index()
        {
            var AllItems =await _itemRepo.GetAllAsync();
            var MappedItems = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(AllItems);
            return View(MappedItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel itemVM)
        {
            if (ModelState.IsValid)
            {
                var mappedItem = _mapper.Map<ItemViewModel, Item>(itemVM);
                int Result = _itemRepo.AddAsync(mappedItem);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(itemVM);
        }

    }
}
