$(document).ready(function() {
  onLoad();
  addEvents();
});

function onLoad() {
  let rowTemplate = `<div class="row">{content}</div>`;
  let colTemplate = `
    <div class="col-md-4">
        <div class="image">
            <div class="image-container" style="background-image: url('{path}');">
                <div class="title black-background"></div>
                <div class="title">{title}</div>
            </div>
        </div>  
    </div>
    `;


  $.get('/portofolio', null, function(response) {
    let finalContent = '';
    let row = rowTemplate;
    let cols = [];

    for (let index = 0; index < response.length; index++) {
      let item = response[index];

      cols.push(colTemplate.replace('{path}', item.path).replace('{title}', item.name));

      if ((index + 1) % 3 === 0 && index > 0) {
        finalContent += rowTemplate.replace('{content}', cols.join(''));
        cols = [];
      }
    }

    $('#portfolio').append(finalContent);
  }, 'json');
}

function addEvents() {
  $(document).on('mouseenter', '.image-container .title', function() {
    $(this).parent().addClass('scale');
  });

  $(document).on('mouseleave', '.image-container .title', function() {
    $(this).parent().removeClass('scale');
  });
}
