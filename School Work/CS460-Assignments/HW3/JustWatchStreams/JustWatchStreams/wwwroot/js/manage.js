
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
    const newActor = document.getElementById("new-actor");
    newActor.addEventListener('click', () => openModal(), false);
    const closeModal1 = document.getElementById("close-actor-modal1");
    const closeModal2 = document.getElementById("close-actor-modal2");
    const saveActor = document.getElementById("submit-actor-modal");
    closeModal1.addEventListener('click', () => closeModal(), false);
    closeModal2.addEventListener('click', () => closeModal(), false);
    saveActor.addEventListener('click', () => putActor(), false);
    const closeMessageBtn = document.getElementById("close-message");
    closeMessageBtn.addEventListener('click', () => closeMessage(), false);
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
        li.addEventListener('click', () => openModal(actor), false);
        ul.appendChild(li);
    });
}

function openModal(actor) {
    console.log("Opening Modal...");
    const actorName = document.getElementById("actor-modal-label");
    const modalForm = document.getElementById("actor-modal-form");
    if (actor === undefined)
    {
        console.log("Adding New Actor...");
        actorName.textContent = "Add New Actor:";
        const idInput = document.createElement("div");
        idInput.innerHTML = `<input type="hidden" id="idInput" class="col-form-label" value=0></input>`;
        const justWatchIdInput = document.createElement("div");
        justWatchIdInput.innerHTML = `<label for="justWatchIdInput" class="col-form-label">JustWatch ID:</label>
                                      <input type="text" class="form-control" id="justWatchIdInput">`;
        const fullNameInput = document.createElement("div");
        fullNameInput.innerHTML = `<label for="fullNameInput" class="col-form-label">Full Name:</label>
                                   <input type="text" class="form-control" id="fullNameInput">`;
        modalForm.appendChild(idInput);
        modalForm.appendChild(justWatchIdInput);
        modalForm.appendChild(fullNameInput);
    }
    else
    {
        console.log("Editing Actor...");
        actorName.textContent = `Edit ${actor.fullName}:`;
        const idInput = document.createElement("div");
        idInput.innerHTML = `<input type="hidden" id="idInput" class="col-form-label" value=${actor.id}></input>`;
        const justWatchIdInput = document.createElement("div");
        justWatchIdInput.innerHTML = `<label for="justWatchIdInput" class="col-form-label">JustWatch ID:</label>
                                      <input type="text" class="form-control" id="justWatchIdInput" value=${actor.justWatchPersonId}>`;
        const fullNameInput = document.createElement("div");
        fullNameInput.innerHTML = `<label for="fullNameInput" class="col-form-label">Full Name:</label>
                                   <input type="text" class="form-control" id="fullNameInput" value="${actor.fullName}">`;
        modalForm.appendChild(idInput);
        modalForm.appendChild(justWatchIdInput);
        modalForm.appendChild(fullNameInput);
    }
}

async function putActor() {
    const searchResults = document.getElementById("results-list");
    searchResults.textContent = "";
    const searchBar = document.getElementById("search-bar");
    searchBar.value = "";
    const id = document.getElementById("idInput").value;
    const justWatchId = document.getElementById("justWatchIdInput").value;
    const fullName = document.getElementById("fullNameInput").value;

    const actor = {
        id: id,
        justWatchPersonId: justWatchId,
        fullName: fullName
    };
    console.log(actor);

    const response = await fetch(`/api/actor/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify(actor)
    });
    if (response.ok) {
        console.log("Actor Saved");
        closeModal();
        const statusDiv = document.getElementById("status-message-area");
        statusDiv.style = "border: 1px rgb(0, 255, 217) solid;";
        const statusMessage = document.getElementById("status-message-text");
        statusMessage.textContent = "Actor Saved Successfully!";
        const closeMessageBtn = document.getElementById("close-message");
        closeMessageBtn.style.display = "block";
    }
    else {
        console.log("Error Saving Actor");
        closeModal();
        const statusDiv = document.getElementById("status-message-area");
        statusDiv.style = "border: 1px rgb(0, 255, 217) solid;";
        const statusMessage = document.getElementById("status-message-text");
        statusMessage.textContent = "Error Saving Actor";
        const closeMessageBtn = document.getElementById("close-message");
        closeMessageBtn.style.display = "block";
    }
}

function closeModal() {
    const actorName = document.getElementById("actor-modal-label");
    actorName.textContent = "";
    const modalForm = document.getElementById("actor-modal-form");
    modalForm.textContent = "";
}

function closeMessage() {
    const statusDiv = document.getElementById("status-message-area");
    statusDiv.style = "border: none;";
    const statusMessage = document.getElementById("status-message-text");
    statusMessage.textContent = "";
    const closeMessageBtn = document.getElementById("close-message");
    closeMessageBtn.style.display = "none";
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