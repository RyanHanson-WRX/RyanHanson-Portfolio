/*            Made By: Ryan Hanson
       ____      ______   _   _   _       ____      
 ______\ \ \     | ___ \ | | | | | |     / / /______ 
 \______\ \ \    | |_/ / | | | |_| |    / / /______/
   ______\ \ \   |    /  | | |  _  |   / / /______
   \______\ \ \  | |\ \  | | | | | |  / / /______/
           \_\_\ \_| \_| | | \_| |_/ /_/_/          
                         | |                        
                         \/                        */

/* Dictionary for planets */
var planetDict = {
    0 : "Sun",
    0.39 : "Mercury",
    0.72 : "Venus",
    1 : "Earth",
    1.52 : "Mars",
    5.20 : "Jupiter",
    9.54 : "Saturn",
    19.20 : "Uranus",
    30.10 : "Neptune",
    39.40 : "Pluto",
};

/* Dictionary for planet images */
var imgDict = {
    0 : "media/sun.png",
    0.39 : "media/mercury.png",
    0.72 : "media/venus.jpeg",
    1 : "media/earth.png",
    1.52 : "media/mars.jpeg",
    5.20 : "media/jupiter.jpeg",
    9.54 : "media/saturn.jpeg",
    19.20 : "media/uranus.png",
    30.10 : "media/neptune.jpeg",
    39.40 : "media/pluto.jpeg", 
};

/* Array of Key values for planetDict */
var planetArr = [0, 0.39, 0.72, 1, 1.52, 5.20, 9.54, 19.20, 30.10, 39.40]

/* Nested Dictionary for radio buttons */
var planetAbout = {
    0 : {
        radius : "696,000 Kilometers",
        lightmins : "N/A",
    },
    0.39 : {
        radius : "2439 Kilometers",
        lightmins : "3.25 Light-Minutes",
    },
    0.72 : {
        radius : "6052 Kilometers",
        lightmins : "6.00 Light-Minutes",
    },
    1 : {
        radius : "6378 Kilometers",
        lightmins : "8.33 Light-Minutes",
    },
    1.52 : {
        radius : "3393 Kilometers",
        lightmins : "12.6 Light-Minutes",
    },
    5.20 : {
        radius : "71,492 Kilometers",
        lightmins : "43.3 Light-Minutes",
    },
    9.54 : {
        radius : "60,268 Kilometers",
        lightmins : "79.5 Light-Minutes",
    },
    19.20 : {
        radius : "25,559 Kilometers",
        lightmins : "160 Light-Minutes",
    },
    30.10 : {
        radius : "24,766 Kilometers",
        lightmins : "250 Light-Minutes",
    },
    39.40 : {
        radius : "1137 Kilometers",
        lightmins : "328 Light-Minutes",
    },
};

/* Calculation for user input */
function jsAUCalc() {
    let x = document.getElementById("AU").value;
    let planetKey = closest(x, planetArr);
    document.getElementById("answer").innerHTML = "The planet with the closest AU distance to your input is: " + planetDict[planetKey] + " at " + planetKey + " AU(s).";
    let divEl = document.getElementById("img");
    divEl.setAttribute("style", "text-align:center");
    let img = document.getElementById("image");
    let checkbox = document.getElementById("toggle");
    if (checkbox.checked){
        img.src = "";
    } else {
        img.src = imgDict[planetKey];
    };
    let newItem = document.createElement("li");
    newItem.innerHTML = planetDict[planetKey];
    let ulist = document.getElementById("spaceList");
    ulist.appendChild(newItem);
    radioShow();
};

/* Calculation for closest planet AU distance compared to user input */
function closest(inputNum, arr) {
    var current = arr[0];
    var diff = Math.abs (inputNum - current);
    for (var i = 0; i < arr.length; i++) {
        var newDiff = Math.abs(inputNum - arr[i]);
        if (newDiff < diff) {
            diff = newDiff;
            current = arr[i];
        }
    }
    return current;
};

/* Reset function to reload page */
function jsReset() {
    window.location.reload(true);
};

/* toggles radio features */
function radioShow() {
    let x = document.getElementById("AU").value;
    let planetKey = closest(x, planetArr);
    let radios = document.querySelectorAll('input[name="radio"]');
    let selectedRadio;
    let paragraph = document.getElementById("about");
    for (const radioBtn of radios) {
        if (radioBtn.checked){
            selectedRadio = radioBtn.value;
            break;
        };
    };
    paragraph.innerHTML = planetAbout[planetKey][selectedRadio];
};

/* toggles image being shown / hidden */
function picToggle() {
    let x = document.getElementById("AU").value;
    let planetKey = closest(x, planetArr);
    let checkbox = document.getElementById("toggle");
    let divEl = document.getElementById("img");
    divEl.setAttribute("style", "text-align:center");
    let img = document.getElementById("image");
    if (checkbox.checked) {
        img.src = "";
    } else {
        img.src = imgDict[planetKey];
    };
};

/* Black hole */
function blackHole() {
    alert("Oh no, you've entered a black hole! Time and Space have seized to exist!");
};