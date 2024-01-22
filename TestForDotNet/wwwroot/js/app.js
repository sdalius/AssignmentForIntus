
function AttachEventListeners(){
    var elements = document.querySelectorAll('.exploder');
    elements.forEach(function (elem) {
        elem.addEventListener('click', function () {

            if (elem.hasChildNodes()) {
                // Toggle button classes
                elem.classList.toggle('btn-success');
                elem.classList.toggle('btn-danger');

                // Toggle next row's hide class

                if (nextRow !== undefined || nextRow !== null) {
                    nextRow.classList.toggle('hide');
                }


                // Toggle slide up or down based on the presence of the hide class
                Array.from(nextRow.children).forEach(function (td) {
                    td.style.display = nextRow.classList.contains('hide') ? 'none' : '';
                });
            }

        });
    });
}