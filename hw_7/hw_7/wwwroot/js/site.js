function tableFilters(actionSort, actionSearch) {
    let $table, $tableBody;
    
    $('.js--table-block__sort-btn').on("click", function() {
        // Плучаем информацию о кнопке
        const $btn = $(this);
        const value = $btn.data("sort-value");
        let option = $btn.data("sort-order") || null;
        console.log(option);

        // У активных кнопок збрасываем до нейтсрального состочния
        $('.js--table-block__sort-btn.active').each(function() {
            $(this)
                .removeClass(`active table-block__sort-btn--${$(this).data("sort-order")}`)
                .data("sort-order", null);
        });


        // Устанавливаем тип сортировки
        if (option === "desc") {
            option = "asc";
        } else if (option === "asc") {
            option = null;
        } else {
            option = "desc";
        }

        // Добовляем класс для выбраного типо сортировки
        if (option !== null) $btn.addClass(`active table-block__sort-btn--${option}`);
        $btn.data("sort-order", option);

        $table = $btn.closest(".js--table-block");
        $tableBody = $table.find(".js--table-block__tbody");
        
        // Отправляем запрос
        sendRequest(actionSort, value, option);
    });

    $(".js--table-block__search_toggle").on("click", function (event) {
        let $btn = $(this);
        let container = $btn.closest('.js--table-block__search');
        
        if (!container.hasClass('active')) {
            container.addClass('active');
        } else if ($(event.target).closest('.js--table-block__search_input_holder').length) {
            let value = container.find("input").val();
            let option = container.find("select").val();
            
            $table = $btn.closest(".js--table-block");
            $tableBody = $table.find(".js--table-block__tbody");

            sendRequest(actionSearch, value, option);
        } else {
            container.removeClass('active');
            container.find('input').val('');
        }
    });
    
    const sendRequest = (action, value, option) => {
        if (!value && !option) return;
        
        $.ajax({
            url: action,
            method: 'GET',
            data: { value, option },
            success: function (response) {
                $($tableBody).html(response);
            },
            error: function (error) {
                console.error(error);
                let columnCount = $($table).find("tr:first-child *").length;
                $($tableBody).html(`<tr><td class="table-block__error text-center" colspan="${columnCount}">${"Error: " + error.responseText}</td></tr>`);
            }
        });
    }
}
