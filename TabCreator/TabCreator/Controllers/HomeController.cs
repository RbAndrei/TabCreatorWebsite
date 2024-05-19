using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TabCreator.Models;
using TabCreator.Services.Interfaces;

namespace TabCreator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IChordsService _chordsService;
        private readonly ISheetService _sheetService;
        private readonly ITablatureService _tablatureService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IUserService userService,
                              IChordsService chordsService, 
                              ISheetService sheetService,
                              ITablatureService tablatureService,
                              IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _chordsService = chordsService;
            _sheetService = sheetService;
            _tablatureService = tablatureService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int? tablatureId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tablatures = _tablatureService.GetTablaturesByUserId(userId);

            /*foreach(var tab in tablatures)
            {
                Debug.WriteLine($"(Index) TablatureId: {tab.TablatureId}");
				Debug.WriteLine($"(Index) TablatureName: {tab.TablatureName}");
				Debug.WriteLine($"(Index) UserTab: {tab.UserTab}");
			}
*/
            if (tablatureId != null)
            {
                Debug.WriteLine($"(Index) TablatureId: {tablatureId}");
                var currentTablature = _tablatureService.GetTablatureById((int)tablatureId);

                if (currentTablature != null)
                {

                    ViewBag.TablatureContent = currentTablature.UserTab;
                    ViewBag.TablatureId = tablatureId;
                }
            }

			ViewBag.Tablatures = tablatures;

			return View();
        }

        [HttpPost]
        public IActionResult CreateTab([Bind("TablatureName,UserTab")] Tablature tablature)
        {
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				Debug.WriteLine("(CreateTab) Error: User ID is null or empty.");
				return RedirectToAction(nameof(Index));
			}

			// Set the UserId to the currently logged-in user's ID
			tablature.UserId = userId;

			Debug.WriteLine($"(CreateTab) TablatureName: {tablature.TablatureName}");
			Debug.WriteLine($"(CreateTab) UserId: {tablature.UserId}");
			Debug.WriteLine($"(CreateTab) UserTab: {tablature.UserTab}");

			ModelState.Clear();
			TryValidateModel(tablature);

			if (ModelState.IsValid)
            {
                try
                {
					_tablatureService.CreateTablature(tablature);
					Debug.WriteLine("(CreateTab) Tablature created successfully.");
				}
                catch (DbUpdateConcurrencyException ex)
                {
					Debug.WriteLine($"(CreateTab) DbUpdateConcurrencyException: {ex.Message}");
					return RedirectToAction(nameof(Index));
                }
				catch (Exception ex)
				{
					// Log any other exception
					Debug.WriteLine($"(CreateTab) Exception: {ex.Message}");
					return RedirectToAction(nameof(Index));
				}
				return RedirectToAction(nameof(Index));
            }
			else
			{
				// Log ModelState errors
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					Debug.WriteLine($"ModelState Error: {error.ErrorMessage}");
				}
			}

			// If we reach here, something went wrong
			return RedirectToAction(nameof(Index));
		}

        [HttpPost]
        public IActionResult OpenTab([Bind("TablatureId,TablatureName")] Tablature tablature)
        {

            if(tablature.TablatureId == null)
            {
                return RedirectToAction(nameof(Index));
            }

			Debug.WriteLine($"(OpenTab) TablatureId: {tablature.TablatureId}");
			Debug.WriteLine($"(OpenTab) TablatureName: {tablature.TablatureName}");
        
            return RedirectToAction("Index", new { tablatureId = tablature.TablatureId });

		}

		[HttpPost]
		public IActionResult DeleteTab([Bind("TablatureId")] Tablature tablature)
        {
            tablature = _tablatureService.GetTablatureById(tablature.TablatureId);

            _tablatureService.DeleteTablature(tablature);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult SheetMusic()
        {
            return View();
        }

        public IActionResult Arrangements()
        {
            return View();
        }

        public IActionResult FretboardEditor()
        {
            return View();
        }

        public IActionResult HowTo()
        {
            return View();
        }
        public IActionResult Contact()
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
