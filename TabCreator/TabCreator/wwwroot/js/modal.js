// Get the modal
var modalSave = document.getElementById("save-modal");
var modalOpen = document.getElementById("open-modal");
var modalDelete = document.getElementById("delete-modal");

// Get the button that opens the modal
var btnSave = document.getElementById("save-btn");
var btnOpen = document.getElementById("open-btn");
var btnDelete = document.getElementById("delete-btn");

// Get the <span> element that closes the modal
var spans = document.getElementsByClassName("close");

// When the user clicks the button, open the modal
btnSave.onclick = function () {
	modalSave.style.display = "flex";
}

btnOpen.onclick = function () {
	modalOpen.style.display = "flex";
}

btnDelete.onclick = function () {
	modalDelete.style.display = "flex";
}

// When the user clicks on <span> (x), close the modal
for (span of spans) { 
	span.onclick = function () {
		modalSave.style.display = "none";
		modalOpen.style.display = "none";
		modalDelete.style.display = "none";
	}
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
	if (event.target == modalSave) {
		modalSave.style.display = "none";
	}
	if (event.target == modalOpen) {
		modalOpen.style.display = "none";
	}
	if (event.target == modalDelete) {
		modalDelete.style.display = "none";
	}
}