AOS.init();

const filterButton = document.querySelectorAll('.filterLesson ul li button');
//  ! filterButton
  filterButton.forEach(btn => {
     btn.onclick = function() {
      //active
      filterButton.forEach(li => {
          li.className = "";
      })
      btn.className = "button-filter-active";
     }
  });


