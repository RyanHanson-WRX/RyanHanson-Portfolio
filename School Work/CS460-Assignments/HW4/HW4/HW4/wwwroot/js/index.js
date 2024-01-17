document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    console.log("Page loaded, initializing javascript");
    const searchButton = document.getElementById("search-button");
    searchButton.addEventListener("click", searchMovies, false);
    const closeModal1 = document.getElementById("close-movie-modal1");
    const closeModal2 = document.getElementById("close-movie-modal2");
    closeModal1.addEventListener('click', () => closeModal(), false);
    closeModal2.addEventListener('click', () => closeModal(), false);
}

async function searchMovies(e) {
    e.preventDefault();
    console.log("Searching movies");
    const searchInput = document.getElementById("search-input");
    const resultsDiv = document.getElementById("movie-results");
    const searchValue = searchInput.value;
    const whitespacePattern = /^\s*$/;
    const specialPattern = /[\\_\|\/\^!@%\&.=+\(\)#*\[\]\<\>\~\`\;]+/;

    if (!searchValue || searchValue.length < 3) {
        resultsDiv.innerHTML = '<h3 class="text-danger text-center">Please enter a movie title</h3>';
        return;
    }
    else if (whitespacePattern.test(searchValue)) {
        resultsDiv.innerHTML = '<h3 class="text-danger text-center">Please enter a valid movie title</h3>';
        return;
    }
    else if (specialPattern.test(searchValue)) {
        resultsDiv.innerHTML = '<h3 class="text-danger text-center">Please enter a valid movie title</h3>';
        return;
    }

    const url = `/api/movie/search?title=${searchValue}`;
    console.log(url);
    try {
        const response = await fetch(url);
        if (response.status !== 200) {
            if (response.status === 204)
            {
                resultsDiv.innerHTML = '<h3 class="text-danger text-center">Service Unavailable</h3>';
                return;
            }
            resultsDiv.innerHTML = '<h3 class="text-danger text-center">Something went wrong</h3>';
            return;
        }
        const movies = await response.json();
        console.log(movies);
        resultsDiv.textContent = "";
        const movieTemplate = document.getElementById("movie-template");
        if (movies.length === 0) {
            resultsDiv.innerHTML = '<h3 class="text-danger text-center">No results found</h3>';
            return;
        }
        movies.forEach(movie => {
            const clone = movieTemplate.content.cloneNode(true);
            const movieDiv = clone.querySelector("div");
            const poster = clone.querySelector("img");
            const title = clone.querySelector("h5");
            const date = clone.querySelector("h6");
            const description = clone.querySelector("p");
            if (movie.posterImagePath) {
                poster.src = `https://image.tmdb.org/t/p/w94_and_h141_bestv2/${movie.posterImagePath}`;
                poster.alt = `${movie.title} poster`;
            }
            title.textContent = movie.title;
            date.textContent = movie.releaseDate;
            if(movie.description.length > 140) {
                description.textContent = movie.description.slice(0, 140) + "...";
            } else {
                description.textContent = movie.description;
            }
            movieDiv.addEventListener('click', () => getMovieInfo(movie.id), false);
            resultsDiv.appendChild(clone);
        })
    }
    catch (err) {
        resultsDiv.innerHTML = '<h3 class="text-danger text-center">Something went wrong</h3>';
        return;
    }
}

async function getMovieInfo(id) {
    const resultsDiv = document.getElementById("movie-results");
    console.log("Getting movie info");
    closeModal();
    try {
        const detailsUrl = `/api/movie/${id}`;
        console.log(detailsUrl);
        const detailResponse = await fetch(detailsUrl);
        if (detailResponse.status !== 200) {
            if (response.status === 204)
            {
                resultsDiv.innerHTML = '<h3 class="text-danger text-center">Service Unavailable</h3>';
                return;
            }
            resultsDiv.innerHTML = '<h3 class="text-danger text-center">Something went wrong</h3>';
            return;
        }
        const movieDetails = await detailResponse.json();
        console.log(movieDetails);
        const creditsUrl = `/api/movie/${id}/credits`;
        console.log(creditsUrl);
        const creditsResponse = await fetch(creditsUrl);
        if (creditsResponse.status !== 200) {
            if (response.status === 204)
            {
                resultsDiv.innerHTML = '<h3 class="text-danger text-center">Service Unavailable</h3>';
                return;
            }
            resultsDiv.innerHTML = '<h3 class="text-danger text-center">Something went wrong</h3>';
            return;
        }
        const movieCredits = await creditsResponse.json();
        console.log(movieCredits);
        const movieModalLabel = document.getElementById("movie-modal-label-title");
        const movieModalYearLabel = document.getElementById("movie-modal-label-year");
        movieModalLabel.textContent = movieDetails.title;
        movieModalYearLabel.textContent = `(${movieDetails.year})`;
        const modalBody = document.getElementById("movie-modal-body");
        const movieDetailsTemplate = document.getElementById("movie-details-template");
        const clone = movieDetailsTemplate.content.cloneNode(true);
        const background = clone.querySelector("img");
        const releaseDate = clone.querySelector("#release-date");
        const genres = clone.querySelector("#genres-list");
        const runtime = clone.querySelector("#runtime");
        const popularity = clone.querySelector("#popularity");
        const revenue = clone.querySelector("#revenue");
        const description = clone.querySelector("#overview");
        const castList = clone.querySelector("#cast-list");
        if (movieDetails.backgroundImagePath)
        {
            background.src = `https://image.tmdb.org/t/p/w1920_and_h800_multi_faces/${movieDetails.backgroundImagePath}`;
            background.alt = `${movieDetails.title} background`;
        }
        releaseDate.textContent = movieDetails.releaseDate;
        if (movieDetails.genres.length > 0) 
        {
            movieDetails.genres.forEach(genre => {
                const genreLi = document.createElement("li");
                genreLi.textContent = genre;
                genreLi.style.listStyleType = "none";
                genres.appendChild(genreLi);
            });
        }
        else
        {
            const genreLi = document.createElement("li");
            genreLi.textContent = "N/A";
            genreLi.style.listStyleType = "none";
            genres.appendChild(genreLi);
        }
        runtime.textContent = movieDetails.runtime;
        popularity.textContent = movieDetails.popularity;
        revenue.textContent = movieDetails.revenue;
        if (movieDetails.description.length > 0)
        {
            description.textContent = `"${movieDetails.description}"`;
        }
        else
        {
            description.textContent = "N/A";
        }
        if (movieCredits.cast.length > 1) 
        {
            if (movieCredits.cast.length > 15) 
            {
                movieCredits.cast = movieCredits.cast.slice(0, 15);
            }
            movieCredits.cast.forEach(actor => {
                const actorLi = document.createElement("li");
                actorLi.textContent = actor;
                actorLi.style.listStyleType = "none";
                castList.appendChild(actorLi);
            });
        }
        else
        {
            const actorLi = document.createElement("li");
            actorLi.textContent = "N/A";
            actorLi.style.listStyleType = "none";
            castList.appendChild(actorLi);
        }
        modalBody.appendChild(clone);
    }
    catch (err) {
        resultsDiv.innerHTML = '<h3 class="text-danger text-center">Something went wrong</h3>';
        return;
    }
}

function closeModal() {
    const modalLabel = document.getElementById("movie-modal-label-title");
    modalLabel.textContent = "";
    const modalYearLabel = document.getElementById("movie-modal-label-year");
    modalYearLabel.textContent = "";
    const modalBody = document.getElementById("movie-modal-body");
    modalBody.textContent = "";
}