$('#journal-form select[name^="Grades"]').on('change', function () {
    var formChanged = false;

    $('#journal-form select[name^="Grades"]').each(function () {
        if ($(this).data('grade') !== $(this).val()) {
            formChanged = true;
            $(this).attr('data-updated', true);
        } else {
            $(this).attr('data-updated', false);
        }
    });

    if (formChanged) {
        $('#journal-save-btn').removeAttr('disabled');
    } else {
        $('#journal-save-btn').prop('disabled', true);
    }
});

$('#journal-form select[name^="Grades"]').each(function () {
    $(this).data('grade', $(this).val());
});

$('#journal-form').on('submit', function (e) {
    e.preventDefault();

    let grades = [];

    $('select[name^="Grades"]').each(function () {
        let ids = $(this).attr('id').split('/');
        let number = parseInt($(this).val());
        let isUpdated = $(this).data('updated');

        grades.push({
            Id: ids[0],
            UserId: ids[1],
            SubjectId: ids[2],
            Number: number,
            IsUpdated: isUpdated
        });
    });


    fetch(`/update-grades`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(grades)
    })
        .then(response => {
            if (response.ok) {
                alert('Сохранено!');
                location.reload();
            } else {
                alert('Ошибка!');
            }
        })
});

$('.js--grades-row').on('click', '.js--grades-toggle-history', function () {
    $(this).closest('.js--grades-row').find('.js--grades-history').toggleClass('d-none');
});
