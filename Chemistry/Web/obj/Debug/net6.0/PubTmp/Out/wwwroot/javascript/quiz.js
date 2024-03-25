// !loading
const loader = document.querySelector('#preloader');
window.addEventListener('load', () => {
  loader.style.display = 'none';
});
document.addEventListener('DOMContentLoaded', function () {
  var timerElement = document.getElementById('timer');
  var adminTimeElement = document.querySelector(".get-time-admin-panel");
  var adminTimeValue = adminTimeElement.getAttribute("get-number");
  var duration = adminTimeValue*60 * 60;
  var timerInterval;

  function startTimer() {
    var timer = duration,
      hours,
      minutes,
      seconds;
    timerInterval = setInterval(function () {
      hours = parseInt(timer / 3600, 10);
      minutes = parseInt((timer % 3600) / 60, 10);
      seconds = parseInt((timer % 3600) % 60, 10);

      hours = hours < 10 ? '0' + hours : hours;
      minutes = minutes < 10 ? '0' + minutes : minutes;
      seconds = seconds < 10 ? '0' + seconds : seconds;

      timerElement.textContent = hours + ':' + minutes + ':' + seconds;

      if (timer <= 0) {
        clearInterval(timerInterval);
      } else {
        timer--;
      }
    }, 1000);
  }
  startTimer();
});
