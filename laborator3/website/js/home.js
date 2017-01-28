$(document).ready(function() {
  addEvents();
  onLoad();
});

function addEvents() {
  $('#start-arrow').click(function() {
    $('html, body').animate({scrollTop: $('section.portfolio').offset().top}, 700);
  });

  $(document).on('mouseenter', '.image-container .title', function() {
    $(this).parent().addClass('scale');
  });

  $(document).on('mouseleave', '.image-container .title', function() {
    $(this).parent().removeClass('scale');
  });

}

function onLoad() {
  var rowTemplate = `<div class="row image-list">{content}</div>`;
  var colTemplate = `
    <div class="{offset} col-md-3">
        <div class="image">
            <div class="image-container" style="background-image: url('{path}');">
                <div class="title black-background"></div>
                <div class="title">{title}</div>
            </div>
        </div>  
    </div>
    `;


  $.get('/portofolio-preview', null, function(response) {
    var finalContent = '';
    var row = rowTemplate;
    var cols = [];

    for (let index = 0; index < response.length; index++) {
      let item = response[index];

      let col = colTemplate
        .replace('{path}', item.path)
        .replace('{title}', item.name)
        .replace('{offset}', index === 0 || (index + 1) % 3 === 1 ? 'col-md-offset-1 firt-element' : '');
      cols.push(col);

      if ((index + 1) % 3 === 0 && index > 0) {
        finalContent += rowTemplate.replace('{content}', cols.join(''));
        cols = [];
      }
    }

    $('#portfolio').append(finalContent);
  }, 'json');
}

function sendQuestion() {
  var askform = $('#ask-form');
  var serialized = askform.serializeArray();
  var form = {};

  serialized.map(function(el) {
    form[el.name] = el.value;
  });

  $('#ask-form :input').prop('disabled', true);

  $.post('/question', form, function() {
    $('#ask').css('display', 'none');
    $('.thx-message').css('display', 'inline');
  }, 'json');
}