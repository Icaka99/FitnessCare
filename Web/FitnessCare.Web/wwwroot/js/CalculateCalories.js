window.onload = calculate();

function calculate() {
    var gender = document.querySelector('input[name="Gender"]:checked').value;
    var weight = $("#Weight").val();
    var height = $("#Height").val();
    var age = $("#Age").val();
    var activity = document.getElementById('Activity').value;
    var multiplicator = 1;
    var result = 0;

    if (activity == 1) {
        multiplicator = 1.2
    } else if (activity == 2) {
        multiplicator = 1.375
    } else if (activity == 3) {
        multiplicator = 1.55
    } else if (activity == 4) {
        multiplicator = 1.725
    } else if (activity == 5) {
        multiplicator = 1.9
    }

    if (gender == "true") {
        result = Math.round(Math.round(88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age)) * multiplicator);
    } else {
        result = Math.round(Math.round(447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age)) * multiplicator);
    }

    if (activity == 0) {
        document.getElementById("Calories").innerHTML = result + " kcal/day";
        document.getElementById("Calories1").innerHTML = "Not Applicable";
        document.getElementById("LoseHalf").innerHTML = "Not Applicable";
        document.getElementById("LoseOne").innerHTML = "Not Applicable";
        document.getElementById("GainHalf").innerHTML = "Not Applicable";
        document.getElementById("GainOne").innerHTML = "Not Applicable";
    } else {
        document.getElementById("Calories").innerHTML = result + " kcal/day";
        document.getElementById("Calories1").innerHTML = result + " kcal/day";
        document.getElementById("LoseHalf").innerHTML = result - 500 + " kcal/day";
        document.getElementById("LoseOne").innerHTML = result - 1000 + " kcal/day";
        document.getElementById("GainHalf").innerHTML = result + 500 + " kcal/day";
        document.getElementById("GainOne").innerHTML = result + 1000 + " kcal/day";
    }
}