
document.addEventListener("DOMContentLoaded", initializePage, false);

function initializePage() {
    console.log("Welcome Page loaded");
    const searchBar = document.getElementById("search-bar");
    const loadingAnimation = document.getElementById("loading-animation");
    searchBar.addEventListener('input', debounce(() => {
                                            loadingAnimation.style.display = "block";
                                            const value = searchBar.value;
                                            if (value.length >= 2) {
                                                searchResults();
                                            } else
                                            {
                                                checkEmpty();
                                            }
                                        }, 500), false);
    searchBar.addEventListener('blur', checkEmpty, false);
    const showModal1 = document.getElementById("close-actor-modal1");
    const showModal2 = document.getElementById("close-actor-modal2");
    showModal1.addEventListener('click', () => closeModal(), false);
    showModal2.addEventListener('click', () => closeModal(), false);
}

function debounce(func, delay) {
    let timeoutId;
    return function() {
        const context = this;
        const args = arguments;
        clearTimeout(timeoutId);
        timeoutId = setTimeout(() => func.apply(context, args), delay);
    };
}

async function searchResults() {
    var value = document.getElementById("search-bar").value;
    const loadingAnimation = document.getElementById("loading-animation");
    var invalidInput = checkEmpty();
    if (invalidInput) {
        return;
    }
    const response = await fetch(`/api/search/actor/${value}`);
    const actors = await response.json();
    const ul = document.getElementById("results-list");
    ul.textContent = "";
    loadingAnimation.style.display = "none";
    const top10 = actors.slice(0, 10);
    if (top10.length === 0) {
        const li = document.createElement("li");
        li.textContent = "No Results Found";
        ul.appendChild(li);
        return;
    }
    top10.forEach(actor => {
        const li = document.createElement("li");
        li.innerHTML = `<button type="button" class="btn btn-secondary" data-bs-toggle="modal" data-bs-target="#actor-modal">${actor.fullName}</button>`
        li.addEventListener('click', () => actorShows(actor), false);
        ul.appendChild(li);
    });
}

async function actorShows(actor) {
    const showResponse = await fetch(`/api/actor/shows/${actor.id}`);
    const shows = await showResponse.json();
    const actorName = document.getElementById("actor-modal-label");
    const showList = document.getElementById("actor-show-list");
    actorName.textContent =`${actor.fullName}'s Shows`;
    showList.textContent = "";
    if (shows.length === 0) {
        const li = document.createElement("li");
        li.innerHTML = `<span class="fw-bold accentText">No Shows Found</span>`;
        showList.appendChild(li);
        return;
    }
    const fragment = document.createDocumentFragment();
    shows.forEach(show => {
        const li = document.createElement("li");
        li.innerHTML = `<ul class="list-unstyled d-flex flex-column align-items-start text-start">
                        <li><span class="fw-bold accentText">Title:</span> ${show.title}</li>
                        <hr>
                        <li><span class="fw-bold accentText">Description:</span> ${show.description}</li>
                        <hr>
                        <li><span class="fw-bold accentText">ShowType:</span> ${show.showTypeIdentifier}</li>
                        <hr>
                        <li><span class="fw-bold accentText">Rating:</span> ${show.certificationIdentifier}</li>
                        <hr>
                        <li><span class="fw-bold accentText">Release Year:</span> ${show.releaseYear}</li>
                        <hr>
                        <li><span class="fw-bold accentText">Runtime:</span> ${show.runtime}</li>
                        <hr>
                        <li><span class="fw-bold accentText">IMDB Score:</span> ${show.imdbScore}</li>
                        <hr>
                        <li><span class="fw-bold accentText">IMDB Votes:</span> ${show.imdbVotes}</li>
                        <hr>
                        <li><span class="fw-bold accentText">TMDB Score:</span> ${show.tmdbScore}</li>
                        <hr>
                        <li><span class="fw-bold accentText">TMDB Popularity:</span> ${show.tmdbPopularity}</li>
                        <hr>
                        <li><span class="fw-bold accentText">Genres:</span> ${show.genres}</li>
                        <hr>
                        <li><span class="fw-bold accentText">Director:</span> ${show.directorName}</li>
                        </ul>
                        <hr>
                        <br>`
        fragment.appendChild(li);
    });
    showList.append(fragment)
}

function closeModal() {
    const actorName = document.getElementById("actor-modal-label");
    actorName.textContent = "";
    const showList = document.getElementById("actor-show-list");
    showList.textContent = "";
}

function checkEmpty() {
    const whitespacePattern = /^\s*$/;
    const specialPattern = /[\\_\|\/\^!@%\&.=+\(\)#*\[\]\<\>\?\~\`]+/;
    const ul = document.getElementById("results-list");
    const loadingAnimation = document.getElementById("loading-animation");
    var value = document.getElementById("search-bar").value;
    if (specialPattern.test(value)) {
        loadingAnimation.style.display = "none";
        ul.textContent = "";
        const li = document.createElement("li");
        li.textContent = "Invalid Input";
        ul.appendChild(li);
        return true;
    } 
    if (value === '' || whitespacePattern.test(value)) {
        loadingAnimation.style.display = "none";
        ul.textContent = "";
        return true;
    }
    if (value.length < 2) {
        loadingAnimation.style.display = "none";
        ul.textContent = "";
        return true;
    }
    return false;
}