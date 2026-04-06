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


window.authListener = {
    register: function (dotNetRef) {
        window.addEventListener("storage", function (event) {
            if (event.key === "bearerToken") {
                // LOGOUT
                if (event.newValue === null) {
                    dotNetRef.invokeMethodAsync("ForceLogout");
                }
                // LOGIN (token added or changed)
                if (event.oldValue === null && event.newValue !== null) {
                    dotNetRef.invokeMethodAsync("OnLoginDetected");
                }
            }
        });
    }
};

window.navigationTracker = {
    saveCurrentUrl: function () {
        sessionStorage.setItem(
            "returnUrl",
            window.location.pathname + window.location.search
        );
    },
    getReturnUrl: function () {
        return sessionStorage.getItem("returnUrl");
    },
    clearReturnUrl: function () {
        sessionStorage.removeItem("returnUrl");
    }
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
    dotNetHelper: {},

    setDotNetHelper: function (dotNetHelper, id) {
        this.dotNetHelper[id] = dotNetHelper;
    },

    invoke: function (id, methodName, ...args) {
        if (this.dotNetHelper[id]) {
            this.dotNetHelper[id].invokeMethodAsync(methodName, ...args);
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

window.setFavicon = (dataUrl) => {
    // Remove all existing favicons
    document.querySelectorAll("link[rel~='icon']").forEach(link => link.parentNode.removeChild(link));

    // Create new favicon link
    const link = document.createElement("link");
    link.rel = "icon";

    // Detect image type from the data URL
    if (dataUrl.startsWith("data:image/svg+xml")) {
        link.type = "image/svg+xml";
    } else if (dataUrl.startsWith("data:image/png")) {
        link.type = "image/png";
    } else if (dataUrl.startsWith("data:image/jpeg")) {
        link.type = "image/jpeg";
    } else if (dataUrl.startsWith("data:image/x-icon")) {
        link.type = "image/x-icon";
    } else {
        console.warn("Unknown favicon format. Defaulting to image/png.");
        link.type = "image/png";
    }

    // Bust cache
    link.href = dataUrl + "?v=" + new Date().getTime();

    document.head.appendChild(link);
};

window.setThemeColor = (variable, value) => {
    document.documentElement.style.setProperty(variable, value);
};

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const buffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([buffer], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement("a");
    anchor.href = url;
    anchor.download = fileName ?? "file.xlsx";
    anchor.click();
    URL.revokeObjectURL(url);
};

window.saveFileFromBytes = (fileName, bytesBase64) => {
    const link = document.createElement('a');
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

window.previewFileFromBytes = (fileName, base64Content, contentType) => {
    const byteCharacters = atob(base64Content);
    const byteArray = Uint8Array.from(byteCharacters, c => c.charCodeAt(0));
    const blob = new Blob([byteArray], { type: contentType });

    if (contentType === "text/csv") {
        const reader = new FileReader();
        reader.onload = function () {
            const csv = reader.result;
            const rows = csv.split("\n").map(r => r.split(","));

            let table = "<table border='1' style='border-collapse:collapse;width:100%'>";
            rows.forEach(row => {
                table += "<tr>";
                row.forEach(cell => {
                    table += `<td style="padding:6px">${cell}</td>`;
                });
                table += "</tr>";
            });
            table += "</table>";
            const win = window.open("", "_blank");
            win.document.write(`
                <html>
                <head><title>${fileName}</title></head>
                <body>${table}</body>
                </html>
            `);
            win.document.close();
        };
        reader.readAsText(blob);
        return;
    }
    const blobUrl = URL.createObjectURL(blob);
    window.open(blobUrl, "_blank");
};

window.fileService = {
    initDropZone: function (dropZoneId, inputFileId) {
        const dropZone = document.getElementById(dropZoneId);
        const inputFile = document.getElementById(inputFileId);
        if (!dropZone || !inputFile) {
            console.error('Drop zone or input file not found');
            return;
        }
        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
            dropZone.addEventListener(eventName, function (e) {
                e.preventDefault();
                e.stopPropagation();
            }, false);
        });
        dropZone.addEventListener('dragenter', function () {
            dropZone.classList.add('border-primary', 'bg-light');
        });
        dropZone.addEventListener('dragover', function () {
            dropZone.classList.add('border-primary', 'bg-light');
        });
        dropZone.addEventListener('dragleave', function (e) {
            if (!dropZone.contains(e.relatedTarget)) {
                dropZone.classList.remove('border-primary', 'bg-light');
            }
        });
        dropZone.addEventListener('drop', function (e) {
            dropZone.classList.remove('border-primary', 'bg-light');
            const files = e.dataTransfer.files;
            if (files && files.length > 0) {
                const dataTransfer = new DataTransfer();
                for (let i = 0; i < files.length; i++) {
                    dataTransfer.items.add(files[i]);
                }
                inputFile.files = dataTransfer.files;
                const event = new Event('change', { bubbles: true });
                inputFile.dispatchEvent(event);
            }
        });
    }
};

window.initDropdowns = function () {
    // Remove all previous listeners
    document.removeEventListener('click', window._dropdownClickHandler, true);
    document.removeEventListener('scroll', window._dropdownScrollHandler, true);
    document.removeEventListener('keydown', window._dropdownKeyHandler, true);
    // Create a single shared menu container on body
    let activeToggle = null;
    function getOrCreateMenu(toggle) {
        const parent = toggle.closest('.dropdown');
        if (!parent) return null;
        const menu = parent.querySelector('.dropdown-menu');
        return menu || null;
    }
    function positionMenu(toggle, menu) {
        const rect = toggle.getBoundingClientRect();
        const menuHeight = menu.offsetHeight || 200; 
        const menuWidth = menu.offsetWidth || 180;
        const viewportHeight = window.innerHeight;
        const viewportWidth = window.innerWidth;
        // Move to body temporarily to measure
        if (menu.parentElement !== document.body) {
            document.body.appendChild(menu);
        }
        menu.style.position = 'fixed';
        menu.style.zIndex = '99999';
        menu.style.display = 'block';
        menu.style.minWidth = '160px';
        // Recalculate after appending to body
        const actualMenuHeight = menu.offsetHeight;
        const actualMenuWidth = menu.offsetWidth;
        // Decide vertical: open down or up
        const spaceBelow = viewportHeight - rect.bottom;
        const spaceAbove = rect.top;
        let top;
        if (spaceBelow >= actualMenuHeight || spaceBelow >= spaceAbove) {
            top = rect.bottom + 2;
        }
        else {
            top = rect.top - actualMenuHeight - 2;
        }
        // Decide horizontal: align to right edge of button, but don't overflow left
        let left = rect.right - actualMenuWidth;
        if (left < 8) left = 8;
        if (left + actualMenuWidth > viewportWidth - 8) {
            left = viewportWidth - actualMenuWidth - 8;
        }
        menu.style.top = top + 'px';
        menu.style.left = left + 'px';
    }
    function closeActive() {
        if (activeToggle) {
            const menu = document.querySelector('.dropdown-menu.manually-shown');
            if (menu) {
                menu.classList.remove('manually-shown', 'show');
                menu.style.display = '';
                menu.style.position = '';
                menu.style.top = '';
                menu.style.left = '';
                menu.style.zIndex = '';
                // Move menu back to its original parent
                if (menu._originalParent && menu.parentElement === document.body) {
                    menu._originalParent.appendChild(menu);
                }
            }
            activeToggle.setAttribute('aria-expanded', 'false');
            activeToggle = null;
        }
    }
    window._dropdownClickHandler = function (e) {
        const toggle = e.target.closest('.dropdown-actions-toggle');
        if (toggle) {
            e.stopPropagation();
            e.preventDefault();
            // Clicking the same toggle → close
            if (activeToggle === toggle) {
                closeActive();
                return;
            }
            closeActive();
            const menu = getOrCreateMenu(toggle);
            if (!menu) return;
            // Remember original parent
            menu._originalParent = toggle.parentElement;
            activeToggle = toggle;
            toggle.setAttribute('aria-expanded', 'true');
            menu.classList.add('manually-shown', 'show');
            positionMenu(toggle, menu);
            return;
        }
        // Click outside → close
        const openMenu = document.querySelector('.dropdown-menu.manually-shown');
        if (openMenu) {
            if (openMenu.contains(e.target)) {
                closeActive();
                return;
            }
            closeActive();
        }
    };
    window._dropdownScrollHandler = function () {
        if (activeToggle) {
            const menu = document.querySelector('.dropdown-menu.manually-shown');
            if (menu) {
                positionMenu(activeToggle, menu);
            }
        }
    };
    window._dropdownKeyHandler = function (e) {
        if (e.key === 'Escape') closeActive();
    };
    document.addEventListener('click', window._dropdownClickHandler, true);
    document.addEventListener('scroll', window._dropdownScrollHandler, true);
    document.addEventListener('keydown', window._dropdownKeyHandler, true);
};