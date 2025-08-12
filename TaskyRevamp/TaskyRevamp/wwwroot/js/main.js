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




function OpenModel(modalRef) {
    if (modalRef?.classList == null || modalRef.classList.contains('show')) {
        return;
    }
    const models = document.querySelectorAll('.modal.show');
    let zIndex = 0;
    if (models.length > 0) {
        zIndex = parseInt(document.defaultView.getComputedStyle(models[models.length - 1], null).zIndex, 10);
        modalRef.style.zIndex = zIndex + 2;
    }
    bootstrap.Modal.getOrCreateInstance(modalRef).show();
    if (zIndex) {
        const backdrops = document.querySelectorAll('.modal-backdrop.show');
        backdrops[backdrops.length - 1].style.zIndex = zIndex + 1;
    }
}

window.DotNetHelper = {
    helpers: {},

    setDotNetHelper: function (dotNetHelper, id) {
        this.helpers[id] = dotNetHelper;
    },

    invoke: function (id, methodName, ...args) {
        if (this.helpers[id]) {
            this.helpers[id].invokeMethodAsync(methodName, ...args);
        } else {
            console.warn(`DotNetHelper with ID ${id} not found`);
        }
    }
};

function AddCloseHandeling(element, elementKey) {
    element.addEventListener("hidden.bs.modal", (event) => {
        DotNetHelper.dotNetHelper[elementKey].invokeMethodAsync("CloseModal");
    });
}

function AddCloseHandelingSideBar(element, elementKey) {
    element.addEventListener("hidden.bs.offcanvas", (event) => {
        DotNetHelper.dotNetHelper[elementKey].invokeMethodAsync(
            "CloseSideBarBlazor"
        );
        event.stopPropagation();
    });
}

function CloseModel(modalRef) {
    if (modalRef?.classList == null) {
        return;
    }
    setTimeout(function () {
        var modalInstance = bootstrap.Modal.getInstance(modalRef);
        if (modalInstance == null) {
            return;
        }
        modalInstance.hide();
    }, 500);
}

function OpenSideBar(modalRef, id) {
    if (modalRef?.classList == null || modalRef.classList.contains("show")) {
        return;
    }
    var bsOffcanvas = bootstrap.Offcanvas.getOrCreateInstance(modalRef);
    DotNetHelper.popups[id] = bsOffcanvas;
    bsOffcanvas.show();
}