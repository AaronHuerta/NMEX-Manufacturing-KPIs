const body = document.querySelector("body"),
    sidebar = body.querySelector(".sidebar"),
    toggle = body.querySelector(".toggle"),
    searchBtm = body.querySelector(".search-box"),
    modeSwitch = body.querySelector(".toggle-switch"),
    modeText = body.querySelector(".mode-text");

toggle.addEventListener("click", () => {
    sidebar.classList.toggle("close")
})

modeSwitch.addEventListener("click", () => {
    body.classList.toggle("dark");

    if (body.classList.contains("dark")) {
        modeText.innerText = "Light Mode"
    } else {
        modeText.innerText = "Dark Mode"
    }

    // Guarda el estado del modo oscuro en el almacenamiento local
    localStorage.setItem('darkMode', body.classList.contains('dark'));
});

// Comprueba si el modo oscuro está activado en el almacenamiento local al cargar la página
document.addEventListener('DOMContentLoaded', (event) => {
    const isDarkMode = localStorage.getItem('darkMode') === 'true';

    if (isDarkMode) {
        body.classList.add('dark');
        modeSwitch.classList.add('active');
        modeText.innerText = "Light Mode";
    } else {
        body.classList.remove('dark');
        modeSwitch.classList.remove('active');
        modeText.innerText = "Dark Mode";
    }
});
