//#region Increase Decrease Value

function increaseValue(button) {
  const numberInput = button.parentElement.querySelector(".number");
  var value = parseInt(numberInput.innerHTML, 10);
  if (isNaN(value)) value = 0;
  numberInput.innerHTML = value + 1;
}

function decreaseValue(button) {
  const numberInput = button.parentElement.querySelector(".number");
  var value = parseInt(numberInput.innerHTML, 10);
  if (isNaN(value)) value = 0;
  if (value < 1) return;
  numberInput.innerHTML = value - 1;
}

//#endregion

//#region Price Range

function getVals() {
  // Get slider values
  let parent = this.parentNode;
  let slides = parent.getElementsByTagName("input");
  let slide1 = parseFloat(slides[0].value);
  let slide2 = parseFloat(slides[1].value);
  slides[0].value = slide1;
  slides[1].value = slide2;
  // Neither slider will clip the other, so make sure we determine which is larger
  if (slide1 > slide2) {
    let tmp = slide2;
    slide2 = slide1;
    slide1 = tmp;
  }

  let displayElement = parent.getElementsByClassName("rangeValues")[0];
    displayElement.innerHTML = "$" + slide1 + " - $" + slide2;

    console.log(slide1);
    console.log(slide2);
}


window.onload = function () {
  // Initialize Sliders
  let sliderSections = document.getElementsByClassName("range-slider");
  for (let x = 0; x < sliderSections.length; x++) {
    let sliders = sliderSections[x].getElementsByTagName("input");
    for (let y = 0; y < sliders.length; y++) {
      if (sliders[y].type === "range") {
        sliders[y].oninput = getVals;
        // Manually trigger event first time to display values
        sliders[y].oninput();
      }
    }
  }
};

//#endregion

//#region ImageChange

function changeImage(element) {
  var main_prodcut_image = document.getElementById("main_product_image");
  main_prodcut_image.src = element.src;
}

//#endregion

function launchNotification() {
  var x = document.getElementById("notification")
  x.className = "show";
  setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
}
