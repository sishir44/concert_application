using ConcertBooking_Entities;
using ConcertBooking_Repository;
using ConcertBooking_Repository.Interfaces;
using ConcertBooking_UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConcertBooking.UI.Controllers
{
    public class ConcertsController : Controller
    {
        // To transfer data from controller to view we use three:
        // ViewBag - make dynamic variable, used for single HTTP request
        // ViewData - key value pair, used for single HTTP request
        // TempData - key value pair, used for multiple HTTP request
        private readonly IConcertRepo _concertRepo;
        private readonly IVenueRepo _venueRepo;
        private readonly IArtistRepo _artistRepo;
        private readonly IUtilityRepo _utilityRepo;
        private readonly IBookingRepo _bookingRepo;

        private string containerName = "ConcertImage";

        private string GetUserNameWithoutDomain(string email)
        {
            int atIndex = email.IndexOf('@'); // Find the index of '@' symbol
            if (atIndex != -1)
            {
                return email.Substring(0, atIndex); // If '@' symbol found, return the substring before '@'
            }
            else
            {
                return email; // If '@' symbol not found, return the original email
            }
        }

        public ConcertsController(IConcertRepo concertRepo, IVenueRepo venueRepo, IArtistRepo artistRepo, IUtilityRepo utilityRepo, IBookingRepo bookingRepo)
        {
            _concertRepo = concertRepo;
            _venueRepo = venueRepo;
            _artistRepo = artistRepo;
            _utilityRepo = utilityRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<ConcertViewModel> vm = new List<ConcertViewModel>();
            var concerts = await _concertRepo.GetAll();
            foreach (var concert in concerts)
            {
                vm.Add(new ConcertViewModel
                {
                    Id= concert.Id,
                    Name = concert.Name,
                    DateTime = concert.DateTime,
                    VenueName = concert.Venue.Name,
                    ArtistName = concert.Artist.Name
                });
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var artists = await _artistRepo.GetAll();
            var venues = await _venueRepo.GetAll();
            ViewBag.artistList = new SelectList(artists, "Id", "Name");
            ViewBag.venueList = new SelectList(venues, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateConcertViewModel vm)
        {
            var concert = new Concert
            {
                Name = vm.Name,
                Description = vm.Description,
                DateTime = vm.DateTime,
                VenueId = vm.VenueId,
                ArtistId = vm.ArtistId,
            };
            if(vm.ImageUrl != null)
            {
                concert.ImageUrl = await _utilityRepo.SaveImage(containerName, vm.ImageUrl);
            }
            await _concertRepo.Save(concert);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var concert = await _concertRepo.GetById(id);
            var artists = await _artistRepo.GetAll();
            var venues = await _venueRepo.GetAll();
            ViewBag.artistList = new SelectList(artists, "Id", "Name");
            ViewBag.venueList = new SelectList(venues, "Id", "Name");

            EditConcertViewModel vm = new EditConcertViewModel()
            {
                Id=concert.Id,
                Name = concert.Name,
                Description = concert.Description,
                DateTime = concert.DateTime,
                ImageUrl = concert.ImageUrl,
                VenueId = concert.VenueId,
                ArtistId = concert.ArtistId,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditConcertViewModel vm)
        {
            var concert = await _concertRepo.GetById(vm.Id);
            concert.Id = vm.Id;
            concert.Name = vm.Name;
            concert.Description = vm.Description;
            concert.DateTime = vm.DateTime;
            concert.ArtistId = vm.ArtistId;
            concert.VenueId = vm.VenueId;

            if(vm.ChooseImage != null)
            {
                concert.ImageUrl = await _utilityRepo.EditImage(containerName, vm.ChooseImage, concert.ImageUrl);
            }
            await _concertRepo.Edit(concert);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var state = await _concertRepo.GetById(id);
            await _concertRepo.RemoveData(state);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets(int id)
        {
            var bookings = await _bookingRepo.GetAll(id);
            var vm = bookings.Select(a => new DashboadViewModel
            {
                UserName = GetUserNameWithoutDomain(a.User.UserName),
                ConcertName = a.Concert.Name,
                SeatNumber = string.Join(",", a.Tickets.Select(t=>t.SeatNumber))
            }).ToList();
            return View(vm);
        }
    }
}
