$(document).ready(function () {
    $('#type-action-input').on('keypress', function (e) {
        if (e.which === 13) {
            $(this).attr('disabled', 'disabled');
            $.post({
                url: '/Gameplay/ExecuteAction',
                data: {
                    action: $(this).val(),
                    heroId: $('#ID').val()
                }
            }).then(function (result) {
                $.each(result, function (e) {
                    $('#events-container').append('<div class=\"col-12\">' + e + '</div>');
                });
            });
            $(this).val('');
            $(this).removeAttr('disabled');
        }
    });

    $('.delete-button').on('click', function () {
        let $this = $(this);
        $($this.data('target')).find('#confirm-modal').data('url', $this.data('href')).data('id', $this.data('id'));
    });

    $('#confirm-modal').on('click', function () {
        let $this = $(this);
        $.ajax({
            url: $this.data('url'),
            type: 'DELETE',
            data: {
                heroId: $this.data('id')
            },
            statusCode: {
                200 : function(response) {
                    $('#character-' + $this.data('id')).remove();
                }
            }
        });
    });
});