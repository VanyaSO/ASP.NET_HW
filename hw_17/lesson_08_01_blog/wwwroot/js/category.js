function sortCols() {
    var input, filter, ul, li, a, i;
    input = document.getElementById('colsearch');
    filter = input.value.toUpperCase();
    ul = document.getElementById("col-inactive");
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("label")[0];
        if (a.textContent.trim().toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

function sortCols2() {
    var input, filter, ul, li, a, i;
    input = document.getElementById('colsearch2');
    filter = input.value.toUpperCase();
    ul = document.getElementById("col-inactive2");
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("label")[0];
        if (a.textContent.trim().toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

$(':checkbox').on('change', function (e) {
    if (this.checked == false && $(this).closest('#col-active').length == 1) {
        $(this).closest('li').appendTo('#col-inactive');
    }
    if (this.checked == true && $(this).closest('#col-inactive').length == 1) {
        $(this).closest('li').appendTo('#col-active');
    }
})

$(':checkbox').on('change', function (e) {
    if (this.checked == false && $(this).closest('#col-active2').length == 1) {
        $(this).closest('li').appendTo('#col-inactive2');
    }
    if (this.checked == true && $(this).closest('#col-inactive2').length == 1) {
        $(this).closest('li').appendTo('#col-active2');
    }
})