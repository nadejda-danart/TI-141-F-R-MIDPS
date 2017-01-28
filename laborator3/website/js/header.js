$(document).ready(function() {
  checkHeader();

  $(document).scroll(checkHeader);
});

function checkHeader() {
  if (window.scrollY === 0) {
    $('.navigation').removeClass('moved');
  } else {
    $('.navigation').addClass('moved');
  }
}
