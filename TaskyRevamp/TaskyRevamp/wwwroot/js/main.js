// tooltip initialization
document.addEventListener("DOMContentLoaded", function () {
  const tooltipTriggerList = document.querySelectorAll(
    '[data-bs-toggle="tooltip"]'
  );
  tooltipTriggerList.forEach(function (tooltipTriggerEl) {
    new bootstrap.Tooltip(tooltipTriggerEl);
  });
});
// === Navigation Active Link Handler ===
// This script stores the last clicked navigation link using localStorage,
// and re-applies the 'active' class on page load to preserve UI state across pages.
document.addEventListener("DOMContentLoaded", function () {
  // Store the ID of the clicked nav-link into localStorage
  document.querySelectorAll(".nav-link").forEach((link) => {
    link.addEventListener("pointerdown", () => {
      const id = link.getAttribute("data-id");
      localStorage.setItem("activeNav", id);
    });
  });

  // On page load: apply 'active' class to the matching nav-link based on stored ID
  const activeId = localStorage.getItem("activeNav");
  if (activeId) {
    document.querySelectorAll(".nav-link").forEach((link) => {
      if (link.getAttribute("data-id") === activeId) {
        link.classList.add("active");
      } else {
        link.classList.remove("active");
      }
    });
  }
});


window.blazorCulture = {
    get: function () {
        return localStorage.getItem('BlazorCulture');
    },
    set: function (value) {
        localStorage.setItem('BlazorCulture', value);
    }
};

window.setDocumentDirection = function (dir) {
    document.documentElement.setAttribute("dir", dir);
};