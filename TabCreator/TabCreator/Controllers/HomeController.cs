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

            ViewBag.UserId = userId;

			var tablatures = _tablatureService.GetTablaturesByUserId(userId);

            if (tablatureId != null)
            {
                Debug.WriteLine($"(Index) TablatureId: {tablatureId}");
                var currentTablature = _tablatureService.GetTablatureById((int)tablatureId);

                if (currentTablature != null)
                {
                    ViewBag.TablatureContent = currentTablature.UserTab;
					ViewBag.TablatureName = currentTablature.TablatureName;
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

        public IActionResult FretboardEditor(int? chordId)
        {
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			ViewBag.UserId = userId;

			var chords = _chordsService.GetChordsByUserId(userId);

			if (chordId != null)
			{
				Debug.WriteLine($"(FretboardEditor) ChordId: {chordId}");
				var currentChord = _chordsService.GetChordById((int)chordId);

				if (currentChord != null)
				{
					ViewBag.ChordContent = currentChord.UserChord;
					ViewBag.ChordId = chordId;
				}
			}

			ViewBag.Chords = chords;

            return View();
		}

		[HttpPost]
        public IActionResult CreateChord([Bind("ChordName,UserChord")] Chords chord)
        {
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				Debug.WriteLine("(CreateChord) Error: User ID is null or empty.");
				return RedirectToAction(nameof(FretboardEditor));
			}

			// Set the UserId to the currently logged-in user's ID
			chord.UserId = userId;

			Debug.WriteLine($"(CreateChord) ChordName: {chord.ChordName}");
			Debug.WriteLine($"(CreateChord) UserId: {chord.UserId}");
			Debug.WriteLine($"(CreateChord) UserChord: {chord.UserChord}");

			ModelState.Clear();
			TryValidateModel(chord);

			if (ModelState.IsValid)
			{
				try
				{
					_chordsService.CreateChord(chord);
					Debug.WriteLine("(CreateChord) Chord created successfully.");
				}
				catch (DbUpdateConcurrencyException ex)
				{
					Debug.WriteLine($"(CreateChord) DbUpdateConcurrencyException: {ex.Message}");
					return RedirectToAction(nameof(FretboardEditor));
				}
				catch (Exception ex)
				{
					// Log any other exception
					Debug.WriteLine($"(CreateChord) Exception: {ex.Message}");
					return RedirectToAction(nameof(FretboardEditor));
				}
				return RedirectToAction(nameof(FretboardEditor));
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
			return RedirectToAction(nameof(FretboardEditor));
		}

		[HttpPost]
		public IActionResult OpenChord([Bind("ChordsId,ChordName")] Chords chord)
		{
			if (chord.ChordsId == null)
			{
				return RedirectToAction(nameof(FretboardEditor));
			}

			Debug.WriteLine($"(OpenChord) ChordId: {chord.ChordsId}");
			Debug.WriteLine($"(OpenChord) ChordName: {chord.ChordName}");

			return RedirectToAction("FretboardEditor", new { chordId = chord.ChordsId });
		}

		[HttpPost]
		public IActionResult DeleteChord([Bind("ChordsId")] Chords chord)
		{
			Debug.WriteLine($"(Delete Chord) ChordsId: {chord.ChordsId}");

			chord = _chordsService.GetChordById(chord.ChordsId);

			_chordsService.DeleteChord(chord);

			return RedirectToAction(nameof(FretboardEditor));
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
